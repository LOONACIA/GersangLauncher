using System.Collections;
using System.Collections.Generic;

namespace GersangGameManager.PatchManager
{
	public class PatchList : ICollection<PatchInfo>
	{
		private List<PatchInfo> _patchList;
		public PatchList()
		{
			_patchList = new List<PatchInfo>();
		}
		public int Count => _patchList.Count;

		public bool IsReadOnly => false;

		public void Add(PatchInfo item)
		{
			if (_patchList.Contains(item))
				return;
			else
				_patchList.Add(item);
		}

		public void AddRange(IEnumerable<PatchInfo> items)
		{
			foreach (var item in items)
				Add(item);
		}

		public void Clear() => _patchList.Clear();

		public bool Contains(PatchInfo item) => _patchList.Contains(item);

		public void CopyTo(PatchInfo[] array, int arrayIndex) => _patchList.CopyTo(array, arrayIndex);

		public IEnumerator<PatchInfo> GetEnumerator() => _patchList.GetEnumerator();

		public bool Remove(PatchInfo item) => _patchList.Remove(item);

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}
