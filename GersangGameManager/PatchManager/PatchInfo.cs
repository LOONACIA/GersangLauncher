using System;

namespace GersangGameManager.PatchManager
{
	public record PatchInfo : IEquatable<PatchInfo>
	{
		public string FileName { get; init; }
		public string FilePath { get; init; }

		public virtual bool Equals(PatchInfo other)
		{
			if (other is null)
				return false;

			return FileName == other.FileName;
		}

		public override int GetHashCode() => throw new NotImplementedException();
	}
}
