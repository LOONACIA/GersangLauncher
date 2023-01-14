namespace GersangLauncher.UserControls
{
	partial class ClientInfoUserControl
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
			this.TablePanelMain = new System.Windows.Forms.TableLayoutPanel();
			this.BtnPatch = new System.Windows.Forms.Button();
			this.BtnStart = new System.Windows.Forms.Button();
			this.TextBoxGamePath = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.TextBoxPW = new System.Windows.Forms.TextBox();
			this.TextBoxID = new System.Windows.Forms.TextBox();
			this.BtnFindGamePath = new System.Windows.Forms.Button();
			this.BtnSave = new System.Windows.Forms.Button();
			this.BtnLogIn = new System.Windows.Forms.Button();
			this.PanelServerType = new System.Windows.Forms.FlowLayoutPanel();
			this.LabelServerType = new System.Windows.Forms.Label();
			this.RBIsMain = new System.Windows.Forms.RadioButton();
			this.RBIsTest = new System.Windows.Forms.RadioButton();
			this.ClientControlToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.TablePanelMain.SuspendLayout();
			this.PanelServerType.SuspendLayout();
			this.SuspendLayout();
			// 
			// TablePanelMain
			// 
			this.TablePanelMain.ColumnCount = 8;
			this.TablePanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 112F));
			this.TablePanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.38587F));
			this.TablePanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.62065F));
			this.TablePanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.38587F));
			this.TablePanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.62065F));
			this.TablePanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.12004F));
			this.TablePanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.12004F));
			this.TablePanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.746871F));
			this.TablePanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TablePanelMain.Controls.Add(this.BtnPatch, 8, 0);
			this.TablePanelMain.Controls.Add(this.BtnStart, 6, 0);
			this.TablePanelMain.Controls.Add(this.TextBoxGamePath, 5, 1);
			this.TablePanelMain.Controls.Add(this.label2, 3, 0);
			this.TablePanelMain.Controls.Add(this.label1, 1, 0);
			this.TablePanelMain.Controls.Add(this.TextBoxPW, 4, 0);
			this.TablePanelMain.Controls.Add(this.TextBoxID, 2, 0);
			this.TablePanelMain.Controls.Add(this.BtnFindGamePath, 4, 1);
			this.TablePanelMain.Controls.Add(this.BtnSave, 0, 0);
			this.TablePanelMain.Controls.Add(this.BtnLogIn, 5, 0);
			this.TablePanelMain.Controls.Add(this.PanelServerType, 1, 1);
			this.TablePanelMain.Location = new System.Drawing.Point(0, 0);
			this.TablePanelMain.Margin = new System.Windows.Forms.Padding(4);
			this.TablePanelMain.Name = "TablePanelMain";
			this.TablePanelMain.RowCount = 2;
			this.TablePanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TablePanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TablePanelMain.Size = new System.Drawing.Size(975, 105);
			this.TablePanelMain.TabIndex = 0;
			// 
			// BtnPatch
			// 
			this.BtnPatch.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BtnPatch.Enabled = false;
			this.BtnPatch.Location = new System.Drawing.Point(892, 8);
			this.BtnPatch.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
			this.BtnPatch.Name = "BtnPatch";
			this.BtnPatch.Size = new System.Drawing.Size(79, 36);
			this.BtnPatch.TabIndex = 10;
			this.BtnPatch.Text = "패치";
			this.BtnPatch.UseVisualStyleBackColor = true;
			this.BtnPatch.Click += new System.EventHandler(this.BtnPatch_Click);
			// 
			// BtnStart
			// 
			this.BtnStart.AutoSize = true;
			this.BtnStart.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BtnStart.Location = new System.Drawing.Point(745, 8);
			this.BtnStart.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
			this.BtnStart.Name = "BtnStart";
			this.BtnStart.Size = new System.Drawing.Size(139, 36);
			this.BtnStart.TabIndex = 6;
			this.BtnStart.Text = "시작";
			this.BtnStart.UseVisualStyleBackColor = true;
			this.BtnStart.Click += new System.EventHandler(this.BtnStart_Click);
			// 
			// TextBoxGamePath
			// 
			this.TablePanelMain.SetColumnSpan(this.TextBoxGamePath, 3);
			this.TextBoxGamePath.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TextBoxGamePath.Enabled = false;
			this.TextBoxGamePath.Location = new System.Drawing.Point(598, 61);
			this.TextBoxGamePath.Margin = new System.Windows.Forms.Padding(4, 9, 4, 9);
			this.TextBoxGamePath.Name = "TextBoxGamePath";
			this.TextBoxGamePath.Size = new System.Drawing.Size(373, 31);
			this.TextBoxGamePath.TabIndex = 8;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Location = new System.Drawing.Point(355, 0);
			this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(42, 44);
			this.label2.TabIndex = 3;
			this.label2.Text = "PW";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Location = new System.Drawing.Point(114, 0);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(42, 44);
			this.label1.TabIndex = 1;
			this.label1.Text = "ID";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// TextBoxPW
			// 
			this.TextBoxPW.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TextBoxPW.Location = new System.Drawing.Point(403, 9);
			this.TextBoxPW.Margin = new System.Windows.Forms.Padding(4, 9, 4, 9);
			this.TextBoxPW.Name = "TextBoxPW";
			this.TextBoxPW.Size = new System.Drawing.Size(187, 31);
			this.TextBoxPW.TabIndex = 4;
			this.TextBoxPW.UseSystemPasswordChar = true;
			this.TextBoxPW.TextChanged += new System.EventHandler(this.TextBoxPW_TextChanged);
			this.TextBoxPW.Leave += new System.EventHandler(this.TextBoxPW_Leave);
			// 
			// TextBoxID
			// 
			this.TextBoxID.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TextBoxID.Location = new System.Drawing.Point(162, 9);
			this.TextBoxID.Margin = new System.Windows.Forms.Padding(4, 9, 4, 9);
			this.TextBoxID.Name = "TextBoxID";
			this.TextBoxID.Size = new System.Drawing.Size(187, 31);
			this.TextBoxID.TabIndex = 2;
			// 
			// BtnFindGamePath
			// 
			this.BtnFindGamePath.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BtnFindGamePath.Location = new System.Drawing.Point(403, 60);
			this.BtnFindGamePath.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
			this.BtnFindGamePath.Name = "BtnFindGamePath";
			this.BtnFindGamePath.Size = new System.Drawing.Size(187, 37);
			this.BtnFindGamePath.TabIndex = 7;
			this.BtnFindGamePath.Text = "경로";
			this.BtnFindGamePath.UseVisualStyleBackColor = true;
			this.BtnFindGamePath.Click += new System.EventHandler(this.BtnFindGamePath_Click);
			// 
			// BtnSave
			// 
			this.BtnSave.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BtnSave.Location = new System.Drawing.Point(8, 8);
			this.BtnSave.Margin = new System.Windows.Forms.Padding(8);
			this.BtnSave.Name = "BtnSave";
			this.TablePanelMain.SetRowSpan(this.BtnSave, 2);
			this.BtnSave.Size = new System.Drawing.Size(96, 89);
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
			this.BtnLogIn.Location = new System.Drawing.Point(598, 8);
			this.BtnLogIn.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
			this.BtnLogIn.Name = "BtnLogIn";
			this.BtnLogIn.Size = new System.Drawing.Size(139, 36);
			this.BtnLogIn.TabIndex = 5;
			this.BtnLogIn.Text = "로그인";
			this.BtnLogIn.UseVisualStyleBackColor = true;
			this.BtnLogIn.Click += new System.EventHandler(this.BtnLogIn_Click);
			// 
			// PanelServerType
			// 
			this.TablePanelMain.SetColumnSpan(this.PanelServerType, 3);
			this.PanelServerType.Controls.Add(this.LabelServerType);
			this.PanelServerType.Controls.Add(this.RBIsMain);
			this.PanelServerType.Controls.Add(this.RBIsTest);
			this.PanelServerType.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PanelServerType.Location = new System.Drawing.Point(116, 56);
			this.PanelServerType.Margin = new System.Windows.Forms.Padding(4);
			this.PanelServerType.Name = "PanelServerType";
			this.PanelServerType.Size = new System.Drawing.Size(279, 45);
			this.PanelServerType.TabIndex = 11;
			// 
			// LabelServerType
			// 
			this.LabelServerType.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.LabelServerType.AutoSize = true;
			this.LabelServerType.Location = new System.Drawing.Point(2, 6);
			this.LabelServerType.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.LabelServerType.Name = "LabelServerType";
			this.LabelServerType.Size = new System.Drawing.Size(48, 25);
			this.LabelServerType.TabIndex = 5;
			this.LabelServerType.Text = "서버";
			// 
			// RBIsMain
			// 
			this.RBIsMain.AutoSize = true;
			this.RBIsMain.Checked = true;
			this.RBIsMain.Location = new System.Drawing.Point(56, 4);
			this.RBIsMain.Margin = new System.Windows.Forms.Padding(4, 4, 2, 4);
			this.RBIsMain.Name = "RBIsMain";
			this.RBIsMain.Size = new System.Drawing.Size(73, 29);
			this.RBIsMain.TabIndex = 3;
			this.RBIsMain.TabStop = true;
			this.RBIsMain.Text = "메인";
			this.RBIsMain.UseVisualStyleBackColor = true;
			// 
			// RBIsTest
			// 
			this.RBIsTest.AutoSize = true;
			this.RBIsTest.Location = new System.Drawing.Point(133, 4);
			this.RBIsTest.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
			this.RBIsTest.Name = "RBIsTest";
			this.RBIsTest.Size = new System.Drawing.Size(91, 29);
			this.RBIsTest.TabIndex = 4;
			this.RBIsTest.Text = "테스트";
			this.RBIsTest.UseVisualStyleBackColor = true;
			// 
			// ClientInfoUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.Controls.Add(this.TablePanelMain);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "ClientInfoUserControl";
			this.Size = new System.Drawing.Size(975, 105);
			this.TablePanelMain.ResumeLayout(false);
			this.TablePanelMain.PerformLayout();
			this.PanelServerType.ResumeLayout(false);
			this.PanelServerType.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel TablePanelMain;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button BtnStart;
		private System.Windows.Forms.TextBox TextBoxGamePath;
		private System.Windows.Forms.TextBox TextBoxPW;
		private System.Windows.Forms.TextBox TextBoxID;
		private System.Windows.Forms.Button BtnFindGamePath;
		private System.Windows.Forms.Button BtnSave;
		private System.Windows.Forms.Button BtnLogIn;
		private System.Windows.Forms.Button BtnPatch;
		private System.Windows.Forms.ToolTip ClientControlToolTip;
		private System.Windows.Forms.FlowLayoutPanel PanelServerType;
		private System.Windows.Forms.RadioButton RBIsMain;
		private System.Windows.Forms.RadioButton RBIsTest;
		private System.Windows.Forms.Label LabelServerType;
	}
}
