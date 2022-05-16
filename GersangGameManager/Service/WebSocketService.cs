using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GersangGameManager.Service
{
	internal class WebSocketService
	{
		private ClientWebSocket _ws = new ClientWebSocket();
		private CancellationTokenSource _cts = new CancellationTokenSource();
		private int _chunckSize = 64 * 1024;
		public bool IsDisConnected { get; private set; }

		public event EventHandler? OnOpen;
		public event EventHandler<WebSocketOnMessageArgs>? OnMessage;

		public async Task ConnectAsync(string url)
		{
			if (this._ws is not null)
			{
				if (this._ws.State == WebSocketState.Open)
					return; // Already Connected.
				else
					this._ws.Dispose();
			}
			this._ws = new ClientWebSocket();
			this._cts = new CancellationTokenSource();
			IsDisConnected = false;
			var uri = new Uri(url);
			await this._ws.ConnectAsync(uri, this._cts.Token);
			await Task.Factory.StartNew(Receive, this._cts.Token);
		}

		public async Task DisconnectAsync()
		{
			if (this._ws is null)
				return; // Already Disconnected
			if (this._ws.State == WebSocketState.Open)
			{
				this._cts.Cancel();
				await this._ws.CloseOutputAsync(WebSocketCloseStatus.Empty, "", CancellationToken.None);
				await this._ws.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
			}
			IsDisConnected = true;
			this._ws.Dispose();
			this._cts.Dispose();
		}

		public async Task SendAsync(string data)
		{
			var encoded = Encoding.UTF8.GetBytes(data);
			await this._ws.SendAsync(new ArraySegment<byte>(encoded), WebSocketMessageType.Text, true, this._cts.Token);
		}

		private async Task Receive()
		{
			OnOpen?.Invoke(this, EventArgs.Empty);
			byte[] buffer = new byte[this._chunckSize];
			while (!this._cts.Token.IsCancellationRequested && (this._ws.State == WebSocketState.Open || this._ws.State == WebSocketState.CloseSent))
			{
				var result = await this._ws.ReceiveAsync(new ArraySegment<byte>(buffer), this._cts.Token);

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
