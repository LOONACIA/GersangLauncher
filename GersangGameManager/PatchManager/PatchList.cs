using System.Collections;
using System.Collections.Generic;

namespace GersangGameManager.PatchManager
{
	public class PatchList : ICollection<PatchInfo>
	{
		private List<PatchInfo> _patchList;
		public PatchList()
		{
			this._patchList = new List<PatchInfo>();
		}
		public int Count => this._patchList.Count;

		public bool IsReadOnly => false;

		public void Add(PatchInfo item)
		{
			if (this._patchList.Contains(item))
				return;
			else
				this._patchList.Add(item);
		}

		public void AddRange(IEnumerable<PatchInfo> items)
		{
			foreach (var item in items)
				Add(item);
		}

		public void Clear() => this._patchList.Clear();

		public bool Contains(PatchInfo item) => this._patchList.Contains(item);

		public void CopyTo(PatchInfo[] array, int arrayIndex) => this._patchList.CopyTo(array, arrayIndex);

		public IEnumerator<PatchInfo> GetEnumerator() => this._patchList.GetEnumerator();

		public bool Remove(PatchInfo item) => this._patchList.Remove(item);

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}
