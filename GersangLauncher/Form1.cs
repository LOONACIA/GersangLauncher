using GersangLauncher.Models;
using GersangLauncher.Models.GameManager;
using GersangLauncher.UserControls;
using System;
using System.Linq;
using System.Windows.Forms;

namespace GersangLauncher
{
	public partial class Form1 : Form
	{
		private GameManager _gameManager;
		private UserConfig _config;
		private const string configFilePath = ".\\config.json";
		internal static string? _entropy;

		public Form1()
		{
			OpenConfig();
			_entropy = null;

			InitializeComponent();

			for (int index = 0; index < _config.AccountInfos.Count; ++index)
			{
				AddAccountInfoControl();
			}

			InitializeGameManager();
		}

		private void OpenConfig()
		{
			_config = UserConfig.Open(configFilePath);
		}

		private void InitializeGameManager()
		{
			IGameManagerHandler handler = _config.HandlerType switch
			{
				HandlerType.HTTP => new HttpClientGameManagerHandler(),
				HandlerType.Browser => new WebBrowserGameManagerHandler(),
				_ => new HttpClientGameManagerHandler()
			};
			_gameManager = new GameManager(handler);
		}

		private void AddAccountInfoControl()
		{
			var index = AccountPanel.Controls.OfType<AccountInfoUserControl>().Count();
			if (index >= 7)
			{
				MessageBox.Show("더 이상 추가할 수 없습니다.");
				return;
			}

			while (_config.AccountInfos.Count <= index)
				_config.AccountInfos.Add(new AccountInfo());

			AccountInfo accountInfo = _config.AccountInfos[index] ?? (_config.AccountInfos[index] = new AccountInfo());

			AccountInfoUserControl accountInfoUserControl = new AccountInfoUserControl(accountInfo, index);
			accountInfoUserControl.SaveBtnClicked += AccountInfoUserControl_SaveBtnClicked;
			accountInfoUserControl.InstallPathChanged += AccountInfoUserControl_InstallPathChanged;
			accountInfoUserControl.LogInBtnClicked += AccountInfoUserControl_LogInBtnClickedAsync;
			accountInfoUserControl.StartBtnClicked += AccountInfoUserControl_StartBtnClicked;
			accountInfoUserControl.SearchBtnClicked += AccountInfoUserControl_SearchBtnClicked;
			AccountPanel.Controls.Add(accountInfoUserControl);
		}

		private void RemoveAccountInfoControl()
		{
			var list = AccountPanel.Controls.OfType<AccountInfoUserControl>().ToList();
			if (list.Count <= 1)
			{
				MessageBox.Show("더 이상 삭제할 수 없습니다.");
				return;
			}
			AccountPanel.Controls.Remove(list[^1]);
			_config.AccountInfos.Remove(_config.AccountInfos[^1]);
		}

		private void AccountInfoUserControl_SaveBtnClicked(object? sender, AccountInfo e)
		{
			var index = GetIndexOfAccountInfo(sender);
			_config.AccountInfos[index] = e;
			_config.Save(configFilePath);
		}

		private void AccountInfoUserControl_InstallPathChanged(object? sender, string e)
		{
			var index = GetIndexOfAccountInfo(sender);
			_config.AccountInfos[index].Path = e;
			_gameManager.ChangeInstallPath(e);
		}

		private async void AccountInfoUserControl_LogInBtnClickedAsync(object? sender, AccountInfo e)
		{
			AccountInfo accountInfo = e;
			try
			{
				var loginResult = await _gameManager.LogIn(accountInfo, x => CryptoFactory.Unprotect(x, _entropy));
				if (loginResult)
				{
					var index = GetIndexOfAccountInfo(sender);
					_config.AccountInfos[index] = accountInfo;
					_config.Save(configFilePath);
				}
				var message = loginResult ? "로그인 성공" : "로그인 실패";
				SetStatusStrip(sender, message);
			}
			catch (System.Net.Http.HttpRequestException)
			{
				MessageBox.Show("통신 오류 발생. 다시 시도해 주세요.");
			}
			catch (Exception ex)
			{
				if (ex.ToString().Contains("WindowsCryptographicException"))
					MessageBox.Show("패스워드 복호화에 실패했습니다.\n키가 손상되었을 수 있습니다. 오류 반복 시 설정 파일을 삭제한 후 재시도해 보시기 바랍니다.");
			}
		}

		private async void AccountInfoUserControl_StartBtnClicked(object? sender, EventArgs e)
		{
			await _gameManager.GameStart();
			SetStatusStrip(sender, "게임 실행");
		}

		private async void AccountInfoUserControl_SearchBtnClicked(object? sender, EventArgs e)
		{
			var searchResult = await _gameManager.GetSearchReward();
			var message = searchResult ? "검색 보상 수령 완료" : "검색 보상 수령 실패";
			SetStatusStrip(sender, message);
		}

		private int GetIndexOfAccountInfo(object? sender) => (sender as AccountInfoUserControl).Index;

		private void SetStatusStrip(object? sender, string message)
		{
			if (sender is AccountInfoUserControl)
			{
				var index = GetIndexOfAccountInfo(sender);
				LauncherStatusLabel.Text = $"{index + 1}번 클라: {message}";
			}
			else
				LauncherStatusLabel.Text = message;
		}

		private void OpenSettingsTSBtn_Click(object sender, EventArgs e)
		{
			bool prev_UseCredential = _config.UseUserCredential;
			FormSettings frmSettings = new(_config);
			frmSettings.ShowDialog();
			if (prev_UseCredential != _config.UseUserCredential)
			{
				MessageBox.Show("재시작 후 적용됩니다. 저장된 비밀번호는 초기화됩니다.");
				_config.AccountInfos.Where(x => !string.IsNullOrEmpty(x.EncryptedPassword)).ToList().ForEach(x => x.EncryptedPassword = string.Empty);
				Close();
				return;
			}
		}

		private void AddClientTSBtn_Click(object sender, EventArgs e)
		{
			AddAccountInfoControl();
		}

		private void RemoveClientTSBtn_Click(object sender, EventArgs e)
		{
			RemoveAccountInfoControl();
		}

		private void OpenInfoTSBtn_Click(object sender, EventArgs e)
		{
			FormInformation frmInformation = new();
			frmInformation.ShowDialog();
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (_config is not null)
				_config.Save(configFilePath);
		}

		private void Form1_Shown(object sender, EventArgs e)
		{
			while (_config.UseUserCredential)
			{
				_entropy = InputCredential();

				if (_entropy is null)
					break;

				if (!_config.AccountInfos.Any(x => !string.IsNullOrEmpty(x.EncryptedPassword)))
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
				_ = CryptoFactory.Unprotect(_config.AccountInfos.FirstOrDefault(x => !string.IsNullOrEmpty(x.EncryptedPassword)).EncryptedPassword, _entropy);
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
