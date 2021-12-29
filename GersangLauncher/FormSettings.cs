using GersangLauncher.Models;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace GersangLauncher
{
	public partial class FormSettings : Form
	{
		public UserConfig UserConfig;

		public FormSettings(UserConfig userConfig)
		{
			InitializeComponent();
			UserConfig = userConfig;
			SettingsToolTip.SetToolTip(GroupBoxHandlerType, "런처 구동 방식을 설정합니다.");
			SettingsToolTip.SetToolTip(RadioBtnUseHttp, "HTTP 통신을 통해 클라이언트를 실행합니다.");
			SettingsToolTip.SetToolTip(RadioBtnUseBrowser, "브라우저 자동화를 통해 클라이언트를 실행합니다.");
			SettingsToolTip.SetToolTip(ChkBoxByPassStarter, "거상 게임 스타터를 통하지 않고 클라이언트를 직접 실행합니다.");

			SettingsToolTip.SetToolTip(ChkBoxUseCredential, "패스워드 암호화에 별도 입력값을 이용할 것인지 선택합니다.\n선택하지 않으면 윈도우 사용자 정보를 이용해 암호화합니다.");
			SettingsToolTip.SetToolTip(ChkBoxSavePassword, "클라이언트 암호를 로컬에 저장합니다.");
			SettingsToolTip.SetToolTip(ChkBoxHideServerType, "서버 선택 그룹을 숨깁니다.");

			SettingsToolTip.SetToolTip(BtnSave, "설정을 저장하고 창을 닫습니다.");
			SettingsToolTip.SetToolTip(BtnCancel, "설정을 저장하지 않고 창을 닫습니다.");

			SetConfig(userConfig);
		}

		private void SetConfig(UserConfig userConfig)
		{
			GroupBoxHandlerType.Controls.OfType<RadioButton>().Select((x, i) => x.Checked = (int)UserConfig.HandlerType == i);

			ChkBoxUseCredential.Checked = UserConfig.UseUserCredential;
			ChkBoxSavePassword.Checked = UserConfig.SavePassword;
			ChkBoxHideServerType.Checked = UserConfig.HideServerPanel;
		}

		private void BtnSave_Click(object sender, EventArgs e)
		{
			UserConfig.HandlerType = (HandlerType)GroupBoxHandlerType.Controls.OfType<RadioButton>().ToList().FindIndex(0, x => x.Checked == true);

			UserConfig.UseUserCredential = ChkBoxUseCredential.Checked;
			UserConfig.SavePassword = ChkBoxSavePassword.Checked;
			UserConfig.HideServerPanel = ChkBoxHideServerType.Checked;
			DialogResult = DialogResult.OK;
		}

		private void BtnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
