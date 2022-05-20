﻿using GersangGameManager;
using GersangGameManager.Handler;
using GersangLauncher.Models;
using GersangLauncher.UserControls;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GersangLauncher
{
	public partial class Form1 : Form
	{
		private GameManager _gameManager;
		private UserConfig _config;
		private const string configFilePath = ".\\config.json";
		internal static string? Entropy;

		private int _currentMainVersion = -1;
		private int _currentTestVersion = -1;

		public Form1()
		{
			this._config = UserConfig.Open(configFilePath);
			Entropy = null;
			
			InitializeComponent();

			this._gameManager = InitializeGameManager();

			for (int index = 0; index < this._config.ClientList.Count; ++index)
			{
				AddClientInfoControl();
			}
		}

		private GameManager InitializeGameManager()
		{
			IGameManagerHandler handler = this._config.HandlerType switch
			{
				HandlerType.HTTP => new HttpClientGameManagerHandler(),
				HandlerType.Browser => new WebBrowserGameManagerHandler(),
				_ => new HttpClientGameManagerHandler()
			};
			return new GameManager(handler);
		}

		private void AddClientInfoControl()
		{
			var index = ClientListPanel.Controls.OfType<ClientInfoUserControl>().Count();
			if (index >= 7)
			{
				MessageBox.Show("더 이상 추가할 수 없습니다.");
				return;
			}

			while (this._config.ClientList.Count <= index)
				this._config.ClientList.Add(new ClientInfo());

			ClientInfo clientInfo = this._config.ClientList[index] ?? (this._config.ClientList[index] = new ClientInfo());
			ClientInfoUserControl clientInfoUserControl = new ClientInfoUserControl(clientInfo, index);
			clientInfoUserControl.HideServerPanel = this._config.HideServerPanel;
			
			if (this._config.HideServerPanel)
				clientInfo.ServerType = ServerType.Main;

			if (!string.IsNullOrEmpty(clientInfo.ClientPath))
				CheckUpdate(clientInfoUserControl, clientInfo);

			clientInfoUserControl.SaveBtnClicked += ClientInfoUserControl_SaveBtnClicked;
			clientInfoUserControl.InstallPathChanged += ClientInfoUserControl_InstallPathChanged;
			clientInfoUserControl.LogInBtnClicked += ClientInfoUserControl_LogInBtnClickedAsync;
			clientInfoUserControl.StartBtnClicked += ClientInfoUserControl_StartBtnClicked;
			clientInfoUserControl.SearchBtnClicked += ClientInfoUserControl_SearchBtnClicked;
			clientInfoUserControl.PatchBtnClicked += ClientInfoUserControl_PatchBtnClicked;

			ClientListPanel.Controls.Add(clientInfoUserControl);
		}

		private async Task CheckUpdate(ClientInfoUserControl clientInfoUserControl, ClientInfo clientInfo)
		{
			if(this._currentMainVersion == -1)
				this._currentMainVersion = await this._gameManager.GetCurrentVersion(ServerType.Main);
			if(this._currentTestVersion == -1)
				this._currentTestVersion = await this._gameManager.GetCurrentVersion(ServerType.Test);

			var localVersion = this._gameManager?.CheckLocalVersion(clientInfo.ClientPath);
			var currentVersion = clientInfo.ServerType == ServerType.Main ? this._currentMainVersion : this._currentTestVersion;
			clientInfoUserControl.IsNeedToUpdate = localVersion != -1 && localVersion < currentVersion;
		}

		private void RemoveClientInfoControl()
		{
			var list = ClientListPanel.Controls.OfType<ClientInfoUserControl>().ToList();
			if (list.Count <= 1)
			{
				MessageBox.Show("더 이상 삭제할 수 없습니다.");
				return;
			}
			ClientListPanel.Controls.Remove(list[^1]);
			this._config.ClientList.Remove(this._config.ClientList[^1]);
		}

		private void ClientInfoUserControl_SaveBtnClicked(object? sender, ClientInfo e)
		{
			var index = GetIndexOfClientInfoControl(sender);
			this._config.ClientList[index] = e;
			this._config.Save(configFilePath);
			CheckUpdate(sender as ClientInfoUserControl, this._config.ClientList[index]);
		}

		private void ClientInfoUserControl_InstallPathChanged(object? sender, string e)
		{
			var index = GetIndexOfClientInfoControl(sender);
			this._config.ClientList[index].ClientPath = e;
			this._gameManager.ChangeInstallPath(e);
			CheckUpdate(sender as ClientInfoUserControl, this._config.ClientList[index]);
		}

		private async Task<bool> TryLogIn(ClientInfo clientInfo, bool ignoreAleadyStarted)
		{
			try
			{
				var loginResult = await this._gameManager.LogIn(clientInfo, x => CryptoFactory.Unprotect(x, Entropy), ignoreAleadyStarted);
				string message = string.Empty;
				OtpResult otpResult = new(OtpResultType.Fail);
				switch (loginResult.Type)
				{
					case LogInResultType.Success:
						break;
					case LogInResultType.Fail:
						if (!string.IsNullOrEmpty(loginResult.Message))
							MessageBox.Show(loginResult.Message);
						break;
					case LogInResultType.RequireOtp:
						FormInputText form = new();
						form.Text = "OTP 입력";
						do
						{
							var dialogResult = form.ShowDialog();
							if (dialogResult != DialogResult.OK)
								break;
							otpResult = await this._gameManager.InputOTP(form.InputValue);

							if(otpResult.Type == OtpResultType.Error)
							{
								MessageBox.Show("확인되지 않은 오류 발생. 다시 로그인해 주시기 바랍니다.");
								return false;
							}

							if (!string.IsNullOrEmpty(otpResult.Message))
								MessageBox.Show(otpResult.Message);
						} while (otpResult.Type != OtpResultType.Success);
						form.Dispose();
						break;
					case LogInResultType.StartedClient:
						var result = MessageBox.Show("게임에 접속 중인 계정으로 재로그인 할 경우 게임이 종료될 수 있습니다. 계속 진행하시겠습니까?", "경고", MessageBoxButtons.OKCancel);
						if (result != DialogResult.OK)
							break;

						return await TryLogIn(clientInfo, true);
				}
				return (loginResult.Type == LogInResultType.Success) ||
					(loginResult.Type == LogInResultType.RequireOtp && otpResult.Type == OtpResultType.Success);
			}
			catch (Exception ex)
			{
				if (ex.ToString().Contains("WindowsCryptographicException"))
					MessageBox.Show("패스워드 복호화에 실패했습니다.\n키가 손상되었을 수 있습니다. 오류 반복 시 설정 파일을 삭제한 후 재시도해 보시기 바랍니다.");

				return false;
			}
		}

		private async void ClientInfoUserControl_LogInBtnClickedAsync(object? sender, ClientInfo e)
		{
			var result = await TryLogIn(e, false);
			var message = result ? "로그인 성공" : "로그인 실패";
			SetStatusStrip(sender, message);
		}

		private async void ClientInfoUserControl_StartBtnClicked(object? sender, ClientInfo e)
		{
			try
			{
				var result = await this._gameManager.GameStart();
				if(!result)
				{
					var loginResult = await TryLogIn(e, false);
					if(loginResult)
					{
						result = await this._gameManager.GameStart();
					}
				}
				if(result)
					SetStatusStrip(sender, "게임 실행");
			}
			catch (GameManagerException ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private async void ClientInfoUserControl_SearchBtnClicked(object? sender, ClientInfo e)
		{
			var searchResult = await this._gameManager.GetSearchReward();
			if (searchResult is null)
			{
				var loginResult = await TryLogIn(e, false);
				if (loginResult)
				{
					searchResult = await this._gameManager.GetSearchReward();
				}
				else
					searchResult = false;
			}

			var message = searchResult.Value ? "검색 보상 수령 완료" : "검색 보상 수령 실패";
			SetStatusStrip(sender, message);
		}

		private void ClientInfoUserControl_PatchBtnClicked(object? sender, ClientInfo e)
		{
			var frmPatch = new FormPatch(this._gameManager, e.ClientPath, e.ServerType);
			frmPatch.ShowDialog();
			CheckUpdate(sender as ClientInfoUserControl, e);
		}

		private int GetIndexOfClientInfoControl(object? sender) => (sender as ClientInfoUserControl).Index;

		private void SetStatusStrip(object? sender, string message)
		{
			if (sender is ClientInfoUserControl)
			{
				var index = GetIndexOfClientInfoControl(sender);
				SetStatusStrip(index, message);
			}
			else
				LauncherStatusLabel.Text = message;
		}
		private void SetStatusStrip(int index, string message)
		{
			LauncherStatusLabel.Text = $"{index + 1}번 클라: {message}";
		}

		private void OpenSettingsTSBtn_Click(object sender, EventArgs e)
		{
			bool prev_UseCredential = this._config.UseUserCredential;
			FormSettings frmSettings = new(this._config);
			frmSettings.ShowDialog();
			if (prev_UseCredential != this._config.UseUserCredential)
			{
				MessageBox.Show("재시작 후 적용됩니다. 저장된 비밀번호는 초기화됩니다.");
				this._config.ClientList.Where(x => !string.IsNullOrEmpty(x.EncryptedPassword)).ToList().ForEach(x => x.EncryptedPassword = string.Empty);
				Close();
				return;
			}
			ClientListPanel.Controls.OfType<ClientInfoUserControl>().ToList().ForEach(x => x.HideServerPanel = this._config.HideServerPanel);
		}

		private void AddClientTSBtn_Click(object sender, EventArgs e)
		{
			AddClientInfoControl();
		}

		private void RemoveClientTSBtn_Click(object sender, EventArgs e)
		{
			RemoveClientInfoControl();
		}

		private void OpenInfoTSBtn_Click(object sender, EventArgs e)
		{
			FormInformation frmInformation = new();
			frmInformation.ShowDialog();
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this._config is null)
				return;

			this._config.Save(configFilePath);

			this._gameManager?.ClearTempDirectory();
		}

		private void Form1_Shown(object sender, EventArgs e)
		{
			while (this._config.UseUserCredential)
			{
				Entropy = InputCredential();

				if (Entropy is null)
					break;

				if (!this._config.ClientList.Any(x => !string.IsNullOrEmpty(x.EncryptedPassword)))
					break;

				if (KeyValidation())
					break;
			}
		}

		private string? InputCredential()
		{
			FormInputText frmInputCredential = new(true);
			frmInputCredential.Text = "런처 암호";
			frmInputCredential.HelpText = "패스워드 암/복호화에 사용할 암호를 설정합니다. 설정에서 사용 여부를 선택할 수 있습니다.";
			return frmInputCredential.ShowDialog() == DialogResult.OK ? frmInputCredential.InputValue : null;
		}

		private bool KeyValidation()
		{
			bool ret = false;
			try
			{
				_ = CryptoFactory.Unprotect(this._config.ClientList.FirstOrDefault(x => !string.IsNullOrEmpty(x.EncryptedPassword)).EncryptedPassword, Entropy);
				ret = true;
			}
			catch (Exception ex)
			{
				if (ex.ToString().Contains("WindowsCryptographicException"))
				{
					ret = MessageBox.Show("패스워드 복호화에 실패했습니다.\n키가 손상되었을 수 있습니다. 오류 반복 시 설정 파일을 삭제한 후 재시도해 보시기 바랍니다.", "오류", MessageBoxButtons.RetryCancel) == DialogResult.Cancel;
				}
				else
				{
					MessageBox.Show(ex.Message);
					Close();
				}
			}
			return ret;
		}
	}
}
