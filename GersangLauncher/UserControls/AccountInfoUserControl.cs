using GersangLauncher.Models;
using GersangLauncher.Models.GameManager;
using System;
using System.Windows.Forms;

namespace GersangLauncher.UserControls
{
	public partial class AccountInfoUserControl : UserControl
	{
		private int _index;
		public int Index
		{
			get => _index;
			set
			{
				_index = value;
				BtnSave.Text = $"{_index + 1}번";
			}
		}

		private bool isPasswordChanged = false;

		public event EventHandler<AccountInfo>? SaveBtnClicked;
		public event EventHandler<AccountInfo>? LogInBtnClicked;
		public event EventHandler<string>? InstallPathChanged;
		public event EventHandler? StartBtnClicked;
		public event EventHandler? SearchBtnClicked;

		public AccountInfoUserControl()
		{
			InitializeComponent();
			AccountControlToolTip.SetToolTip(BtnSave, "현재 설정을 저장합니다.");
			AccountControlToolTip.SetToolTip(BtnSearch, "검색 보상을 수령합니다. 로그인 상태에서만 작동합니다.");
		}

		public AccountInfoUserControl(int index) : this()
		{
			Index = index;
		}

		public AccountInfoUserControl(AccountInfo accountInfo, int index) : this(index)
		{
			if (accountInfo == null)
				accountInfo = new AccountInfo();
			TextBoxID.Text = accountInfo.ID;
			TextBoxPW.Text = accountInfo.EncryptedPassword;
			TextBoxGamePath.Text = accountInfo.Path;
			isPasswordChanged = false;
		}

		private void BtnSave_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(TextBoxID.Text) && string.IsNullOrEmpty(TextBoxPW.Text) && string.IsNullOrEmpty(TextBoxGamePath.Text))
				return;

			OnSaveBtnClicked();
		}

		private void BtnLogIn_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(TextBoxID.Text) || string.IsNullOrEmpty(TextBoxPW.Text))
			{
				MessageBox.Show("ID/PW를 입력하시기 바랍니다.");
				return;
			}

			OnLogInBtnClicked();
		}

		private void BtnStart_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(TextBoxGamePath.Text))
			{
				MessageBox.Show("게임 경로를 선택해 주시기 바랍니다.");
				return;
			}
			OnStartBtnClicked();
		}

		private void BtnFindGamePath_Click(object sender, EventArgs e)
		{
			var fbd = new FolderBrowserDialog();
			fbd.Description = "경로 선택";
			if (fbd.ShowDialog() == DialogResult.OK)
			{
				TextBoxGamePath.Text = fbd.SelectedPath;
				InstallPathChanged?.Invoke(this, TextBoxGamePath.Text);
			}
		}

		private void BtnSearch_Click(object sender, EventArgs e)
		{
			OnSearchBtnClicked();
		}

		private void OnSaveBtnClicked()
		{
			var accountInfo = new AccountInfo
			{
				ID = TextBoxID.Text,
				EncryptedPassword = TextBoxPW.Text,
				Path = TextBoxGamePath.Text,
			};
			SaveBtnClicked?.Invoke(this, accountInfo);
		}

		private void OnLogInBtnClicked()
		{
			var accountInfo = new AccountInfo
			{
				ID = TextBoxID.Text,
				EncryptedPassword = TextBoxPW.Text,
				Path = TextBoxGamePath.Text,
			};
			LogInBtnClicked?.Invoke(this, accountInfo);
		}

		private void OnStartBtnClicked()
		{
			StartBtnClicked?.Invoke(this, EventArgs.Empty);
		}

		private void OnSearchBtnClicked()
		{
			SearchBtnClicked?.Invoke(this, EventArgs.Empty);
		}

		private void TextBoxPW_TextChanged(object sender, EventArgs e)
		{
			isPasswordChanged = true;
		}

		// Encrypt Password
		private void TextBoxPW_Leave(object sender, EventArgs e)
		{
			if (isPasswordChanged)
			{
				TextBoxPW.Text = CryptoFactory.Protect(TextBoxPW.Text, Form1._entropy);
			}
			isPasswordChanged = false;
		}
	}
}
