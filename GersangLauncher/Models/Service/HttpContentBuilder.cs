using System.Collections.Generic;
using System.Net.Http;

namespace GersangLauncher.Models.Service
{
	internal class HttpContentBuilder
	{
		private Dictionary<string, string> _postData;

		public HttpContentBuilder()
		{
			_postData = new Dictionary<string, string>();
		}

		public void Add(string key, string value) => _postData[key] = value;

		public void Clear() => _postData?.Clear();

		public HttpContent Build(ContentType contentType) => contentType switch
		{
			ContentType.FormData => new FormUrlEncodedContent(_postData),
			_ => new FormUrlEncodedContent(_postData),
		};
	}
}
