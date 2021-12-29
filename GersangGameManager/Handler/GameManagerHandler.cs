﻿using System;
using System.Threading.Tasks;

namespace GersangGameManager.Handler
{
	public abstract class GameManagerHandler : IGameManagerHandler
	{
		protected ClientInfo _clientInfo;
		public virtual string BaseAddress { get; set; } = "https://www.gersang.co.kr/";
		protected const string LogInUrl = "member/loginProc.gs";
		protected const string LogOutUrl = "member/logoutProc.gs";
		protected const string OtpUrl = "member/otpProc.gs";
		protected const string IndexUrl = "main/index.gs?";
		protected const string MainUrl = "main.gs";
		protected const string EventUrl = "news/event.gs";

		protected const string ParamID = "GSuserID";
		protected const string ParamPW = "GSuserPW";
		protected const string ParamOTP = "GSotpNo";
		protected const string ParamReturnUrl = "returnUrl";

		protected const string ReferrerUrl = "https://search.naver.com/search.naver?where=nexearch&sm=top_hty&fbm=0&ie=utf8&query=거상";

		public GameManagerHandler()
		{
			_clientInfo = new ClientInfo();
		}

		void IGameManagerHandler.Configure(ClientInfo clientInfo)
		{
			Configure(clientInfo);
		}

		protected virtual void Configure(ClientInfo clientInfo)
		{
			if (clientInfo is null)
				throw new ArgumentException(nameof(ClientInfo));
			_clientInfo = clientInfo;
		}

		Task<LogInResult> IGameManagerHandler.LogIn(DecryptDelegate decryptor)
		{
			return LogIn(decryptor);
		}
		protected abstract Task<LogInResult> LogIn(DecryptDelegate decryptor);

		Task<OtpResult> IGameManagerHandler.InputOtp(string otp)
		{
			return InputOtp(otp);
		}
		protected abstract Task<OtpResult> InputOtp(string otp);

		Task<bool> IGameManagerHandler.CheckLogIn()
		{
			return CheckLogIn();
		}
		protected abstract Task<bool> CheckLogIn();

		Task IGameManagerHandler.GameStart()
		{
			return GameStart();
		}
		protected abstract Task GameStart();

		Task IGameManagerHandler.LogOut()
		{
			return LogOut();
		}
		protected abstract Task LogOut();

		Task<bool> IGameManagerHandler.GetSearchReward()
		{
			return GetSearchReward();
		}
		protected abstract Task<bool> GetSearchReward();
	}
}