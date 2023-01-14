using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace GersangGameManager.Service
{
	internal static class FileService
	{
		public static void CopyAll(DirectoryInfo source, DirectoryInfo destination, DirectoryInfo? backUpPath, IProgress<float>? progressHandler)
		{
			Directory.CreateDirectory(destination.FullName);

			int numOfFiles = Directory.EnumerateFiles(source.FullName, "*.*", SearchOption.AllDirectories).Count();
			CopyAll(source, destination, backUpPath, 0, numOfFiles, progressHandler);
			progressHandler?.Report(1);
		}

		private static void CopyAll(DirectoryInfo source, DirectoryInfo destination, DirectoryInfo? backUpPath, int current, int numOfFiles, IProgress<float> ?progressHandler)
		{
			foreach (FileInfo fi in source.GetFiles())
			{
				var fullPath = Path.Combine(destination.FullName, fi.Name);

				if (!IsSymbolicLink(fullPath))
				{
					if (backUpPath is not null && File.Exists(fullPath))
					{
						if (!backUpPath.Exists)
						{
							backUpPath.Create();
						}
						string backupDirectory = Path.Join(backUpPath.FullName, fi.Name);
						File.Move(fullPath, backupDirectory, true);
					}
					fi.MoveTo(fullPath, true);
				}
				current++;
				Debug.Assert(current / (float)numOfFiles <= 1);
				progressHandler?.Report(current / (float)numOfFiles);
			}

			foreach (DirectoryInfo subDir in source.GetDirectories())
			{
				var nextSubDir = destination.CreateSubdirectory(subDir.Name);
				var nextBackUpPath = backUpPath?.CreateSubdirectory(subDir.Name);
				CopyAll(subDir, nextSubDir, nextBackUpPath, current, numOfFiles, progressHandler);
			}
		}

		public static void CopyAll(string source, string target, string? backUpPath, IProgress<float>? progressHandler)
		{
			var sourceDi = new DirectoryInfo(source);
			var targetDi = new DirectoryInfo(target);
			var backUpDi = string.IsNullOrEmpty(backUpPath) ? null : new DirectoryInfo(backUpPath);
			CopyAll(sourceDi, targetDi, backUpDi, progressHandler);
		}

		private static bool IsSymbolicLink(string filePath)
		{
			if (File.Exists(filePath))
			{
				FileInfo destFi = new(filePath);
				return destFi.Attributes.HasFlag(FileAttributes.ReparsePoint);
			}

			return false;
		}
	}
}
