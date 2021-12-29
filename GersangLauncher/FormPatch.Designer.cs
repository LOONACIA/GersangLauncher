namespace GersangLauncher
{
	partial class FormPatch
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			this.TextBoxPatchNote = new System.Windows.Forms.TextBox();
			this.UpdateTableView = new System.Windows.Forms.DataGridView();
			this.ProgressBarMain = new System.Windows.Forms.ProgressBar();
			this.LabelMessage = new System.Windows.Forms.Label();
			this.BtnStop = new System.Windows.Forms.Button();
			this.LabelTick = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.UpdateTableView)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// TextBoxPatchNote
			// 
			this.TextBoxPatchNote.Location = new System.Drawing.Point(12, 12);
			this.TextBoxPatchNote.Multiline = true;
			this.TextBoxPatchNote.Name = "TextBoxPatchNote";
			this.TextBoxPatchNote.ReadOnly = true;
			this.TextBoxPatchNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TextBoxPatchNote.Size = new System.Drawing.Size(489, 327);
			this.TextBoxPatchNote.TabIndex = 0;
			// 
			// UpdateTableView
			// 
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.NullValue = null;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.UpdateTableView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.UpdateTableView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle4.Format = "#.00\\%";
			dataGridViewCellStyle4.NullValue = null;
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.UpdateTableView.DefaultCellStyle = dataGridViewCellStyle4;
			this.UpdateTableView.Location = new System.Drawing.Point(507, 12);
			this.UpdateTableView.Name = "UpdateTableView";
			this.UpdateTableView.ReadOnly = true;
			this.UpdateTableView.RowTemplate.Height = 25;
			this.UpdateTableView.Size = new System.Drawing.Size(274, 327);
			this.UpdateTableView.TabIndex = 1;
			this.UpdateTableView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.UpdateTableView_RowsAdded);
			// 
			// ProgressBarMain
			// 
			this.ProgressBarMain.Location = new System.Drawing.Point(12, 376);
			this.ProgressBarMain.Name = "ProgressBarMain";
			this.ProgressBarMain.Size = new System.Drawing.Size(665, 32);
			this.ProgressBarMain.TabIndex = 2;
			// 
			// LabelMessage
			// 
			this.LabelMessage.AutoSize = true;
			this.LabelMessage.Location = new System.Drawing.Point(12, 351);
			this.LabelMessage.Name = "LabelMessage";
			this.LabelMessage.Size = new System.Drawing.Size(43, 15);
			this.LabelMessage.TabIndex = 3;
			this.LabelMessage.Text = "메시지";
			// 
			// BtnStop
			// 
			this.BtnStop.Location = new System.Drawing.Point(686, 376);
			this.BtnStop.Name = "BtnStop";
			this.BtnStop.Size = new System.Drawing.Size(95, 32);
			this.BtnStop.TabIndex = 4;
			this.BtnStop.Text = "취소";
			this.BtnStop.UseVisualStyleBackColor = true;
			this.BtnStop.Click += new System.EventHandler(this.BtnStop_Click);
			// 
			// LabelTick
			// 
			this.LabelTick.AutoSize = true;
			this.LabelTick.Dock = System.Windows.Forms.DockStyle.Right;
			this.LabelTick.Location = new System.Drawing.Point(84, 0);
			this.LabelTick.Name = "LabelTick";
			this.LabelTick.Size = new System.Drawing.Size(59, 15);
			this.LabelTick.TabIndex = 5;
			this.LabelTick.Text = "경과 시간";
			this.LabelTick.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.LabelTick);
			this.panel1.Location = new System.Drawing.Point(534, 351);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(143, 23);
			this.panel1.TabIndex = 6;
			// 
			// FormPatch
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(800, 416);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.BtnStop);
			this.Controls.Add(this.LabelMessage);
			this.Controls.Add(this.ProgressBarMain);
			this.Controls.Add(this.UpdateTableView);
			this.Controls.Add(this.TextBoxPatchNote);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormPatch";
			this.Text = "패치";
			this.Shown += new System.EventHandler(this.FormPatch_Shown);
			((System.ComponentModel.ISupportInitialize)(this.UpdateTableView)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox TextBoxPatchNote;
		private System.Windows.Forms.DataGridView UpdateTableView;
		private System.Windows.Forms.ProgressBar ProgressBarMain;
		private System.Windows.Forms.Label LabelMessage;
		private System.Windows.Forms.Button BtnStop;
		private System.Windows.Forms.Label LabelTick;
		private System.Windows.Forms.Panel panel1;
	}
}