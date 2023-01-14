using System;
using System.Buffers;
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

			var buffer = ArrayPool<byte>.Shared.Rent(81920);
			try
			{
				long totalBytesRead = 0;
				int bytesRead;
				int i = 0;
				while ((bytesRead = await source.ReadAsync(new Memory<byte>(buffer), cancellationToken).ConfigureAwait(false)) != 0)
				{
					await destination.WriteAsync(new ReadOnlyMemory<byte>(buffer, 0, bytesRead), cancellationToken).ConfigureAwait(false);
					totalBytesRead += bytesRead;
				}
				progress?.Report(totalBytesRead);
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(buffer);
			}
		}
	}
}