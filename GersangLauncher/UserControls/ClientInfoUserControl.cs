using GersangGameManager;
using GersangLauncher.Models;
using System;
using System.Windows.Forms;

namespace GersangLauncher.UserControls
{
	public partial class ClientInfoUserControl : UserControl
	{
		private int _index;
		public int Index
		{
			get => this._index;
			set
			{
				this._index = value;
				BtnSave.Text = $"{this._index + 1}번";
			}
		}

		private bool _hideServerPanel;
		public bool HideServerPanel
		{
			get => this._hideServerPanel;
			set
			{
				if (this._hideServerPanel == value)
					return;

				this._hideServerPanel = value;
				var colBtnFindPath = TablePanelMain.GetColumn(BtnFindGamePath);
				var colSpanBtnFindPath = TablePanelMain.GetColumnSpan(BtnFindGamePath);
				var colTBFindPath = TablePanelMain.GetColumn(TextBoxGamePath);
				var colSpanTBFindPath = TablePanelMain.GetColumnSpan(TextBoxGamePath);
				if (this._hideServerPanel)
				{
					RBIsMain.Checked = true;
					PanelServerType.Visible = false;
					PanelServerType.Enabled = false;
					TablePanelMain.SetColumn(BtnFindGamePath, colBtnFindPath - 3);
					TablePanelMain.SetColumnSpan(BtnFindGamePath, colSpanBtnFindPath + 1);
					TablePanelMain.SetColumn(TextBoxGamePath, colTBFindPath - 2);
					TablePanelMain.SetColumnSpan(TextBoxGamePath, colSpanTBFindPath + 2);
				}
				else
				{
					PanelServerType.Visible = true;
					PanelServerType.Enabled = true;
					TablePanelMain.SetColumn(BtnFindGamePath, colBtnFindPath + 3);
					TablePanelMain.SetColumnSpan(BtnFindGamePath, colSpanBtnFindPath - 1);
					TablePanelMain.SetColumn(TextBoxGamePath, colTBFindPath + 2);
					TablePanelMain.SetColumnSpan(TextBoxGamePath, colSpanTBFindPath - 2);
				}
				PanelServerType.Refresh();
			}
		}

		public bool IsNeedToUpdate
		{
			set => BtnPatch.Enabled = value;
		}

		private bool isPasswordChanged = false;

		public event EventHandler<ClientInfo>? SaveBtnClicked;
		public event EventHandler<ClientInfo>? LogInBtnClicked;
		public event EventHandler<string>? InstallPathChanged;
		public event EventHandler<ClientInfo>? StartBtnClicked;
		public event EventHandler<ClientInfo>? SearchBtnClicked;
		public event EventHandler<ClientInfo>? PatchBtnClicked;

		public ClientInfoUserControl()
		{
			InitializeComponent();
			ClientControlToolTip.SetToolTip(BtnSave, "현재 설정을 저장합니다.");
			ClientControlToolTip.SetToolTip(BtnSearch, "검색 보상을 수령합니다. 로그인 상태에서만 작동합니다.");
		}

		public ClientInfoUserControl(int index) : this()
		{
			Index = index;
		}

		public ClientInfoUserControl(ClientInfo clientInfo, int index) : this(index)
		{
			if (clientInfo == null)
				clientInfo = new ClientInfo();
			TextBoxID.Text = clientInfo.ID;
			TextBoxPW.Text = clientInfo.EncryptedPassword;
			TextBoxGamePath.Text = clientInfo.ClientPath;
			RBIsTest.Checked = clientInfo.ServerType == ServerType.Test;
			isPasswordChanged = false;
		}

		private ClientInfo BuildClientInfo() => new ClientInfo
		{
			ID = TextBoxID.Text,
			EncryptedPassword = TextBoxPW.Text,
			ClientPath = TextBoxGamePath.Text,
			ServerType = RBIsMain.Checked ? ServerType.Main : ServerType.Test
		};

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

		private void BtnPatch_Click(object sender, EventArgs e)
		{
			OnPatchBtnClicked();
		}

		private void OnSaveBtnClicked()
		{
			SaveBtnClicked?.Invoke(this, BuildClientInfo());
		}

		private void OnLogInBtnClicked()
		{
			LogInBtnClicked?.Invoke(this, BuildClientInfo());
		}

		private void OnStartBtnClicked()
		{
			StartBtnClicked?.Invoke(this, BuildClientInfo());
		}

		private void OnSearchBtnClicked()
		{
			SearchBtnClicked?.Invoke(this, BuildClientInfo());
		}

		private void OnPatchBtnClicked()
		{
			PatchBtnClicked?.Invoke(this, BuildClientInfo());
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
				TextBoxPW.Text = CryptoFactory.Protect(TextBoxPW.Text, Form1.Entropy);
			}
			isPasswordChanged = false;
		}
	}
}
