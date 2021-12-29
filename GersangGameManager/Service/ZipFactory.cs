using System.IO;
using System.IO.Compression;

namespace GersangGameManager.Service
{
	internal static class ZipFactory
	{
		public static bool ZipValidation(string zipFilePath)
		{
			try
			{
				using (var zipFile = ZipFile.OpenRead(zipFilePath))
				{
					var entries = zipFile.Entries;
					return true;
				}
			}
			catch (InvalidDataException)
			{
				return false;
			}
		}

		public static void Unzip(string zipFilePath, string targetFolder)
		{
			if (!Directory.Exists(targetFolder))
				Directory.CreateDirectory(targetFolder);
			using (var zip = ZipFile.OpenRead(zipFilePath))
			{
				foreach (var entry in zip.Entries)
				{
					var targetPath = Path.GetDirectoryName(Path.Combine(targetFolder, entry.FullName));
					if (string.IsNullOrEmpty(targetPath))
						continue;

					if (!Directory.Exists(targetPath))
					{
						Directory.CreateDirectory(targetPath);
					}
					entry.ExtractToFile(Path.Combine(targetFolder, entry.FullName));
				}
			}
		}
	}
}
