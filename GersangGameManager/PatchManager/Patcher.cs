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
		private const string _cdnUrl = "http://akgersang.xdn.kinxcdn.com/Gersang/Patch/";
		private const string _readmeFile = "Client_Readme/readme.txt";
		private const string _infoFolder = "Client_info_File/";
		private const string _patchFolder = "Client_Patch_File/";
		private string _serverFolder;

		private HttpService _httpService;

		private ServerType _serverType;
		private string _installPath;
		private const string tempInfoFolder = ".\\temp_info\\";
		private const string tempPatchFolder = ".\\temp_patch\\";

		private string[] _patchNote;
		private PatchList _patchList;

		private int _currentVersion = -1;

		public Patcher()
		{
			_httpService = new HttpService();
			_httpService.BaseAddress = _cdnUrl;
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
			_patchList = new PatchList();
		}

		private void SetServerFolder(ServerType serverType) => _serverFolder = serverType switch
		{
			ServerType.Main => "Gersang_Server/",
			ServerType.Test => "Test_Server/",
			_ => "Gersang_Server/"
		};

		public async Task<bool> CheckNeedToUpdate()
		{
			var currentVersionCheck = CheckCurrentVersion();
			var localVersion = CheckLocalVersion(_installPath);
			_currentVersion = await currentVersionCheck;

			return localVersion < _currentVersion;
		}

		public async Task<bool> Update(IProgress<UpdateProgressEventArgs> progressHandler, CancellationToken cancellationToken = default)
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();
			var patchNote = await GetPatchNote(progressHandler);
			var currentVersionCheck = CheckCurrentVersion(progressHandler);
			var localVersion = CheckLocalVersion(_installPath);
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

			ClearTempDirectory();

			var versionList = await GetVersionList(patchNote);
			versionList = versionList.Where(x => x > localVersion).ToArray();

			await DownloadPatchInfo(versionList, progressHandler);

			SetPatchList(Directory.GetFiles(tempInfoFolder));

			await DownloadPatchFileList(progressHandler, cancellationToken);
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
			FileService.CopyAll(tempPatchFolder, _installPath, copyProgress);
			ClearTempDirectory();
			_patchList.Clear();

			sw.Stop();
			Debug.WriteLine(sw.ElapsedMilliseconds);

			return true;
		}

		public async Task DownloadPatchInfo(int[] versionList, IProgress<UpdateProgressEventArgs> progressHandler)
		{
			var downloadInfoTasks = versionList.Select(rev =>
			{
				var url = _serverFolder + _infoFolder + rev;
				return _httpService.DownloadAsync(url, tempInfoFolder + rev);
			});
			await Task.WhenAll(downloadInfoTasks);
		}

		private void ClearTempDirectory()
		{
			if (Directory.Exists(tempInfoFolder))
				Directory.Delete(tempInfoFolder, true);
			if (Directory.Exists(tempPatchFolder))
				Directory.Delete(tempPatchFolder, true);
		}

		private void SetPatchList(string fileName)
		{
			if (!File.Exists(fileName))
				return;

			var lines = File.ReadAllLines(fileName);
			foreach (var line in lines)
			{
				var splited = line.Split('\t');
				if (!int.TryParse(splited[0], out _))
					continue;

				_patchList.Add(new PatchInfo
				{
					FileName = splited[1],
					FilePath = splited[3],
				});
			}
		}

		private void SetPatchList(string[] files)
		{
			foreach (var file in files)
				SetPatchList(file);
		}

		private volatile int _completed = 0;
		public async Task DownloadPatchFileList(IProgress<UpdateProgressEventArgs> progressHandler, CancellationToken cancellationToken = default)
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
				foreach (var patchInfo in _patchList)
				{
					await semaphore.WaitAsync();
					tasks.Add(DownloadPatchFile(patchInfo, semaphore, progressHandler, cancellationToken)
						.ContinueWith(_ =>
						{
							args.Percentage = (int)Math.Ceiling(++_completed / (float)_patchList.Count * 100);
							args.Percentage = args.Percentage <= 100 ? args.Percentage : 100;
							progressHandler?.Report(args);
						}));
				}
				await Task.WhenAll(tasks);
			}
		}

		private async Task DownloadPatchFile(PatchInfo patchInfo, SemaphoreSlim semaphore, IProgress<UpdateProgressEventArgs> progressHandler = null, CancellationToken cancellationToken = default)
		{
			var downloadPath = Path.Combine(Environment.CurrentDirectory, tempPatchFolder);
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
			var url = _serverFolder + _patchFolder + path + patchInfo.FileName;
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

		public int CheckLocalVersion(string installPath)
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

		public async Task<int> GetCurrentVersion(ServerType serverType)
		{
			SetServerFolder(serverType);
			return await CheckCurrentVersion();
		}

		public async Task<int> CheckCurrentVersion(IProgress<UpdateProgressEventArgs> progressHandler = null)
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

		public async Task<string[]> GetPatchNote(IProgress<UpdateProgressEventArgs> progressHandler)
		{
			var skipReport = progressHandler is null;
			var args = new UpdateProgressEventArgs();
			if (!skipReport)
			{
				args.UpdateTaskType = UpdateTaskType.GetPatchNote;
				args.Percentage = 0;
				progressHandler?.Report(args);
			}
			var url = _serverFolder + _readmeFile;

			var bytes = await _httpService.GetAsBytesAsync(url);
			var patchNote = Encoding.GetEncoding("euc-kr").GetString(bytes);

			if (!skipReport)
			{
				args.Percentage = 100;
				progressHandler?.Report(args);
			}

			return Regex.Split(patchNote, "\r\n|\r|\n");
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
