using GersangGameManager.Service;
using HtmlAgilityPack;
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
#nullable disable
		private HttpService _httpService;
#nullable restore
		private HttpContentBuilder _builder = new();

		private WebSocketService? _websocketService;
		private const string _wsUrl = "ws://127.0.0.1:1818";

		private string? _cmdStr;

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
			if (string.IsNullOrEmpty(_clientInfo.ID) || string.IsNullOrEmpty(_clientInfo.EncryptedPassword))
				throw new InvalidOperationException("Check ID or Password");

			_builder.Clear();
			var password = decryptor(_clientInfo.EncryptedPassword);
			_builder.Add(ParamID, _clientInfo.ID);
			_builder.Add(ParamPW, password);
			var content = _builder.Build(ContentType.FormData);

			var body = await _httpService.PostAsync(LogInUrl, content).ConfigureAwait(false);

			if (CheckRequiredOtp(body))
				return new LogInResult(LogInResultType.RequireOtp);

			var errorMessage = GetAlert(body);
			if (!string.IsNullOrEmpty(errorMessage))
			{
				return new LogInResult(LogInResultType.Fail, errorMessage);
			}
			if (CheckifInCert(body))
			{
				return new LogInResult(LogInResultType.Fail, "본인인증이 필요합니다. 홈페이지에서 인증 후 재로그인하시기 바랍니다.");
			}

			return new LogInResult(LogInResultType.Success, null);
		}

		protected override async Task<OtpResult> InputOtp(string otp)
		{
			_builder.Add(ParamOTP, otp);
			var content = _builder.Build(ContentType.FormData);

			var body = await _httpService.PostAsync(OtpUrl, content).ConfigureAwait(false);
			_builder.Clear();
			_builder.AddRange(GetLoginParameter(body));
			_builder.Add(ParamOTP, otp);
			content = _builder.Build(ContentType.FormData);

			body = await _httpService.PostAsync(OtpProcUrl, content).ConfigureAwait(false);
			_builder.Clear();

			var errorMessage = GetAlert(body);
			if (!string.IsNullOrEmpty(errorMessage))
				return new OtpResult(OtpResultType.Fail, errorMessage);

			bool isloginSucceeded = await CheckLogIn().ConfigureAwait(false);
			return isloginSucceeded
				? new OtpResult(OtpResultType.Success, null)
				: new OtpResult(OtpResultType.Error, "Undefined Error");
		}

		private Dictionary<string, string> GetLoginParameter(string htmlString)
		{
			var ret = new Dictionary<string, string>();

			var html = new HtmlDocument();
			html.LoadHtml(htmlString);

			var divNodes = html.DocumentNode.SelectNodes("//div[@class='cont_box']");
			foreach (var divNode in divNodes)
			{
				var inputNodes = divNode.SelectNodes("form/input[@id]");
				foreach (var inputNode in inputNodes)
				{
					ret.Add(inputNode.Attributes["name"].Value, inputNode.Attributes["value"].Value);
				}
			}

			return ret;
		}

		protected override async Task<bool> CheckLogIn()
		{
			if(_httpService is null)
			{
				_httpService = new HttpService();
				_httpService.BaseAddress = base.BaseAddress;
			}

			var body = await _httpService.GetAsync(IndexUrl).ConfigureAwait(false);

			var html = new HtmlDocument();
			html.LoadHtml(body);

			return html.DocumentNode.SelectSingleNode("//div[@class='user_name']") is not null;
		}

		protected override async Task GameStart()
		{
			var body = await _httpService.GetAsync(IndexUrl).ConfigureAwait(false);
			var serverType = _clientInfo.ServerType switch
			{
				ServerType.Main => "main",
				ServerType.Test => "test",
				_ => "main"
			};
			_cmdStr = serverType + '\t' + GetCmdStr(body);

			await ExecuteStarter();
		}

		protected override Task LogOut()
		{
			return _httpService.GetAsync(LogOutUrl);
		}

		protected override async Task<bool> GetSearchReward()
		{
			var body = await _httpService.GetAsync(EventUrl).ConfigureAwait(false);

			var eventUrls = GetEventUrls(body);
			string target = string.Empty;
			foreach (var evtUrl in eventUrls)
			{
				var response = await _httpService.GetAsync(evtUrl).ConfigureAwait(false);
				if (!response.Contains("거상 페이지 오류"))
				{
					var uri = new Uri(evtUrl);
					var segments = uri.PathAndQuery;
					string lastSegment = uri.Segments[^1];
					target = segments.Replace(lastSegment, string.Empty);
					break;
				}
			}

			if (target.StartsWith('/'))
				target = target[1..];

			// Get referrer cookie from BaseAddress/main.gs
			await GetReferrerCookie().ConfigureAwait(false);

			bool ret = false;
			for (int i = 1; i < 5; ++i)
			{
				_builder.Clear();
				_builder.Add("EventIdx", i.ToString());
				var result = GetAlert(await _httpService.PostAsync(target + "event_Search_UseProc.gs", _builder.Build(ContentType.FormData)).ConfigureAwait(false));
				Debug.WriteLine(result);
				ret |= result.Contains("지급");
			}
			return ret;
		}

		protected virtual bool CheckRequiredOtp(string responseBody)
		{
			var html = new HtmlDocument();
			html.LoadHtml(responseBody);

			return html.DocumentNode.SelectSingleNode("form[@method='post' and @action='otp.gs']") is not null;
		}

		protected virtual bool CheckifInCert(string responseBody)
		{
			var html = new HtmlDocument();
			html.LoadHtml(responseBody);

			return html.DocumentNode.SelectSingleNode("form[@method='post' and @action='loginCertUp.gs']") is not null;
		}

		private async Task ExecuteStarter()
		{
			if (!OperatingSystem.IsWindows())
				throw new NotSupportedException("Check if OS is MS Windows");

			using var registryKey = Registry.ClassesRoot.OpenSubKey("Gersang\\shell\\open\\command");
			if (registryKey is null)
				throw new GameManagerException("스타터가 설치되어 있지 않습니다.");

			string path = registryKey.GetValue("", true)?.ToString() ?? throw new GameManagerException("스타터에서 설치 경로를 추출할 수 없습니다.");

			Process process = new Process();
			Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			process.StartInfo.FileName = path;
			process.StartInfo.UseShellExecute = true;

			process.Start();

			await _websocketService!.ConnectAsync(_wsUrl).ConfigureAwait(false);
		}

		private async void _websocketService_OnOpen()
		{
			if (string.IsNullOrEmpty(_cmdStr))
				return;

			// Send Start Command to Game Starter
			if (_websocketService is not null)
			{
				await _websocketService.SendAsync(_cmdStr).ConfigureAwait(false);
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
			var regex_Find_Error = new System.Text.RegularExpressions.Regex(@"alert\((.*?)\)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
			return regex_Find_Error.Match(body).Groups[1].Value;
		}

		private Task GetReferrerCookie()
		{
			_httpService.SetReferrer(ReferrerUrl);
			return _httpService.GetAsync(MainUrl);
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
