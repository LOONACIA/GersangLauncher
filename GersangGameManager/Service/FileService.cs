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
					if (backUpPath != null)
					{
						if (File.Exists(fullPath))
						{
							var backUpFile = Path.Combine(backUpPath.FullName, fi.Name);
							var dir = Path.GetDirectoryName(backUpFile);
							if (dir is not null && !Directory.Exists(dir))
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
					progressHandler?.Report(current / (float)numOfFiles);
				}
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
				var destFi = new FileInfo(filePath);
				return destFi.Attributes.HasFlag(FileAttributes.ReparsePoint);
			}
			else
				return false;
		}
	}
}
