using Microsoft.Win32;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GersangLauncher.Models.GameManager
{
	public class GameManager
	{
		private IGameManagerHandler _handler;
		private bool _isLoginSucceed;

		public GameManager(IGameManagerHandler handler)
		{
			_handler = handler;
		}

		public async Task<bool> LogIn(AccountInfo accountInfo, DecryptDelegate decryptor)
		{
			_isLoginSucceed = false;
			_handler.Configure(accountInfo);
			ChangeInstallPath(accountInfo.Path);
			var loginResult = await _handler.LogIn(decryptor);
			switch (loginResult.result)
			{
				case LogInResult.Success:
					_isLoginSucceed = true;
					break;
				case LogInResult.RequireOtp:
					FormInputText form = new();
					form.Text = "OTP 입력";
					bool ret;
					do
					{
						var result = form.ShowDialog();
						if (result != DialogResult.OK)
							break;
						_isLoginSucceed = await InputOTP(form.InputValue);
					} while (!_isLoginSucceed);
					form.Dispose();
					break;
				case LogInResult.Fail:
					if (!string.IsNullOrEmpty(loginResult.message))
						MessageBox.Show(loginResult.message);
					break;
			}
			return _isLoginSucceed;
		}

		public async Task<bool> InputOTP(string otp)
		{
			var otpResult = await _handler.InputOtp(otp);
			if (otpResult.result == OtpResult.Fail && !string.IsNullOrEmpty(otpResult.message))
				MessageBox.Show(otpResult.message);

			return otpResult.result == OtpResult.Success;
		}

		public async Task GameStart()
		{
			if (!_isLoginSucceed)
			{
				MessageBox.Show("로그인 상태를 확인하세요.");
				return;
			}

			await _handler.GameStart();

			_handler.LogOut();
		}

		public async Task<bool> GetSearchReward()
		{
			if (!_isLoginSucceed)
			{
				MessageBox.Show("로그인 상태를 확인하세요.");
				return false;
			}

			bool ret = false;
			if (_handler is HttpClientGameManagerHandler)
				ret = await (_handler as HttpClientGameManagerHandler).GetSearchReward();

			return ret;
		}

		public void ChangeInstallPath(string newInstallPath)
		{
			using (RegistryKey? registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\JOYON\\Gersang\\Korean", RegistryKeyPermissionCheck.ReadWriteSubTree))
			{
				if (registryKey != null)
				{
					registryKey.SetValue("InstallPath", newInstallPath);
					registryKey.Close();
				}
			}
		}
	}
}
