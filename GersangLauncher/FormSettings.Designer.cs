namespace GersangLauncher
{
	partial class FormSettings
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.GroupBoxHandlerType = new System.Windows.Forms.GroupBox();
			this.RadioBtnUseHttp = new System.Windows.Forms.RadioButton();
			this.RadioBtnUseBrowser = new System.Windows.Forms.RadioButton();
			this.GroupBoxClientLauncher = new System.Windows.Forms.GroupBox();
			this.ChkBoxByPassStarter = new System.Windows.Forms.CheckBox();
			this.GroupBoxLauncher = new System.Windows.Forms.GroupBox();
			this.ChkBoxHideServerType = new System.Windows.Forms.CheckBox();
			this.ChkBoxSavePassword = new System.Windows.Forms.CheckBox();
			this.ChkBoxUseCredential = new System.Windows.Forms.CheckBox();
			this.SettingsToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.BtnSave = new System.Windows.Forms.Button();
			this.BtnCancel = new System.Windows.Forms.Button();
			this.GroupBoxHandlerType.SuspendLayout();
			this.GroupBoxClientLauncher.SuspendLayout();
			this.GroupBoxLauncher.SuspendLayout();
			this.SuspendLayout();
			// 
			// GroupBoxHandlerType
			// 
			this.GroupBoxHandlerType.Controls.Add(this.RadioBtnUseHttp);
			this.GroupBoxHandlerType.Controls.Add(this.RadioBtnUseBrowser);
			this.GroupBoxHandlerType.Location = new System.Drawing.Point(6, 22);
			this.GroupBoxHandlerType.Name = "GroupBoxHandlerType";
			this.GroupBoxHandlerType.Size = new System.Drawing.Size(131, 76);
			this.GroupBoxHandlerType.TabIndex = 0;
			this.GroupBoxHandlerType.TabStop = false;
			this.GroupBoxHandlerType.Text = "런처 구동 방식";
			// 
			// RadioBtnUseHttp
			// 
			this.RadioBtnUseHttp.AutoSize = true;
			this.RadioBtnUseHttp.Checked = true;
			this.RadioBtnUseHttp.Location = new System.Drawing.Point(6, 22);
			this.RadioBtnUseHttp.Name = "RadioBtnUseHttp";
			this.RadioBtnUseHttp.Size = new System.Drawing.Size(53, 19);
			this.RadioBtnUseHttp.TabIndex = 0;
			this.RadioBtnUseHttp.TabStop = true;
			this.RadioBtnUseHttp.Text = "HTTP";
			this.RadioBtnUseHttp.UseVisualStyleBackColor = true;
			// 
			// RadioBtnUseBrowser
			// 
			this.RadioBtnUseBrowser.AutoSize = true;
			this.RadioBtnUseBrowser.Enabled = false;
			this.RadioBtnUseBrowser.Location = new System.Drawing.Point(6, 47);
			this.RadioBtnUseBrowser.Name = "RadioBtnUseBrowser";
			this.RadioBtnUseBrowser.Size = new System.Drawing.Size(113, 19);
			this.RadioBtnUseBrowser.TabIndex = 1;
			this.RadioBtnUseBrowser.Text = "브라우저 자동화";
			this.RadioBtnUseBrowser.UseVisualStyleBackColor = true;
			// 
			// GroupBoxClientLauncher
			// 
			this.GroupBoxClientLauncher.Controls.Add(this.ChkBoxByPassStarter);
			this.GroupBoxClientLauncher.Controls.Add(this.GroupBoxHandlerType);
			this.GroupBoxClientLauncher.Location = new System.Drawing.Point(12, 12);
			this.GroupBoxClientLauncher.Name = "GroupBoxClientLauncher";
			this.GroupBoxClientLauncher.Size = new System.Drawing.Size(149, 133);
			this.GroupBoxClientLauncher.TabIndex = 1;
			this.GroupBoxClientLauncher.TabStop = false;
			this.GroupBoxClientLauncher.Text = "클라 실행";
			// 
			// ChkBoxByPassStarter
			// 
			this.ChkBoxByPassStarter.AutoSize = true;
			this.ChkBoxByPassStarter.Enabled = false;
			this.ChkBoxByPassStarter.Location = new System.Drawing.Point(12, 104);
			this.ChkBoxByPassStarter.Name = "ChkBoxByPassStarter";
			this.ChkBoxByPassStarter.Size = new System.Drawing.Size(78, 19);
			this.ChkBoxByPassStarter.TabIndex = 1;
			this.ChkBoxByPassStarter.Text = "직접 실행";
			this.ChkBoxByPassStarter.UseVisualStyleBackColor = true;
			// 
			// GroupBoxLauncher
			// 
			this.GroupBoxLauncher.Controls.Add(this.ChkBoxHideServerType);
			this.GroupBoxLauncher.Controls.Add(this.ChkBoxSavePassword);
			this.GroupBoxLauncher.Controls.Add(this.ChkBoxUseCredential);
			this.GroupBoxLauncher.Location = new System.Drawing.Point(167, 12);
			this.GroupBoxLauncher.Name = "GroupBoxLauncher";
			this.GroupBoxLauncher.Size = new System.Drawing.Size(150, 98);
			this.GroupBoxLauncher.TabIndex = 2;
			this.GroupBoxLauncher.TabStop = false;
			this.GroupBoxLauncher.Text = "기본 설정";
			// 
			// ChkBoxHideServerType
			// 
			this.ChkBoxHideServerType.AutoSize = true;
			this.ChkBoxHideServerType.Location = new System.Drawing.Point(6, 73);
			this.ChkBoxHideServerType.Name = "ChkBoxHideServerType";
			this.ChkBoxHideServerType.Size = new System.Drawing.Size(118, 19);
			this.ChkBoxHideServerType.TabIndex = 2;
			this.ChkBoxHideServerType.Text = "서버 선택 숨기기";
			this.ChkBoxHideServerType.UseVisualStyleBackColor = true;
			// 
			// ChkBoxSavePassword
			// 
			this.ChkBoxSavePassword.AutoSize = true;
			this.ChkBoxSavePassword.Checked = true;
			this.ChkBoxSavePassword.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ChkBoxSavePassword.Location = new System.Drawing.Point(6, 47);
			this.ChkBoxSavePassword.Name = "ChkBoxSavePassword";
			this.ChkBoxSavePassword.Size = new System.Drawing.Size(102, 19);
			this.ChkBoxSavePassword.TabIndex = 1;
			this.ChkBoxSavePassword.Text = "패스워드 저장";
			this.ChkBoxSavePassword.UseVisualStyleBackColor = true;
			// 
			// ChkBoxUseCredential
			// 
			this.ChkBoxUseCredential.AutoSize = true;
			this.ChkBoxUseCredential.Checked = true;
			this.ChkBoxUseCredential.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ChkBoxUseCredential.Location = new System.Drawing.Point(6, 22);
			this.ChkBoxUseCredential.Name = "ChkBoxUseCredential";
			this.ChkBoxUseCredential.Size = new System.Drawing.Size(134, 19);
			this.ChkBoxUseCredential.TabIndex = 0;
			this.ChkBoxUseCredential.Text = "암호 사용(재시작됨)";
			this.ChkBoxUseCredential.UseVisualStyleBackColor = true;
			// 
			// BtnSave
			// 
			this.BtnSave.Location = new System.Drawing.Point(222, 141);
			this.BtnSave.Name = "BtnSave";
			this.BtnSave.Size = new System.Drawing.Size(95, 29);
			this.BtnSave.TabIndex = 3;
			this.BtnSave.Text = "저장";
			this.BtnSave.UseVisualStyleBackColor = true;
			this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
			// 
			// BtnCancel
			// 
			this.BtnCancel.Location = new System.Drawing.Point(222, 176);
			this.BtnCancel.Name = "BtnCancel";
			this.BtnCancel.Size = new System.Drawing.Size(95, 29);
			this.BtnCancel.TabIndex = 4;
			this.BtnCancel.Text = "취소";
			this.BtnCancel.UseVisualStyleBackColor = true;
			this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
			// 
			// FormSettings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(331, 217);
			this.Controls.Add(this.BtnCancel);
			this.Controls.Add(this.BtnSave);
			this.Controls.Add(this.GroupBoxLauncher);
			this.Controls.Add(this.GroupBoxClientLauncher);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormSettings";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "설정";
			this.GroupBoxHandlerType.ResumeLayout(false);
			this.GroupBoxHandlerType.PerformLayout();
			this.GroupBoxClientLauncher.ResumeLayout(false);
			this.GroupBoxClientLauncher.PerformLayout();
			this.GroupBoxLauncher.ResumeLayout(false);
			this.GroupBoxLauncher.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox GroupBoxHandlerType;
		private System.Windows.Forms.RadioButton RadioBtnUseBrowser;
		private System.Windows.Forms.RadioButton RadioBtnUseHttp;
		private System.Windows.Forms.GroupBox GroupBoxClientLauncher;
		private System.Windows.Forms.GroupBox GroupBoxLauncher;
		private System.Windows.Forms.CheckBox ChkBoxSavePassword;
		private System.Windows.Forms.CheckBox ChkBoxUseCredential;
		private System.Windows.Forms.CheckBox ChkBoxByPassStarter;
		private System.Windows.Forms.ToolTip SettingsToolTip;
		private System.Windows.Forms.Button BtnSave;
		private System.Windows.Forms.Button BtnCancel;
		private System.Windows.Forms.CheckBox ChkBoxHideServerType;
	}
}