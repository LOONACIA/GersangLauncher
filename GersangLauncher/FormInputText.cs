using System;
using System.Windows.Forms;

namespace GersangLauncher
{
	public partial class FormInputText : Form
	{
		public string? InputValue { get; private set; }
		public string? HelpText
		{
			set
			{
				FrmToolTip.SetToolTip(TextBoxInput, value);
				FrmToolTip.SetToolTip(BtnOK, value);
			}
		}
		public FormInputText(bool isPasswordChar = false)
		{
			InitializeComponent();
			TextBoxInput.UseSystemPasswordChar = isPasswordChar;
		}

		private void BtnOK_Click(object sender, EventArgs e)
		{
			InputValue = TextBoxInput.Text;
			DialogResult = DialogResult.OK;
		}

		private void TextBoxOtp_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				if (!string.IsNullOrEmpty(TextBoxInput.Text))
				{
					InputValue = TextBoxInput.Text;
					DialogResult = DialogResult.OK;
				}
			}
		}

		private void FormInputText_TextChanged(object sender, EventArgs e)
		{
			LabelTitle.Text = Text;
		}

		private void FormInputText_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (string.IsNullOrEmpty(TextBoxInput.Text) || DialogResult != DialogResult.OK)
			{
				var result = MessageBox.Show("입력값이 없습니다. 계속 진행하시겠습니까?", "경고", MessageBoxButtons.OKCancel);
				if (result != DialogResult.OK)
					e.Cancel = true;
			}
		}
	}
}
