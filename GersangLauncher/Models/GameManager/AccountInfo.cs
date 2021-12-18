using System.Text.Json.Serialization;

namespace GersangLauncher.Models.GameManager
{
	public delegate string DecryptDelegate(string encryptedPassword);

	public record AccountInfo
	{
		[JsonPropertyName("id")]
		public string ID { get; set; }
		[JsonPropertyName("pw")]
		public string EncryptedPassword { get; set; }
		[JsonPropertyName("path")]
		public string Path { get; set; }
	}
}
