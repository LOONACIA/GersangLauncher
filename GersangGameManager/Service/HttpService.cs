using GersangGameManager.Service.Extensions;
using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace GersangGameManager.Service
{
	internal class HttpService
	{
		private HttpClient _client;
		private HttpClientHandler _handler;
		private CookieContainer _cookieContainer;
		public string BaseAddress
		{
			get => _client.BaseAddress.OriginalString;
			set => _client.BaseAddress = new Uri(value);
		}

		public HttpService(HttpClientHandler handler = null)
		{
			_cookieContainer = new CookieContainer();
			if (handler is null)
			{
				_handler = new HttpClientHandler
				{
					CookieContainer = _cookieContainer,
					AllowAutoRedirect = true,
					MaxConnectionsPerServer = 6,
				};
			}
			else
			{
				_handler = handler;
			}
			_client = new HttpClient(_handler);
		}

		public void SetCookie(string url, string name, string value)
		{
			_cookieContainer.Add(new Uri(url), new Cookie(name, value));
		}
		public string GetCookie(string name)
		{
			var table = (Hashtable)_cookieContainer.GetType().InvokeMember("m_domainTable",
																			BindingFlags.NonPublic |
																			BindingFlags.GetField |
																			BindingFlags.Instance,
																			null,
																			_cookieContainer,
																			null);
			if (table is null)
				return string.Empty;

			foreach (string key in table.Keys)
			{
				foreach (Cookie cookie in _cookieContainer.GetCookies(_client.BaseAddress))
				{
					if (cookie.Name.ToLower() == name.ToLower())
						return cookie.Value;
				}
			}

			return string.Empty;
		}

		public void SetReferrer(string referer) => _client.DefaultRequestHeaders.Referrer = new Uri(HttpUtility.UrlDecode(referer));

		public async Task<string> GetAsync(string url)
		{
			var message = await _client.GetAsync(url);
			return await message.Content.ReadAsStringAsync();
		}

		public async Task<string> PostAsync(string url, HttpContent content)
		{
			var message = await _client.PostAsync(url, content);
			return await message.Content.ReadAsStringAsync();
		}

		public async Task<byte[]> GetAsBytesAsync(string url)
		{
			var message = await _client.GetAsync(url);
			return await message.Content.ReadAsByteArrayAsync();
		}

		public async Task DownloadAsync(string url, string fileName, IProgress<float> progress = null, CancellationToken cancellationToken = default)
		{
			using (var response = await _client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
			{
				var contentLength = response.Content.Headers.ContentLength;

				using (var download = await response.Content.ReadAsStreamAsync())
				{
					var dir = Path.GetDirectoryName(fileName);
					if (!Directory.Exists(dir))
						Directory.CreateDirectory(dir);
					using (var fs = new FileStream(fileName, FileMode.CreateNew))
					{
						if (progress is null || !contentLength.HasValue)
						{
							await download.CopyToAsync(fs);
							return;
						}
						var streamProgress = new Progress<float>(totalBytes => progress.Report((float)totalBytes / contentLength.Value));
						await download.CopyToAsync(fs, streamProgress, cancellationToken);
						progress.Report(1);
					}
				}
			}
		}
	}
}