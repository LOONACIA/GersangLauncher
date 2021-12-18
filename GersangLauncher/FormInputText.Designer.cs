namespace GersangLauncher
{
	partial class FormInputText
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
			this.LabelTitle = new System.Windows.Forms.Label();
			this.TextBoxInput = new System.Windows.Forms.TextBox();
			this.BtnOK = new System.Windows.Forms.Button();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.FrmToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// LabelTitle
			// 
			this.LabelTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.LabelTitle.AutoSize = true;
			this.LabelTitle.Location = new System.Drawing.Point(5, 23);
			this.LabelTitle.Margin = new System.Windows.Forms.Padding(5, 0, 3, 0);
			this.LabelTitle.Name = "LabelTitle";
			this.LabelTitle.Size = new System.Drawing.Size(31, 15);
			this.LabelTitle.TabIndex = 0;
			this.LabelTitle.Text = "암호";
			this.LabelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// TextBoxInput
			// 
			this.TextBoxInput.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.TextBoxInput.Location = new System.Drawing.Point(42, 19);
			this.TextBoxInput.Name = "TextBoxInput";
			this.TextBoxInput.Size = new System.Drawing.Size(144, 23);
			this.TextBoxInput.TabIndex = 1;
			this.TextBoxInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxOtp_KeyDown);
			// 
			// BtnOK
			// 
			this.BtnOK.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.BtnOK.Location = new System.Drawing.Point(192, 18);
			this.BtnOK.Margin = new System.Windows.Forms.Padding(3, 3, 5, 3);
			this.BtnOK.Name = "BtnOK";
			this.BtnOK.Size = new System.Drawing.Size(93, 25);
			this.BtnOK.TabIndex = 2;
			this.BtnOK.Text = "확인";
			this.BtnOK.UseVisualStyleBackColor = true;
			this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.LabelTitle, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.BtnOK, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.TextBoxInput, 1, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(292, 61);
			this.tableLayoutPanel1.TabIndex = 3;
			// 
			// FormInputText
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(292, 61);
			this.Controls.Add(this.tableLayoutPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormInputText";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormInputText_FormClosing);
			this.TextChanged += new System.EventHandler(this.FormInputText_TextChanged);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label LabelTitle;
		private System.Windows.Forms.TextBox TextBoxInput;
		private System.Windows.Forms.Button BtnOK;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.ToolTip FrmToolTip;
	}
}