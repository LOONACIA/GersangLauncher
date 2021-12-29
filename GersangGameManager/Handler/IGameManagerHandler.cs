using System.Threading.Tasks;

namespace GersangGameManager.Handler
{
	public interface IGameManagerHandler
	{
		void Configure(ClientInfo clientInfo);
		Task<LogInResult> LogIn(DecryptDelegate decryptor);
		Task<OtpResult> InputOtp(string otp);
		Task<bool> CheckLogIn();
		Task GameStart();
		Task<bool> GetSearchReward();
		Task LogOut();
	}
}
