using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace GersangGameManager.Service.Extensions
{
	internal static class StreamExtensions
	{
		public static async Task CopyToAsync(this Stream source, Stream destination, IProgress<float> progress = null, CancellationToken cancellationToken = default)
		{
			if (source is null)
				throw new ArgumentNullException(nameof(source));
			if (destination is null)
				throw new ArgumentNullException(nameof(destination));

			var buffer = new byte[81920];
			long totalBytesRead = 0;
			int bytesRead;
			int i = 0;
			while ((bytesRead = await source.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false)) != 0)
			{
				await destination.WriteAsync(buffer, 0, bytesRead, cancellationToken).ConfigureAwait(false);
				totalBytesRead += bytesRead;
				if (i % 512 == 0)
					progress?.Report(totalBytesRead);
			}
			progress?.Report(totalBytesRead);
		}
	}
}