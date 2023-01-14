using System;
using System.Buffers;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GersangGameManager.Service
{
	public delegate void OpenEventHandler();

	public delegate void ReceiveEventHandler(string message);

	public delegate void CloseEventHandler(Exception ex);

	public class WebSocketService : IDisposable
	{
		private bool _isDisposed;

		private SemaphoreSlim _lock = new(1, 1);

		private ClientWebSocket? _webSocketClient;

		private CancellationTokenSource? _cancelTokenSource = new();

		public event OpenEventHandler? OnOpen;

		public event ReceiveEventHandler? OnMessage;

		public event CloseEventHandler? Closed;

		public async Task ConnectAsync(string url)
		{
			await _lock.WaitAsync().ConfigureAwait(false);

			try
			{
				await ConnectAsyncInternal(url);
			}
			finally
			{
				_lock.Release();
			}
		}

		public async Task DisconnectAsync()
		{
			await _lock.WaitAsync().ConfigureAwait(false);

			try
			{
				await DisconnectAsyncInternal().ConfigureAwait(false);
			}
			finally
			{
				_lock.Release();
			}
		}

		public async Task SendAsync(string data)
		{
			if (_cancelTokenSource is null)
			{
				return;
			}

			try
			{
				await _lock.WaitAsync(_cancelTokenSource.Token).ConfigureAwait(false);
			}
			catch (TaskCanceledException)
			{
				return;
			}

			try
			{
				if (_webSocketClient is null)
				{
					return;
				}
				var encoded = Encoding.UTF8.GetBytes(data);
				await _webSocketClient.SendAsync(new ArraySegment<byte>(encoded), WebSocketMessageType.Text, true, _cancelTokenSource?.Token ?? default).ConfigureAwait(false);
			}
			finally
			{
				_lock.Release();
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private async Task ConnectAsyncInternal(string url)
		{
			await DisconnectAsyncInternal().ConfigureAwait(false);

			_webSocketClient?.Dispose();
			_cancelTokenSource?.Dispose();

			_webSocketClient = new ClientWebSocket();
			_cancelTokenSource = new CancellationTokenSource();

			await _webSocketClient.ConnectAsync(new Uri(url), _cancelTokenSource.Token).ConfigureAwait(false);

			await Task.Factory.StartNew(ReceiveAsync, _cancelTokenSource.Token).ConfigureAwait(false);
		}

		private async Task ReceiveAsync()
		{
			const ushort bufferSize = 0x40 * 512;

			OnOpen?.Invoke();
			byte[] buffer = ArrayPool<byte>.Shared.Rent(bufferSize);

			try
			{
				var memory = new Memory<byte>(buffer);
				while (_webSocketClient?.State == WebSocketState.Open && (!_cancelTokenSource?.Token.IsCancellationRequested ?? default(CancellationToken).IsCancellationRequested))
				{
					var result = await _webSocketClient!.ReceiveAsync(memory, _cancelTokenSource?.Token ?? default).ConfigureAwait(false);

					if (result.Count > 0 && !memory.IsEmpty)
					{
						var message = Encoding.UTF8.GetString(memory.Span[..result.Count]);
						OnMessage?.Invoke(message);
					}
					if (result.MessageType == WebSocketMessageType.Close)
					{
						throw new WebSocketClosedException(_webSocketClient.CloseStatusDescription);
					}
				}
			}
			catch (Exception ex)
			{
				await CloseAsync(ex);
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(buffer);
			}
		}

		private async Task CloseAsync(Exception ex)
		{
			await _lock.WaitAsync().ConfigureAwait(false);

			try
			{
				await DisconnectAsyncInternal().ConfigureAwait(false);
			}
			finally
			{
				_lock.Release();
			}

			Closed?.Invoke(ex);
		}

		private async Task DisconnectAsyncInternal()
		{
			if (_webSocketClient is null)
			{
				// Already disconnected.
				return;
			}

			try
			{
				_cancelTokenSource?.Cancel();
				await _webSocketClient.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None).ConfigureAwait(false);
			}
			catch { }
		}

		private void Dispose(bool isDisposing)
		{
			if (_isDisposed)
			{
				return;
			}

			if (isDisposing)
			{
				_webSocketClient?.Dispose();
				_cancelTokenSource?.Dispose();
				_lock?.Dispose();
			}

			_isDisposed = true;
		}
	}
}
