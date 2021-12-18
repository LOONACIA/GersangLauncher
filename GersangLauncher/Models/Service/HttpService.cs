using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace GersangLauncher.Models.Service
{
	public class HttpService
	{
		private HttpClient _client;
		private HttpClientHandler _handler;
		private CookieContainer _cookieContainer;
		public string BaseAddress
		{
			get => _client.BaseAddress.OriginalString;
			set => _client.BaseAddress = new Uri(value);
		}

		public HttpService()
		{
			_cookieContainer = new CookieContainer();
			_handler = new HttpClientHandler
			{
				CookieContainer = _cookieContainer,
				AllowAutoRedirect = true,
			};
			_handler.ServerCertificateCustomValidationCallback += (sender, certificate, chain, errors) => true;
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
			_client = new HttpClient(_handler);
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
	}
}
