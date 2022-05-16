using System.Text.Json.Serialization;

namespace GersangGameManager
{
	public delegate string DecryptDelegate(string encryptedPassword);

	public enum ServerType
	{
		Main = 0,
		Test
	}

	public record ClientInfo
	{
		[JsonPropertyName("id")]
		public string? ID { get; set; }
		[JsonPropertyName("pw")]
		public string? EncryptedPassword { get; set; }
		[JsonPropertyName("path")]
		public string? ClientPath { get; set; }
		[JsonPropertyName("server")]
		public ServerType ServerType { get; set; }
	}
}
