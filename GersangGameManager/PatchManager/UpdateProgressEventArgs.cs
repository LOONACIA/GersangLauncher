namespace GersangGameManager.PatchManager
{
	public enum UpdateTaskType
	{
		GetPatchNote = 0,
		GetVersionList,
		CheckCurrentVersion,
		DownloadPatchInfo,
		DownloadPatchFile,
		CopyPatchFile,


		NoUpdateRequired
	}
	public class UpdateProgressEventArgs
	{
		public UpdateTaskType UpdateTaskType { get; set; }
		public int Percentage { get; set; }
		public PatchInfo? PatchInfo { get; set; }
		public float? DownloadProgress { get; set; }
	}
}
