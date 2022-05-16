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
		private string? _currentLogInAccount;

		private Patcher _patcher;

		public GameManager(IGameManagerHandler handler)
		{
			this._handler = handler;
			this._patcher = new Patcher();
			this._runningClientList = new Dictionary<string, bool>();
			this._patcher.ClearTempDirectory();
		}

		public async Task<LogInResult> LogIn(ClientInfo clientInfo, DecryptDelegate decryptor, bool ignoreAleadyStarted)
		{
			if (string.IsNullOrEmpty(clientInfo.ID) || string.IsNullOrEmpty(clientInfo.EncryptedPassword))
				throw new InvalidOperationException("Check ID or Password");

			this._handler.Configure(clientInfo);
			_ = this._runningClientList.TryGetValue(clientInfo.ID, out bool isStartedClient);
			if (isStartedClient && !ignoreAleadyStarted)
			{
				return new LogInResult(LogInResultType.StartedClient);
			}
			if(!string.IsNullOrEmpty(clientInfo.ClientPath))
				ChangeInstallPath(clientInfo.ClientPath);

			int count = 3;
			LogInResult loginResult;
			while (true)
			{
				try
				{
					loginResult = await this._handler.LogIn(decryptor);
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
				this._currentLogInAccount = clientInfo.ID;
				this._runningClientList[this._currentLogInAccount] = false;
			}
			else if (loginResult.Type == LogInResultType.RequireOtp)
				this._currentLogInAccount = clientInfo.ID;

			return loginResult;
		}

		public async Task<OtpResult> InputOTP(string otp)
		{
			var otpResult = await this._handler.InputOtp(otp);

			if (otpResult.Type == OtpResultType.Success)
			{
				this._runningClientList[this._currentLogInAccount!] = false;
			}

			return otpResult;
		}

		public async Task<bool> GameStart()
		{
			this._isLoginSucceed = await this._handler.CheckLogIn();
			if (!this._isLoginSucceed)
				return false;

			await this._handler.GameStart();
			this._runningClientList[this._currentLogInAccount!] = true;

			this._currentLogInAccount = string.Empty;
			await this._handler.LogOut();
			return true;
		}

		public async Task<bool?> GetSearchReward()
		{
			this._isLoginSucceed = await this._handler.CheckLogIn();

			if (!this._isLoginSucceed)
				return null;

			bool ret = false;
			ret = await this._handler.GetSearchReward();

			return ret;
		}

		public void ChangeInstallPath(string newInstallPath)
		{
			if (!OperatingSystem.IsWindows())
				throw new NotSupportedException("Check if OS is MS Windows");

			using (RegistryKey? registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\JOYON\\Gersang\\Korean", RegistryKeyPermissionCheck.ReadWriteSubTree))
			{
				if (registryKey != null)
				{
					registryKey.SetValue("InstallPath", newInstallPath);
					registryKey.Close();
				}
			}

			this._handler.ChangeClientPath(newInstallPath);
		}

		public async Task<int> GetCurrentVersion(ServerType serverType)
		{
			return await this._patcher.GetCurrentVersion(serverType);
		}

		public int CheckLocalVersion(string installPath)
		{
			return this._patcher.GetLocalVersion(installPath);
		}

		public async Task CheckUpdate(string installPath, ServerType serverType)
		{
			this._patcher = new Patcher();
			this._patcher.Configure(installPath, serverType);
			await this._patcher.CheckNeedToUpdate();
		}

		public async Task<string[]> GetPatchNote(ServerType serverType, IProgress<UpdateProgressEventArgs>? progressHandler = null)
		{
			this._patcher.Configure(string.Empty, serverType);
			return await this._patcher.GetPatchNote(progressHandler);
		}

		public async Task<bool> Update(string installPath, ServerType serverType, IProgress<UpdateProgressEventArgs>? progressHandler = null, CancellationToken cancellationToken = default)
		{
			if (this._patcher is null)
			{
				this._patcher = new Patcher();
			}
			this._patcher.BackUpDirectoryName = DateTime.Now.ToString("MM월 dd일 HH시 mm분 ss초");
			this._patcher.Configure(installPath, serverType);
			return await this._patcher.Update(progressHandler, cancellationToken);
		}

		public void ClearTempDirectory()
		{
			this._patcher?.ClearTempDirectory();
		}
	}
}
