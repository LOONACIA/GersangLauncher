using System;
using System.Threading.Tasks;

namespace GersangGameManager.Handler
{
	public interface IGameManagerHandler
	{
		void Configure(ClientInfo clientInfo);

		void ChangeClientPath(string clientPath);

		Task<LogInResult> LogIn(DecryptDelegate decryptor);

		Task<OtpResult> InputOtp(string otp);

		Task<bool> CheckLogIn();

		Task GameStart();

		[Obsolete]
		Task<bool> GetSearchReward();

		Task LogOut();
	}
}
