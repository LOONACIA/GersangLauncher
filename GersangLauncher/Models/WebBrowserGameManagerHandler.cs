﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GersangGameManager;
using GersangGameManager.Handler;
using GersangLauncher.Models.GameManager.Extensions;

namespace GersangLauncher.Models
{
	public class WebBrowserGameManagerHandler : GameManagerHandler
	{
		private WebBrowser _webBrowser;
		private const string ContentType = "Content-Type: application/x-www-form-urlencoded";
		TaskCompletionSource<bool> _tcs = null;

		[DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool InternetSetCookie(string url, string name, string data);

		private List<KeyValuePair<string, string>> _queries;

		protected override void Configure(ClientInfo clientInfo)
		{
			base.Configure(clientInfo);
			_tcs = new TaskCompletionSource<bool>();
			if (_webBrowser != null)
			{
				_webBrowser.Dispose();
				_webBrowser = null;
			}
			_webBrowser = new WebBrowser();
			_webBrowser.ScrollBarsEnabled = false;
			_webBrowser.ScriptErrorsSuppressed = true;
			_webBrowser.Navigated += (s, e) =>
			{
				if (_tcs.Task.IsCompleted)
					return;
				_tcs.SetResult(true);
			};
			_queries = new List<KeyValuePair<string, string>>();

			InternetSetCookie(base.BaseAddress, "passwdChange", "checked");
			InternetSetCookie(base.BaseAddress, "EventNotToday", "Y");
		}

		protected override async Task<LogInResult> LogIn(DecryptDelegate decryptor)
		{
			var password = decryptor(_clientInfo.EncryptedPassword);
			_queries.Clear();
			_queries.Add(ParamID, _clientInfo.ID);
			_queries.Add(ParamPW, password);
			var postData = _queries.BuildPostData();
			string url = base.BaseAddress + LogInUrl;

			_webBrowser.Navigate(url, string.Empty, postData, ContentType);
			await _tcs.Task;

			if (_webBrowser.DocumentText.ToLower().Contains("otp"))
				return new LogInResult(LogInResultType.RequireOtp);
			else
				return new LogInResult(LogInResultType.Fail);
		}

		protected override async Task<OtpResult> InputOtp(string otp)
		{
			if (_webBrowser.ReadyState != WebBrowserReadyState.Complete)
				await _tcs.Task;

			_queries.Add(ParamOTP, otp);
			var uri = new System.Uri(base.BaseAddress);
			var returnUrl = uri.Host + uri.PathAndQuery + IndexUrl;
			_queries.Add(ParamReturnUrl, returnUrl);
			var postData = _queries.BuildPostData();
			string url = base.BaseAddress + OtpProcUrl;

			_webBrowser.Navigate(url, string.Empty, postData, ContentType);
			await _tcs.Task;

			if (_webBrowser.Url.OriginalString.ToLower().Contains("otp"))
				return new OtpResult(OtpResultType.Fail);
			else
			{
				_queries.Clear();
				return new OtpResult(OtpResultType.Success);
			}
		}

		protected override async Task GameStart()
		{
			// js injection
			HtmlDocument _doc = _webBrowser.Document;
			HtmlElement head = _doc.GetElementsByTagName("head")[0];
			HtmlElement script = _doc.CreateElement("script");
			var serverType = _clientInfo.ServerType switch
			{
				ServerType.Main => "main",
				ServerType.Test => "test",
				_ => "main"
			};
			script.SetAttribute("text", $"function openStarter() {{ self.location.href='Gersang:'; \n startRetry = setTimeout(\"socketStart('{serverType}')\", 2000); }}");
			head.AppendChild(script);
			_webBrowser.Document.InvokeScript("openStarter");
		}

		protected override async Task<bool> CheckLogIn()
		{
			throw new System.NotImplementedException();
		}

		protected override async Task LogOut()
		{
			_webBrowser.Navigate(base.BaseAddress + LogOutUrl);
		}

		protected override Task<bool> GetSearchReward()
		{
			throw new System.NotImplementedException();
		}
	}
}

namespace GersangLauncher.Models.GameManager.Extensions
{
	using System.Linq;
	internal static class PostDataExt
	{
		internal static void Add(this List<KeyValuePair<string, string>> queries, string key, string value)
		{
			Debug.Assert(!queries.Any(x => x.Key == key));

			queries.Add(new KeyValuePair<string, string>(key, value));
		}

		internal static byte[] BuildPostData(this List<KeyValuePair<string, string>> queries)
		{
			var ret = queries[0].Key + '=' + queries[0].Value;
			for (var i = 1; i < queries.Count; i++)
				ret += $"&{queries[i].Key}={queries[i].Value}";

			return Encoding.UTF8.GetBytes(ret);
		}
	}
}