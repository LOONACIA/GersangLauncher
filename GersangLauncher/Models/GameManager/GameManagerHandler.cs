using Microsoft.Win32;
using System;
using System.Threading.Tasks;

namespace GersangLauncher.Models.GameManager
{
	public abstract class GameManagerHandler : IGameManagerHandler
	{
		protected AccountInfo _accountInfo;
		public virtual string BaseAddress { get; set; } = "https://www.gersang.co.kr/";
		public const string LogInUrl = "member/loginProc.gs";
		public const string LogOutUrl = "member/logoutProc.gs";
		public const string OtpUrl = "member/otpProc.gs";
		public const string IndexUrl = "main/index.gs?";
		public const string MainUrl = "main.gs";
		public const string EventUrl = "news/event.gs";

		public const string ParamID = "GSuserID";
		public const string ParamPW = "GSuserPW";
		public const string ParamOTP = "GSotpNo";
		public const string ParamReturnUrl = "returnUrl";

		public const string ReferrerUrl = "https://search.naver.com/search.naver?where=nexearch&sm=top_hty&fbm=0&ie=utf8&query=거상";

		public virtual void Configure(AccountInfo accountInfo)
		{
			if (accountInfo is null)
				throw new ArgumentException(nameof(AccountInfo));
			_accountInfo = accountInfo;
		}

		public abstract Task<(LogInResult result, string message)> LogIn(DecryptDelegate decryptor);

		public abstract Task<(OtpResult result, string message)> InputOtp(string otp);

		public abstract Task GameStart();

		public abstract Task LogOut();
	}
}
