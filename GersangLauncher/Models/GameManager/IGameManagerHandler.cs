using System.Threading.Tasks;

namespace GersangLauncher.Models.GameManager
{
	public interface IGameManagerHandler
	{
		void Configure(AccountInfo gameInfo);
		Task<(LogInResult result, string message)> LogIn(DecryptDelegate decryptor);
		Task<(OtpResult result, string message)> InputOtp(string otp);
		Task GameStart();

		Task LogOut();
	}
}
