using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace GersangGameManager.Service
{
	internal static class FileService
	{
		public static void CopyAll(DirectoryInfo source, DirectoryInfo destination, IProgress<float> progressHandler)
		{
			Directory.CreateDirectory(destination.FullName);

			int numOfFiles = Directory.EnumerateFiles(source.FullName, "*.*", SearchOption.AllDirectories).Count();
			CopyAll(source, destination, 0, numOfFiles, progressHandler);
			progressHandler.Report(1);
		}

		private static void CopyAll(DirectoryInfo source, DirectoryInfo destination, int current, int numOfFiles, IProgress<float> progressHandler)
		{
			foreach (FileInfo fi in source.GetFiles())
			{
				fi.CopyTo(Path.Combine(destination.FullName, fi.Name), true);
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
				CopyAll(subDir, nextSubDir, current, numOfFiles, progressHandler);
			}
		}

		public static void CopyAll(string source, string target, IProgress<float> progressHandler)
		{
			var sourceDi = new DirectoryInfo(source);
			var targetDi = new DirectoryInfo(target);
			CopyAll(sourceDi, targetDi, progressHandler);
		}
	}
}
