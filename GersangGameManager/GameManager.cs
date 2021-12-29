using GersangGameManager.Handler;
using GersangGameManager.PatchManager;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GersangGameManager
{
	public class GameManager
	{
		private IGameManagerHandler _handler;
		private bool _isLoginSucceed;
		private Dictionary<string, bool> _runningClientList;
		private string _currentLogInAccount;

		private Patcher _patcher;

		public GameManager(IGameManagerHandler handler)
		{
			_handler = handler;
			_patcher = new Patcher();
			_runningClientList = new Dictionary<string, bool>();
		}

		public async Task<LogInResult> LogIn(ClientInfo accountInfo, DecryptDelegate decryptor, bool ignoreAleadyStarted)
		{
			_isLoginSucceed = false;
			_handler.Configure(accountInfo);
			_ = _runningClientList.TryGetValue(accountInfo.ID, out bool isStartedClient);
			if (isStartedClient)
			{
				return new LogInResult(LogInResultType.StartedClient);
			}
			ChangeInstallPath(accountInfo.ClientPath);

			int count = 3;
			LogInResult loginResult;
			while (true)
			{
				try
				{
					loginResult = await _handler.LogIn(decryptor);
					break;
				}
				catch (System.Net.Http.HttpRequestException)
				{
					count--;
				}
				if (count <= 0)
				{
					return new LogInResult(LogInResultType.Fail, "통신 오류 발생. 다시 시도해 주세요.");
				}
			}

			if (loginResult.Type == LogInResultType.Success)
			{
				_currentLogInAccount = accountInfo.ID;
				_runningClientList[_currentLogInAccount] = false;
			}

			return loginResult;
		}

		public async Task<OtpResult> InputOTP(string otp)
		{
			var otpResult = await _handler.InputOtp(otp);

			return otpResult;
		}

		public async Task GameStart()
		{
			_isLoginSucceed = await _handler.CheckLogIn();

			await _handler.GameStart();
			_runningClientList[_currentLogInAccount] = true;

			_currentLogInAccount = string.Empty;
			_handler.LogOut();
		}

		public async Task<bool?> GetSearchReward()
		{
			_isLoginSucceed = await _handler.CheckLogIn();

			if (!_isLoginSucceed)
				return null;

			bool ret = false;
			ret = await _handler.GetSearchReward();

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

		public async Task<int> GetCurrentVersion(ServerType serverType)
		{
			return await _patcher.GetCurrentVersion(serverType);
		}

		public int CheckLocalVersion(string installPath)
		{
			return _patcher.CheckLocalVersion(installPath);
		}

		public async Task CheckUpdate(string installPath, ServerType serverType)
		{
			_patcher = new Patcher();
			_patcher.Configure(installPath, serverType);
			await _patcher.CheckNeedToUpdate();
		}

		public async Task<string[]> GetPatchNote(ServerType serverType, IProgress<UpdateProgressEventArgs> progressHandler = null)
		{
			_patcher.Configure(string.Empty, serverType);
			return await _patcher.GetPatchNote(progressHandler);
		}

		public async Task<bool> Update(string installPath, ServerType serverType, IProgress<UpdateProgressEventArgs> progressHandler = null, CancellationToken cancellationToken = default)
		{
			if (_patcher is null)
			{
				_patcher = new Patcher();
			}

			_patcher.Configure(installPath, serverType);
			return await _patcher.Update(progressHandler, cancellationToken);
		}
	}
}
