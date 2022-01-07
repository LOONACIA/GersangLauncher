using GersangGameManager.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace GersangGameManager.PatchManager
{
	internal class Patcher
	{
		private const string CdnUrl = "http://akgersang.xdn.kinxcdn.com/Gersang/Patch/";
		private const string ReadmeFile = "Client_Readme/readme.txt";
		private const string InfoFolder = "Client_info_File/";
		private const string PatchFolder = "Client_Patch_File/";
		private string _serverFolder;

		private HttpService _httpService;

		private ServerType _serverType;
		private string _installPath;
		private const string TempInfoFolder = ".\\temp_info\\";
		private const string TempPatchFolder = ".\\temp_patch\\";

		private string _backUpDirectoryName;
		internal string BackUpDirectoryName
		{
			get => _backUpDirectoryName;
			set => _backUpDirectoryName = value;
		}

		private string[] _patchNote;

		private volatile int _completed = 0;

		private int _currentVersion = -1;

		public Patcher()
		{
			_httpService = new HttpService();
			_httpService.BaseAddress = CdnUrl;
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
		}

		public void Configure(ClientInfo clientInfo)
		{
			Configure(clientInfo.ClientPath, clientInfo.ServerType);
		}

		public void Configure(string installPath, ServerType serverType)
		{
			_installPath = installPath;
			_serverType = serverType;
			SetServerFolder(_serverType);
		}

		private void SetServerFolder(ServerType serverType) => _serverFolder = serverType switch
		{
			ServerType.Main => "Gersang_Server/",
			ServerType.Test => "Test_Server/",
			_ => "Gersang_Server/"
		};

		public async Task<bool> CheckNeedToUpdate()
		{
			var currentVersionCheck = GetCurrentVersion();
			var localVersion = GetLocalVersion(_installPath);
			_currentVersion = await currentVersionCheck;

			return localVersion < _currentVersion;
		}

		public async Task<bool> Update(IProgress<UpdateProgressEventArgs> progressHandler, CancellationToken cancellationToken = default)
		{
			var patchNote = await GetPatchNote(progressHandler);
			var currentVersionCheck = GetCurrentVersion(progressHandler);
			var localVersion = GetLocalVersion(_installPath);
			_currentVersion = await currentVersionCheck;

			if (localVersion >= _currentVersion)
			{
				if (progressHandler is not null)
				{
					var args = new UpdateProgressEventArgs
					{
						UpdateTaskType = UpdateTaskType.NoUpdateRequired
					};
					progressHandler.Report(args);
				}
				return true;
			}

			var versionList = await GetVersionList(patchNote);
			versionList = versionList.Where(x => x > localVersion).ToArray();

			await DownloadPatchInfo(versionList, TempInfoFolder, progressHandler);

			var patchList = ReadPatchInfo(Directory.GetFiles(TempInfoFolder));

			await DownloadPatchFiles(patchList, progressHandler, cancellationToken);
			Progress<float> copyProgress = null;

			if (cancellationToken.IsCancellationRequested)
			{
				return false;
			}

			if (progressHandler is not null)
			{
				var args = new UpdateProgressEventArgs
				{
					UpdateTaskType = UpdateTaskType.CopyPatchFile,
					Percentage = 0
				};
				progressHandler.Report(args);

				copyProgress = new Progress<float>(x =>
				{
					args.Percentage = (int)Math.Ceiling(x * 100);
					progressHandler.Report(args);
				});
			}
			var backUpPath = Path.GetFullPath(".\\백업_" + _backUpDirectoryName);
			if(Directory.Exists(backUpPath))
				Directory.Delete(backUpPath, true);

			FileService.CopyAll(TempPatchFolder, _installPath, backUpPath, copyProgress);

			return true;
		}

		public async Task DownloadPatchInfo(IEnumerable<int> versionList, string targetPath, IProgress<UpdateProgressEventArgs> progressHandler)
		{
			var fullPath = Path.GetFullPath(targetPath);

			var skipReport = progressHandler is null;
			var args = new UpdateProgressEventArgs();
			if (!skipReport)
			{
				args.UpdateTaskType = UpdateTaskType.DownloadPatchInfo;
				args.Percentage = 0;
				progressHandler?.Report(args);
			}

			var downloadInfoTasks = versionList
				.Where(rev => !File.Exists(fullPath + rev))
				.Select(rev =>
				{
					var url = _serverFolder + InfoFolder + rev;
					var infoFileFullPath = fullPath + rev;
					return _httpService.DownloadAsync(url, fullPath + rev);
				});
			await Task.WhenAll(downloadInfoTasks);

			if (!skipReport)
			{
				args.Percentage = 100;
				progressHandler?.Report(args);
			}
		}

		internal void ClearTempDirectory()
		{
			if (Directory.Exists(TempInfoFolder))
				Directory.Delete(TempInfoFolder, true);
			if (Directory.Exists(TempPatchFolder))
				Directory.Delete(TempPatchFolder, true);
		}

		private PatchList ReadPatchInfo(string infoFileName)
		{
			if (!File.Exists(infoFileName))
				return null;

			var patchList = new PatchList();
			var lines = File.ReadAllLines(infoFileName);

			foreach (var line in lines)
			{
				var splited = line.Split('\t');
				if (!int.TryParse(splited[0], out _))
					continue;

				patchList.Add(new PatchInfo
				{
					FileName = splited[1],
					FilePath = splited[3],
				});
			}

			return patchList;
		}

		private PatchList ReadPatchInfo(IEnumerable<string> infoFileNames)
		{
			var patchList = new PatchList();
			var lists = infoFileNames
									.Select(infofileName => ReadPatchInfo(infofileName))
									.ToList();
			
			lists.ForEach(list => patchList.AddRange(list));

			return patchList;
		}

		private async Task DownloadPatchFiles(PatchList patchList, IProgress<UpdateProgressEventArgs> progressHandler, CancellationToken cancellationToken = default)
		{
			var skipReport = progressHandler is null;
			var args = new UpdateProgressEventArgs();
			if (!skipReport)
			{
				args.UpdateTaskType = UpdateTaskType.DownloadPatchFile;
				args.Percentage = 0;
				progressHandler?.Report(args);
			}

			List<Task> tasks = new List<Task>();
			int max = Environment.ProcessorCount * 2;
			using (var semaphore = new SemaphoreSlim(max))
			{
				foreach (var patchInfo in patchList)
				{
					await semaphore.WaitAsync();
					tasks.Add(DownloadPatchFile(patchInfo, semaphore, progressHandler, cancellationToken)
						.ContinueWith(_ =>
						{
							args.Percentage = (int)Math.Ceiling(++_completed / (float)patchList.Count * 100);
							args.Percentage = args.Percentage <= 100 ? args.Percentage : 100;
							progressHandler?.Report(args);
						}));
				}
				await Task.WhenAll(tasks);
			}
		}

		private async Task DownloadPatchFile(PatchInfo patchInfo, SemaphoreSlim semaphore, IProgress<UpdateProgressEventArgs> progressHandler = null, CancellationToken cancellationToken = default)
		{
			var downloadPath = Path.Combine(Environment.CurrentDirectory, TempPatchFolder);
			downloadPath = Path.GetFullPath(downloadPath) + patchInfo.FilePath;

			if (!Directory.Exists(downloadPath))
				Directory.CreateDirectory(downloadPath);

			var skipReport = progressHandler is null;
			var args = new UpdateProgressEventArgs();
			Progress<float> downloadProgress = null;
			if (!skipReport)
			{
				args.UpdateTaskType = UpdateTaskType.DownloadPatchFile;
				args.PatchInfo = patchInfo;
				args.DownloadProgress = 0;
				args.Percentage = -1;
				progressHandler?.Report(args);
				downloadProgress = new Progress<float>(x =>
				{
					args.DownloadProgress = x * 100;
					progressHandler?.Report(args);
				});
			}

			var path = patchInfo.FilePath.Replace("\\", "/");
			var zipPath = Path.Combine(downloadPath, patchInfo.FileName);

			var file = Path.Combine(downloadPath, Path.GetFileNameWithoutExtension(patchInfo.FileName));
			if (File.Exists(file))
			{
				args.DownloadProgress = 1;
				progressHandler?.Report(args);
				semaphore.Release();
				return;
			}

			var url = _serverFolder + PatchFolder + path + patchInfo.FileName;
			await _httpService.DownloadAsync(url, zipPath, downloadProgress, cancellationToken);

			int count = 3;
			while (!ZipFactory.ZipValidation(zipPath))
			{
				await _httpService.DownloadAsync(url, zipPath);
				count--;
				if (count <= 0)
					throw new InvalidDataException(zipPath);
			}

			await Task.Run(() =>
			{
				try
				{
					var file = Path.GetFileNameWithoutExtension(zipPath);
					if (File.Exists(file))
						File.Delete(file);
					ZipFactory.Unzip(zipPath, downloadPath);
					File.Delete(zipPath);
				}
				catch (IOException ex)
				{
					Debug.WriteLine("UnZipException: " + ex.Message);
				}
			}).ConfigureAwait(false);
			semaphore.Release();
		}

		internal int GetLocalVersion(string installPath)
		{
			if (string.IsNullOrEmpty(installPath))
				throw new ArgumentNullException(nameof(installPath));

			int vsn = -1;
			var vsnPath = Path.Combine(installPath, "Online\\vsn.dat");

			if (!File.Exists(vsnPath))
				return vsn;

			using (var fs = new FileStream(vsnPath, FileMode.Open))
			{
				using (var br = new BinaryReader(fs))
				{
					vsn = ~br.ReadInt32();
				}
			}

			return vsn;
		}

		internal async Task<int> GetCurrentVersion(ServerType serverType)
		{
			SetServerFolder(serverType);
			return await GetCurrentVersion();
		}

		internal async Task<int> GetCurrentVersion(IProgress<UpdateProgressEventArgs> progressHandler = null)
		{
			var skipReport = progressHandler is null;
			var args = new UpdateProgressEventArgs();
			if (!skipReport)
			{
				args.UpdateTaskType = UpdateTaskType.CheckCurrentVersion;
				args.Percentage = 0;
				progressHandler?.Report(args);
			}

			var versionList = await GetVersionList();
			var ret = -1;

			if (!skipReport)
			{
				args.Percentage = 100;
				progressHandler?.Report(args);
			}

			if (versionList.Length > 0)
				return versionList.ToList().Max();

			return ret;
		}

		internal async Task<string[]> GetPatchNote(IProgress<UpdateProgressEventArgs> progressHandler)
		{
			if (_patchNote != null)
				return _patchNote;

			var skipReport = progressHandler is null;
			var args = new UpdateProgressEventArgs();
			if (!skipReport)
			{
				args.UpdateTaskType = UpdateTaskType.GetPatchNote;
				args.Percentage = 0;
				progressHandler?.Report(args);
			}
			var url = _serverFolder + ReadmeFile;

			var bytes = await _httpService.GetAsBytesAsync(url);
			var patchNote = Encoding.GetEncoding("euc-kr").GetString(bytes);

			if (!skipReport)
			{
				args.Percentage = 100;
				progressHandler?.Report(args);
			}
			_patchNote = Regex.Split(patchNote, "\r\n|\r|\n");

			return _patchNote;
		}

		private async Task<int[]> GetVersionList(string[] patchNote = null, IProgress<UpdateProgressEventArgs> progressHandler = null)
		{
			if (patchNote is null)
				patchNote = await GetPatchNote(progressHandler);

			var skipReport = progressHandler is null;
			var args = new UpdateProgressEventArgs();
			if (!skipReport)
			{
				args.UpdateTaskType = UpdateTaskType.GetVersionList;
				args.Percentage = 0;
				progressHandler?.Report(args);
			}

			var regex_Find_Rev = new Regex(@"\[거상\s+패치\s+V(\d+)\]");

			if (!skipReport)
			{
				args.Percentage = 100;
				progressHandler?.Report(args);
			}

			return patchNote
					.Select(line =>
					{
						bool success = int.TryParse(regex_Find_Rev.Match(line).Groups[1].Value, out int rev);
						return (success, rev);
					})
					.Where(x => x.success)
					.Select(x => x.rev)
					.ToArray();
		}
	}
}
