namespace GersangLauncher
{
	partial class Form1
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.LauncherStatusStrip = new System.Windows.Forms.StatusStrip();
			this.LauncherStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.ClientListPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.LauncherToolStrip = new System.Windows.Forms.ToolStrip();
			this.OpenSettingsTSBtn = new System.Windows.Forms.ToolStripButton();
			this.AddClientTSBtn = new System.Windows.Forms.ToolStripButton();
			this.RemoveClientTSBtn = new System.Windows.Forms.ToolStripButton();
			this.OpenInfoTSBtn = new System.Windows.Forms.ToolStripButton();
			this.LauncherStatusStrip.SuspendLayout();
			this.panel1.SuspendLayout();
			this.ClientListPanel.SuspendLayout();
			this.LauncherToolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// LauncherStatusStrip
			// 
			this.LauncherStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LauncherStatusLabel});
			this.LauncherStatusStrip.Location = new System.Drawing.Point(0, 231);
			this.LauncherStatusStrip.Name = "LauncherStatusStrip";
			this.LauncherStatusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.LauncherStatusStrip.Size = new System.Drawing.Size(654, 22);
			this.LauncherStatusStrip.SizingGrip = false;
			this.LauncherStatusStrip.TabIndex = 1;
			// 
			// LauncherStatusLabel
			// 
			this.LauncherStatusLabel.Name = "LauncherStatusLabel";
			this.LauncherStatusLabel.Size = new System.Drawing.Size(31, 17);
			this.LauncherStatusLabel.Text = "상태";
			// 
			// panel1
			// 
			this.panel1.AutoSize = true;
			this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panel1.Controls.Add(this.ClientListPanel);
			this.panel1.Controls.Add(this.LauncherStatusStrip);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(654, 253);
			this.panel1.TabIndex = 2;
			// 
			// AccountPanel
			// 
			this.ClientListPanel.AutoSize = true;
			this.ClientListPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientListPanel.Controls.Add(this.LauncherToolStrip);
			this.ClientListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ClientListPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.ClientListPanel.Location = new System.Drawing.Point(0, 0);
			this.ClientListPanel.Name = "AccountPanel";
			this.ClientListPanel.Size = new System.Drawing.Size(654, 231);
			this.ClientListPanel.TabIndex = 2;
			// 
			// LauncherToolStrip
			// 
			this.LauncherToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.LauncherToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenSettingsTSBtn,
            this.AddClientTSBtn,
            this.RemoveClientTSBtn,
            this.OpenInfoTSBtn});
			this.LauncherToolStrip.Location = new System.Drawing.Point(0, 0);
			this.LauncherToolStrip.Name = "LauncherToolStrip";
			this.LauncherToolStrip.Size = new System.Drawing.Size(143, 25);
			this.LauncherToolStrip.TabIndex = 0;
			this.LauncherToolStrip.Text = "툴바";
			// 
			// OpenSettingsTSBtn
			// 
			this.OpenSettingsTSBtn.AutoToolTip = false;
			this.OpenSettingsTSBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.OpenSettingsTSBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.OpenSettingsTSBtn.Name = "OpenSettingsTSBtn";
			this.OpenSettingsTSBtn.Size = new System.Drawing.Size(35, 22);
			this.OpenSettingsTSBtn.Text = "설정";
			this.OpenSettingsTSBtn.Click += new System.EventHandler(this.OpenSettingsTSBtn_Click);
			// 
			// AddClientTSBtn
			// 
			this.AddClientTSBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.AddClientTSBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.AddClientTSBtn.Name = "AddClientTSBtn";
			this.AddClientTSBtn.Size = new System.Drawing.Size(35, 22);
			this.AddClientTSBtn.Text = "추가";
			this.AddClientTSBtn.ToolTipText = "클라이언트를 추가합니다.";
			this.AddClientTSBtn.Click += new System.EventHandler(this.AddClientTSBtn_Click);
			// 
			// RemoveClientTSBtn
			// 
			this.RemoveClientTSBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.RemoveClientTSBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.RemoveClientTSBtn.Name = "RemoveClientTSBtn";
			this.RemoveClientTSBtn.Size = new System.Drawing.Size(35, 22);
			this.RemoveClientTSBtn.Text = "삭제";
			this.RemoveClientTSBtn.ToolTipText = "클라이언트를 삭제합니다.";
			this.RemoveClientTSBtn.Click += new System.EventHandler(this.RemoveClientTSBtn_Click);
			// 
			// OpenInfoTSBtn
			// 
			this.OpenInfoTSBtn.AutoToolTip = false;
			this.OpenInfoTSBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.OpenInfoTSBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.OpenInfoTSBtn.Name = "OpenInfoTSBtn";
			this.OpenInfoTSBtn.Size = new System.Drawing.Size(35, 22);
			this.OpenInfoTSBtn.Text = "정보";
			this.OpenInfoTSBtn.Click += new System.EventHandler(this.OpenInfoTSBtn_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(654, 253);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Form1";
			this.Text = "Gersang Launcher";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.Shown += new System.EventHandler(this.Form1_Shown);
			this.LauncherStatusStrip.ResumeLayout(false);
			this.LauncherStatusStrip.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ClientListPanel.ResumeLayout(false);
			this.ClientListPanel.PerformLayout();
			this.LauncherToolStrip.ResumeLayout(false);
			this.LauncherToolStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.StatusStrip LauncherStatusStrip;
		private System.Windows.Forms.ToolStripStatusLabel LauncherStatusLabel;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.FlowLayoutPanel ClientListPanel;
		private System.Windows.Forms.ToolStrip LauncherToolStrip;
		private System.Windows.Forms.ToolStripButton AddClientTSBtn;
		private System.Windows.Forms.ToolStripButton RemoveClientTSBtn;
		private System.Windows.Forms.ToolStripButton OpenSettingsTSBtn;
		private System.Windows.Forms.ToolStripButton OpenInfoTSBtn;
	}
}
