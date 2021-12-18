namespace GersangLauncher.UserControls
{
	partial class AccountInfoUserControl
	{
		/// <summary> 
		/// 필수 디자이너 변수입니다.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// 사용 중인 모든 리소스를 정리합니다.
		/// </summary>
		/// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region 구성 요소 디자이너에서 생성한 코드

		/// <summary> 
		/// 디자이너 지원에 필요한 메서드입니다. 
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.BtnPatch = new System.Windows.Forms.Button();
			this.BtnSearch = new System.Windows.Forms.Button();
			this.BtnStart = new System.Windows.Forms.Button();
			this.TextBoxGamePath = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.TextBoxPW = new System.Windows.Forms.TextBox();
			this.TextBoxID = new System.Windows.Forms.TextBox();
			this.BtnFindGamePath = new System.Windows.Forms.Button();
			this.BtnSave = new System.Windows.Forms.Button();
			this.BtnLogIn = new System.Windows.Forms.Button();
			this.AccountControlToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 9;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.907539F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.61166F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.907539F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.61166F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.59957F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.59957F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.881229F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.881229F));
			this.tableLayoutPanel1.Controls.Add(this.BtnPatch, 9, 0);
			this.tableLayoutPanel1.Controls.Add(this.BtnSearch, 7, 0);
			this.tableLayoutPanel1.Controls.Add(this.BtnStart, 6, 0);
			this.tableLayoutPanel1.Controls.Add(this.TextBoxGamePath, 3, 1);
			this.tableLayoutPanel1.Controls.Add(this.label2, 3, 0);
			this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.TextBoxPW, 4, 0);
			this.tableLayoutPanel1.Controls.Add(this.TextBoxID, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.BtnFindGamePath, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.BtnSave, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.BtnLogIn, 5, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(650, 70);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// BtnPatch
			// 
			this.BtnPatch.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BtnPatch.Enabled = false;
			this.BtnPatch.Location = new System.Drawing.Point(599, 5);
			this.BtnPatch.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.BtnPatch.Name = "BtnPatch";
			this.BtnPatch.Size = new System.Drawing.Size(48, 25);
			this.BtnPatch.TabIndex = 10;
			this.BtnPatch.Text = "패치";
			this.BtnPatch.UseVisualStyleBackColor = true;
			// 
			// BtnSearch
			// 
			this.BtnSearch.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BtnSearch.Location = new System.Drawing.Point(548, 5);
			this.BtnSearch.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.BtnSearch.Name = "BtnSearch";
			this.BtnSearch.Size = new System.Drawing.Size(45, 25);
			this.BtnSearch.TabIndex = 9;
			this.BtnSearch.Text = "검색";
			this.BtnSearch.UseVisualStyleBackColor = true;
			this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
			// 
			// BtnStart
			// 
			this.BtnStart.AutoSize = true;
			this.BtnStart.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BtnStart.Location = new System.Drawing.Point(459, 5);
			this.BtnStart.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.BtnStart.Name = "BtnStart";
			this.BtnStart.Size = new System.Drawing.Size(83, 25);
			this.BtnStart.TabIndex = 6;
			this.BtnStart.Text = "시작";
			this.BtnStart.UseVisualStyleBackColor = true;
			this.BtnStart.Click += new System.EventHandler(this.BtnStart_Click);
			// 
			// TextBoxGamePath
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.TextBoxGamePath, 6);
			this.TextBoxGamePath.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TextBoxGamePath.Enabled = false;
			this.TextBoxGamePath.Location = new System.Drawing.Point(224, 41);
			this.TextBoxGamePath.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
			this.TextBoxGamePath.Name = "TextBoxGamePath";
			this.TextBoxGamePath.Size = new System.Drawing.Size(423, 23);
			this.TextBoxGamePath.TabIndex = 8;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Location = new System.Drawing.Point(222, 0);
			this.label2.Margin = new System.Windows.Forms.Padding(1, 0, 1, 5);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(26, 30);
			this.label2.TabIndex = 3;
			this.label2.Text = "PW";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Location = new System.Drawing.Point(76, 0);
			this.label1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(26, 30);
			this.label1.TabIndex = 1;
			this.label1.Text = "ID";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// TextBoxPW
			// 
			this.TextBoxPW.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TextBoxPW.Location = new System.Drawing.Point(252, 6);
			this.TextBoxPW.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
			this.TextBoxPW.Name = "TextBoxPW";
			this.TextBoxPW.Size = new System.Drawing.Size(112, 23);
			this.TextBoxPW.TabIndex = 4;
			this.TextBoxPW.UseSystemPasswordChar = true;
			this.TextBoxPW.TextChanged += new System.EventHandler(this.TextBoxPW_TextChanged);
			this.TextBoxPW.Leave += new System.EventHandler(this.TextBoxPW_Leave);
			// 
			// TextBoxID
			// 
			this.TextBoxID.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TextBoxID.Location = new System.Drawing.Point(106, 6);
			this.TextBoxID.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
			this.TextBoxID.Name = "TextBoxID";
			this.TextBoxID.Size = new System.Drawing.Size(112, 23);
			this.TextBoxID.TabIndex = 2;
			// 
			// BtnFindGamePath
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.BtnFindGamePath, 2);
			this.BtnFindGamePath.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BtnFindGamePath.Location = new System.Drawing.Point(78, 40);
			this.BtnFindGamePath.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.BtnFindGamePath.Name = "BtnFindGamePath";
			this.BtnFindGamePath.Size = new System.Drawing.Size(140, 25);
			this.BtnFindGamePath.TabIndex = 7;
			this.BtnFindGamePath.Text = "경로";
			this.BtnFindGamePath.UseVisualStyleBackColor = true;
			this.BtnFindGamePath.Click += new System.EventHandler(this.BtnFindGamePath_Click);
			// 
			// BtnSave
			// 
			this.BtnSave.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BtnSave.Location = new System.Drawing.Point(5, 5);
			this.BtnSave.Margin = new System.Windows.Forms.Padding(5);
			this.BtnSave.Name = "BtnSave";
			this.tableLayoutPanel1.SetRowSpan(this.BtnSave, 2);
			this.BtnSave.Size = new System.Drawing.Size(65, 60);
			this.BtnSave.TabIndex = 0;
			this.BtnSave.TabStop = false;
			this.BtnSave.Text = "-";
			this.BtnSave.UseVisualStyleBackColor = true;
			this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
			// 
			// BtnLogIn
			// 
			this.BtnLogIn.AutoSize = true;
			this.BtnLogIn.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BtnLogIn.Location = new System.Drawing.Point(370, 5);
			this.BtnLogIn.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.BtnLogIn.Name = "BtnLogIn";
			this.BtnLogIn.Size = new System.Drawing.Size(83, 25);
			this.BtnLogIn.TabIndex = 5;
			this.BtnLogIn.Text = "로그인";
			this.BtnLogIn.UseVisualStyleBackColor = true;
			this.BtnLogIn.Click += new System.EventHandler(this.BtnLogIn_Click);
			// 
			// AccountInfoUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "AccountInfoUserControl";
			this.Size = new System.Drawing.Size(650, 70);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button BtnStart;
		private System.Windows.Forms.TextBox TextBoxGamePath;
		private System.Windows.Forms.TextBox TextBoxPW;
		private System.Windows.Forms.TextBox TextBoxID;
		private System.Windows.Forms.Button BtnFindGamePath;
		private System.Windows.Forms.Button BtnSave;
		private System.Windows.Forms.Button BtnLogIn;
		private System.Windows.Forms.Button BtnSearch;
		private System.Windows.Forms.Button BtnPatch;
		private System.Windows.Forms.ToolTip AccountControlToolTip;
	}
}
