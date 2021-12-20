namespace GersangLauncher
{
	partial class FormInformation
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
			this.label1 = new System.Windows.Forms.Label();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.LabelVersion = new System.Windows.Forms.Label();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.TextBoxLicense = new System.Windows.Forms.TextBox();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.LabelInformation = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.linkLabel2 = new System.Windows.Forms.LinkLabel();
			this.tableLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.label1.Location = new System.Drawing.Point(3, 5);
			this.label1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(298, 37);
			this.label1.TabIndex = 0;
			this.label1.Text = "Gersang Launcher";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.LabelVersion, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.linkLabel1, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 3);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 5;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 185F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(304, 421);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// LabelVersion
			// 
			this.LabelVersion.AutoSize = true;
			this.LabelVersion.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LabelVersion.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.LabelVersion.Location = new System.Drawing.Point(3, 52);
			this.LabelVersion.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.LabelVersion.Name = "LabelVersion";
			this.LabelVersion.Size = new System.Drawing.Size(298, 17);
			this.LabelVersion.TabIndex = 2;
			this.LabelVersion.Text = "거상 런처";
			this.LabelVersion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// linkLabel1
			// 
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.linkLabel1.Location = new System.Drawing.Point(3, 79);
			this.linkLabel1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(298, 15);
			this.linkLabel1.TabIndex = 3;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "https://github.com/LOONACIA/GersangLauncher";
			this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.flowLayoutPanel1.Controls.Add(this.TextBoxLicense);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 239);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(298, 179);
			this.flowLayoutPanel1.TabIndex = 4;
			// 
			// TextBoxLicense
			// 
			this.TextBoxLicense.BackColor = System.Drawing.SystemColors.Window;
			this.TextBoxLicense.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.TextBoxLicense.Location = new System.Drawing.Point(3, 3);
			this.TextBoxLicense.Multiline = true;
			this.TextBoxLicense.Name = "TextBoxLicense";
			this.TextBoxLicense.ReadOnly = true;
			this.TextBoxLicense.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TextBoxLicense.Size = new System.Drawing.Size(296, 177);
			this.TextBoxLicense.TabIndex = 1;
			this.TextBoxLicense.TabStop = false;
			this.TextBoxLicense.Text = "라이선스";
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 41.61074F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58.38926F));
			this.tableLayoutPanel2.Controls.Add(this.LabelInformation, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.label3, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.linkLabel2, 1, 1);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 102);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 3;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(298, 131);
			this.tableLayoutPanel2.TabIndex = 5;
			// 
			// LabelInformation
			// 
			this.LabelInformation.AutoSize = true;
			this.tableLayoutPanel2.SetColumnSpan(this.LabelInformation, 2);
			this.LabelInformation.Location = new System.Drawing.Point(3, 5);
			this.LabelInformation.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.LabelInformation.Name = "LabelInformation";
			this.LabelInformation.Size = new System.Drawing.Size(287, 75);
			this.LabelInformation.TabIndex = 6;
			this.LabelInformation.Text = "사용법 및 프로그램 최신 버전은 상단 페이지에서 확인할 수 있습니다.\r\n\r\n※ 프로그램 사용에 대한 책임은 전적으로 사용자에게 있습니다.";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label3.Location = new System.Drawing.Point(3, 90);
			this.label3.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(118, 15);
			this.label3.TabIndex = 7;
			this.label3.Text = "버그 제보/기능 건의";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// linkLabel2
			// 
			this.linkLabel2.AutoSize = true;
			this.linkLabel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.linkLabel2.Location = new System.Drawing.Point(127, 90);
			this.linkLabel2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.linkLabel2.Name = "linkLabel2";
			this.linkLabel2.Size = new System.Drawing.Size(168, 15);
			this.linkLabel2.TabIndex = 8;
			this.linkLabel2.TabStop = true;
			this.linkLabel2.Text = "https://loonacia.tistory.com";
			this.linkLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
			// 
			// FormInformation
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(304, 421);
			this.Controls.Add(this.tableLayoutPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormInformation";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "프로그램 정보";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label LabelVersion;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Label LabelInformation;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.LinkLabel linkLabel2;
		private System.Windows.Forms.TextBox TextBoxLicense;
	}
}