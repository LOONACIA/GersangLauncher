using GersangGameManager;
using GersangGameManager.PatchManager;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GersangLauncher
{
	public partial class FormPatch : Form
	{
		private GameManager _gameManager;
		private CancellationTokenSource _cts;
		private Progress<UpdateProgressEventArgs> _progressHandler;
		private string _installPath;
		private ServerType _serverType;
		private DataTable _dataTable;
		Stopwatch _sw;

		public FormPatch(GameManager gameManager, string installPath, ServerType serverType)
		{
			InitializeComponent();
			_gameManager = gameManager;
			_installPath = installPath;
			_serverType = serverType;
			_cts = new CancellationTokenSource();
			_progressHandler = new Progress<UpdateProgressEventArgs>();
			_progressHandler.ProgressChanged += _progressHandler_ProgressChanged;

			_dataTable = new DataTable();
			_dataTable.Columns.Add("이름");
			_dataTable.Columns.Add("경로");
			_dataTable.Columns.Add("진행도", typeof(float));
			_dataTable.DefaultView.Sort = "진행도 desc";
			UpdateTableView.RowHeadersVisible = false;

			UpdateTableView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			UpdateTableView.DataSource = _dataTable;

			ProgressBarMain.Maximum = 100;
			_sw = new Stopwatch();
		}

		private void _progressHandler_ProgressChanged(object? sender, UpdateProgressEventArgs e)
		{
			var ticks = _sw.Elapsed;

			if (ticks.Hours > 0)
				LabelTick.Text = ticks.ToString("hh\\:mm\\:ss");
			else
				LabelTick.Text = ticks.ToString("m\\:ss");

			LabelMessage.Text = e.UpdateTaskType switch
			{
				UpdateTaskType.GetPatchNote => "패치 노트를 가져오는 중",
				UpdateTaskType.GetVersionList => "버전 정보를 가져오는 중",
				UpdateTaskType.CheckCurrentVersion => "버전 정보를 확인하는 중",
				UpdateTaskType.DownloadPatchInfo => "패치 정보 파일을 가져오는 중",
				UpdateTaskType.DownloadPatchFile => "패치 다운로드 중",
				UpdateTaskType.CopyPatchFile => "파일을 복사하는 중",
				UpdateTaskType.NoUpdateRequired => "이미 최신 버전입니다.",
			};

			if (e.UpdateTaskType != UpdateTaskType.DownloadPatchFile)
			{
				if (e.Percentage >= 0)
					ProgressBarMain.Value = e.Percentage;
			}
			else if (e.UpdateTaskType == UpdateTaskType.DownloadPatchFile)
			{
				if (e.Percentage >= 0)
					ProgressBarMain.Value = e.Percentage;

				if (e.PatchInfo is null)
					return;

				var fileName = Path.GetFileName(e.PatchInfo.FileName);
				var rows = _dataTable
					.AsEnumerable()
					.Where(c => c.Field<string>("이름").Equals(fileName)).ToList();

				var progress = e.DownloadProgress;
				if (rows is null || rows.Count == 0)
				{
					_dataTable.Rows.Add(e.PatchInfo.FileName, e.PatchInfo.FilePath, progress);
				}
				else if (rows.Count > 0)
				{
					rows.First()["진행도"] = progress;
					Debug.Assert(rows.Count < 2);
				}
				
				UpdateTableView.FirstDisplayedScrollingRowIndex = UpdateTableView.Rows.Count - 1;
			}
		}

		public async Task StartPatch()
		{
			var timer = new System.Windows.Forms.Timer();
			timer.Interval = 500;
			timer.Tick += (s, e) =>
			{
				var ticks = _sw.Elapsed;

				if (ticks.Hours > 0)
					LabelTick.Text = ticks.ToString("hh\\:mm\\:ss");
				else
					LabelTick.Text = ticks.ToString("m\\:ss");
			};

			_sw.Start();
			timer.Start();
			var result = await Task.Run(() => _gameManager.Update(_installPath, _serverType, _progressHandler, _cts.Token));
			timer.Stop();
			_sw.Stop();

			if (result)
			{
				LabelMessage.Text = "업데이트 완료";
				MessageBox.Show("업데이트 완료.");
				DialogResult = DialogResult.OK;
			}
			else
			{
				if (LabelMessage.Text.Contains("최신 버전"))
				{
					MessageBox.Show(LabelMessage.Text);
					DialogResult = DialogResult.OK;
				}
				else
					MessageBox.Show("업데이트 실패.");
			}
		}

		private async Task UpdatePatchNote()
		{
			var patchNote = await _gameManager.GetPatchNote(_serverType, _progressHandler);
			TextBoxPatchNote.Text = string.Join("\r\n", patchNote);
		}

		private void BtnStop_Click(object sender, EventArgs e)
		{
			_cts.Cancel();
			DialogResult = DialogResult.Cancel;
		}

		private void UpdateTableView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
			if(UpdateTableView.Rows.Count > 1)
			{
				foreach(DataGridViewRow row in UpdateTableView.Rows)
				{
					if (row.Cells.Count < 3)
						return;
					if (row.Cells[2].Value is null)
						return;
					
					if(float.TryParse(row.Cells[2].Value.ToString(), out float value))
					{
						if(value < 0)
							row.Cells[2].Style.ForeColor = Color.Red;
					}
				}
			}
		}

		private async void FormPatch_Shown(object sender, EventArgs e)
		{
			await UpdatePatchNote();
			await StartPatch();
		}
	}
}