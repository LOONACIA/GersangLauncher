using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace GersangGameManager.Service
{
	internal static class FileService
	{
		public static void CopyAll(DirectoryInfo source, DirectoryInfo destination, DirectoryInfo? backUpPath, IProgress<float> progressHandler)
		{
			Directory.CreateDirectory(destination.FullName);

			int numOfFiles = Directory.EnumerateFiles(source.FullName, "*.*", SearchOption.AllDirectories).Count();
			CopyAll(source, destination, backUpPath, 0, numOfFiles, progressHandler);
			progressHandler.Report(1);
		}

		private static void CopyAll(DirectoryInfo source, DirectoryInfo destination, DirectoryInfo? backUpPath, int current, int numOfFiles, IProgress<float> progressHandler)
		{
			foreach (FileInfo fi in source.GetFiles())
			{
				var fullPath = Path.Combine(destination.FullName, fi.Name);

				var destFi = new FileInfo(fullPath);
				var isSymbolicLink = destFi.Attributes.HasFlag(FileAttributes.ReparsePoint);
				if (!isSymbolicLink)
				{
					if (backUpPath != null)
					{
						if (File.Exists(fullPath))
						{
							var backUpFile = Path.Combine(backUpPath.FullName, fi.Name);
							var dir = Path.GetDirectoryName(backUpFile);
							if (!Directory.Exists(dir))
								Directory.CreateDirectory(dir);
							File.Copy(fullPath, backUpFile, true);
						}
					}
					fi.CopyTo(fullPath, true);
				}
				current++;
				if (current % 10 == 0)
				{
					Debug.Assert(current / (float)numOfFiles <= 1);
					progressHandler.Report(current / (float)numOfFiles);
				}
			}

			foreach (DirectoryInfo subDir in source.GetDirectories())
			{
				DirectoryInfo nextSubDir = destination.CreateSubdirectory(subDir.Name);
				DirectoryInfo nextBackUpPath = backUpPath.CreateSubdirectory(subDir.Name);
				CopyAll(subDir, nextSubDir, nextBackUpPath, current, numOfFiles, progressHandler);
			}
		}

		public static void CopyAll(string source, string target, string? backUpPath, IProgress<float> progressHandler)
		{
			var sourceDi = new DirectoryInfo(source);
			var targetDi = new DirectoryInfo(target);
			var backUpDi = string.IsNullOrEmpty(backUpPath) ? null : new DirectoryInfo(backUpPath);
			CopyAll(sourceDi, targetDi, backUpDi, progressHandler);
		}
	}
}
