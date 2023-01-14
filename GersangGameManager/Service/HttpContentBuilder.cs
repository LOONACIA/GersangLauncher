using System.Collections.Generic;
using System.Net.Http;

namespace GersangGameManager.Service
{
	internal class HttpContentBuilder
	{
		private Dictionary<string, string> _postData;

		public HttpContentBuilder()
		{
			_postData = new Dictionary<string, string>();
		}

		public void Add(string key, string value) => _postData[key] = value;

		public void AddRange(IEnumerable<KeyValuePair<string, string>> values)
		{
			foreach (var value in values)
				Add(value.Key, value.Value);
		}

		public void Clear() => _postData?.Clear();

		public HttpContent Build(ContentType contentType) => contentType switch
		{
			ContentType.FormData => new FormUrlEncodedContent(_postData),
			_ => new FormUrlEncodedContent(_postData),
		};
	}
}
