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
			_handler = handler;
			_patcher = new Patcher();
			_runningClientList = new Dictionary<string, bool>();
			_patcher.ClearTempDirectory();
		}

		public async Task<LogInResult> LogIn(ClientInfo clientInfo, DecryptDelegate decryptor, bool ignoreAleadyStarted)
		{
			if (string.IsNullOrEmpty(clientInfo.ID) || string.IsNullOrEmpty(clientInfo.EncryptedPassword))
				throw new InvalidOperationException("Check ID or Password");

			_handler.Configure(clientInfo);
			_ = _runningClientList.TryGetValue(clientInfo.ID, out bool isStartedClient);
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
					loginResult = await _handler.LogIn(decryptor).ConfigureAwait(false);
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
				_currentLogInAccount = clientInfo.ID;
				_runningClientList[_currentLogInAccount] = false;
			}
			else if (loginResult.Type == LogInResultType.RequireOtp)
				_currentLogInAccount = clientInfo.ID;

			return loginResult;
		}

		public async Task<OtpResult> InputOTP(string otp)
		{
			var otpResult = await _handler.InputOtp(otp).ConfigureAwait(false);

			if (otpResult.Type == OtpResultType.Success)
			{
				_runningClientList[_currentLogInAccount!] = false;
			}

			return otpResult;
		}

		public async Task<bool> GameStart()
		{
			int count = 3;
			while (true)
			{
				try
				{
					_isLoginSucceed = await _handler.CheckLogIn().ConfigureAwait(false);
					break;
				}
				catch (System.Net.Http.HttpRequestException)
				{
					count--;
				}
				if (count <= 0)
				{
					return false;
				}
			}
			
			if (!_isLoginSucceed)
				return false;

			await _handler.GameStart().ConfigureAwait(false);
			_runningClientList[_currentLogInAccount!] = true;

			_currentLogInAccount = string.Empty;
			await _handler.LogOut().ConfigureAwait(false);
			return true;
		}

		[Obsolete]
		public async Task<bool?> GetSearchReward()
		{
			_isLoginSucceed = await _handler.CheckLogIn().ConfigureAwait(false);

			if (!_isLoginSucceed)
				return null;

			return await _handler.GetSearchReward().ConfigureAwait(false);
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

			_handler.ChangeClientPath(newInstallPath);
		}

		public Task<int> GetCurrentVersion(ServerType serverType)
		{
			return _patcher.GetCurrentVersionAsync(serverType);
		}

		public int CheckLocalVersion(string installPath)
		{
			return _patcher.GetLocalVersion(installPath);
		}

		public Task CheckUpdate(string installPath, ServerType serverType)
		{
			_patcher = new Patcher();
			_patcher.Configure(installPath, serverType);
			return _patcher.CheckNeedToUpdate();
		}

		public Task<string[]> GetPatchNote(ServerType serverType, IProgress<UpdateProgressEventArgs>? progressHandler = null)
		{
			_patcher.Configure(string.Empty, serverType);
			return _patcher.GetPatchNoteAsync(progressHandler);
		}

		public Task<bool> Update(string installPath, ServerType serverType, IProgress<UpdateProgressEventArgs>? progressHandler = null, CancellationToken cancellationToken = default)
		{
			if (_patcher is null)
			{
				_patcher = new Patcher();
			}
			_patcher.BackUpDirectoryName = DateTime.Now.ToString("MM월 dd일 HH시 mm분 ss초");
			_patcher.Configure(installPath, serverType);
			return _patcher.Update(progressHandler, cancellationToken);
		}

		public void ClearTempDirectory()
		{
			_patcher?.ClearTempDirectory();
		}
	}
}
