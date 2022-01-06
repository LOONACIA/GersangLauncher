using GersangGameManager.Service;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GersangGameManager.Handler
{
	public class HttpClientGameManagerHandler : GameManagerHandler
	{
		private HttpService _httpService;
		private HttpContentBuilder _builder;

		private WebSocketService _websocketService;
		private const string _wsUrl = "ws://127.0.0.1:1818";

		private string _cmdStr;

		protected override void Configure(ClientInfo clientInfo)
		{
			base.Configure(clientInfo);
			_builder = new HttpContentBuilder();
			_httpService = new HttpService();
			_httpService.BaseAddress = base.BaseAddress;
			_websocketService = new WebSocketService();
			_websocketService.OnOpen += _websocketService_OnOpen;
		}

		protected override async Task<LogInResult> LogIn(DecryptDelegate decryptor)
		{
			_builder.Clear();
			var password = decryptor(_clientInfo.EncryptedPassword);
			_builder.Add(ParamID, _clientInfo.ID);
			_builder.Add(ParamPW, password);
			var content = _builder.Build(ContentType.FormData);

			var body = await _httpService.PostAsync(LogInUrl, content);

			var result = body.ToLower();
			if (result.Contains("otp"))
				return new LogInResult(LogInResultType.RequireOtp);
			else if (result.Contains("alert"))
			{
				var errorMessage = GetAlert(result);
				return new LogInResult(LogInResultType.Fail, errorMessage);
			}
			else
				return new LogInResult(LogInResultType.Success);
		}

		protected override async Task<OtpResult> InputOtp(string otp)
		{
			_builder.Add(ParamOTP, otp);
			var content = _builder.Build(ContentType.FormData);

			var body = await _httpService.PostAsync(OtpUrl, content);

			var result = body.ToLower();
			var errorMessage = GetAlert(result);

			if (!string.IsNullOrEmpty(errorMessage))
				return new OtpResult(OtpResultType.Fail, errorMessage);
			else
			{
				_builder.Clear();
				return new OtpResult(OtpResultType.Success);
			}
		}

		protected override async Task<bool> CheckLogIn()
		{
			await Task.Yield();
			return !string.IsNullOrEmpty(_httpService.GetCookie("memberID"));
		}

		protected override async Task GameStart()
		{
			var body = await _httpService.GetAsync(IndexUrl);
			var serverType = _clientInfo.ServerType switch
			{
				ServerType.Main => "main",
				ServerType.Test => "test",
				_ => "main"
			};
			_cmdStr = serverType + '\t' + GetCmdStr(body);

			await ExecuteStarter();
		}

		protected override async Task LogOut()
		{
			await _httpService.GetAsync(LogOutUrl);
		}

		protected override async Task<bool> GetSearchReward()
		{
			var body = await _httpService.GetAsync(EventUrl);

			var eventUrls = GetEventUrls(body);
			string target = string.Empty;
			string lastSegment = string.Empty;
			foreach (var evtUrl in eventUrls)
			{
				var response = await _httpService.GetAsync(evtUrl);
				if (!response.Contains("거상 페이지 오류"))
				{
					var uri = new Uri(evtUrl);
					var segments = uri.PathAndQuery;
					lastSegment = uri.Segments[^1];
					target = segments.Replace(lastSegment, string.Empty);
					break;
				}
			}

			if (target.StartsWith('/'))
				target = target[1..];

			// Get referrer cookie from BaseAddress/main.gs
			await GetReferrerCookie();

			bool ret = false;
			for (int i = 1; i < 5; ++i)
			{
				_builder.Clear();
				_builder.Add("EventIdx", i.ToString());
				var result = GetAlert(await _httpService.PostAsync(target + "event_Search_UseProc.gs", _builder.Build(ContentType.FormData)));
				Debug.WriteLine(result);
				ret |= result.Contains("지급");
			}
			return ret;
		}

		private async Task ExecuteStarter()
		{
			if (_websocketService.IsDisConnected)
			{
				return;
			}
			else if (_websocketService is null)
				return;

			string path = string.Empty;
			using (RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey("Gersang\\shell\\open\\command"))
			{
				if (registryKey != null)
				{
					path = registryKey.GetValue("", true).ToString();
				}
			}
			if (string.IsNullOrEmpty(path))
			{
				throw new GameManagerException("거상 게임 스타터 설치 후 다시 실행해 주세요.");
			}

			Process process = new Process();
			Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			process.StartInfo.FileName = path;
			process.StartInfo.UseShellExecute = true;

			process.Start();

			await _websocketService.ConnectAsync(_wsUrl);
		}

		private async void _websocketService_OnOpen(object sender, EventArgs e)
		{
			// Send Start Command to Game Starter
			if (_websocketService is not null)
			{
				await _websocketService.SendAsync(_cmdStr);
				Debug.WriteLine(_cmdStr);
			}
		}

		private string GetCmdStr(string body)
		{
			body = body.Replace("\r\n", "");
			var regex_Find_CmdStr = new System.Text.RegularExpressions.Regex(@"function\ssocketStart\(.*?\)\s\{.*?this.send\(.*?\\t(.*?)\'\).*?\}");
			return System.Text.RegularExpressions.Regex.Unescape(regex_Find_CmdStr.Match(body).Groups[1].Value);
		}

		private string GetAlert(string body)
		{
			var regex_Find_Error = new System.Text.RegularExpressions.Regex(@"alert\((.*?)\)");
			return regex_Find_Error.Match(body).Groups[1].Value;
		}

		private async Task GetReferrerCookie()
		{
			_httpService.SetReferrer(ReferrerUrl);
			await _httpService.GetAsync(MainUrl);
		}

		private string[] GetEventUrls(string body) => FindLink(body).Where(x => x.ToLower().Contains("attendance")).ToArray();

		private string[] FindLink(string body)
		{
			var regex_Fine_HTML_Tag = new System.Text.RegularExpressions.Regex(@"<(?<Tag_Name>(a)|img)\b[^>]*?\b(?<URL_Type>(?(1)href|src))\s*=\s*(?:""(?<URL>(?:\\""|[^""])*)""|'(?<URL>(?:\\'|[^'])*)')");
			var matches = regex_Fine_HTML_Tag.Matches(body);

			var ret = new List<string>();

			matches.ToList()
				.ForEach(match =>
				{
					ret.Add(match.Groups["URL"].Value);
				});

			return ret.ToArray();
		}
	}
}
