using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GersangLauncher.Models.Service
{
	internal class WebSocketService
	{
		private ClientWebSocket _ws = new ClientWebSocket();
		private CancellationTokenSource _cts = new CancellationTokenSource();
		private int _chunckSize = 64 * 1024;
		public bool IsDisConnected { get; private set; }

		public event EventHandler OnOpen;
		public event EventHandler<WebSocketOnMessageArgs> OnMessage;

		public async Task ConnectAsync(string url)
		{
			if (_ws is not null)
			{
				if (_ws.State == WebSocketState.Open)
					return; // Already Connected.
				else
					_ws.Dispose();
			}
			_ws = new ClientWebSocket();
			_cts = new CancellationTokenSource();
			IsDisConnected = false;
			var uri = new Uri(url);
			await _ws.ConnectAsync(uri, _cts.Token);
			await Task.Factory.StartNew(Receive, _cts.Token);
		}

		public async Task DisconnectAsync()
		{
			if (_ws is null)
				return; // Already Disconnected
			if (_ws.State == WebSocketState.Open)
			{
				_cts.Cancel();
				await _ws.CloseOutputAsync(WebSocketCloseStatus.Empty, "", CancellationToken.None);
				await _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
			}
			IsDisConnected = true;
			_ws.Dispose();
			_cts.Dispose();
		}

		public async Task SendAsync(string data)
		{
			var encoded = Encoding.UTF8.GetBytes(data);
			await _ws.SendAsync(new ArraySegment<byte>(encoded), WebSocketMessageType.Text, true, _cts.Token);
		}

		private async Task Receive()
		{
			OnOpen?.Invoke(this, EventArgs.Empty);
			byte[] buffer = new byte[_chunckSize];
			while (!_cts.Token.IsCancellationRequested && (_ws.State == WebSocketState.Open || _ws.State == WebSocketState.CloseSent))
			{
				var result = await _ws.ReceiveAsync(new ArraySegment<byte>(buffer), _cts.Token);

				if (result.Count > 0)
				{
					OnReceive(Encoding.UTF8.GetString(buffer, 0, result.Count));
				}
				if (result.MessageType == WebSocketMessageType.Close)
				{
					await DisconnectAsync();
					break;
				}
			}
		}

		private void OnReceive(string message)
		{
			WebSocketOnMessageArgs args = new WebSocketOnMessageArgs(message);
			OnMessage?.Invoke(this, args);
		}
	}

	public class WebSocketOnMessageArgs : EventArgs
	{
		public string Message { get; init; }

		public WebSocketOnMessageArgs(string message)
		{
			Message = message;
		}
	}
}
