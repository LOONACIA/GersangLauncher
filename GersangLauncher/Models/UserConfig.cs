using GersangLauncher.Models.GameManager;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GersangLauncher.Models
{
	public class UserConfig
	{
		[JsonPropertyName("Account")]
		public List<AccountInfo> AccountInfos { get; set; }
		[JsonPropertyName("Type")]
		public HandlerType HandlerType { get; set; }
		public bool SavePassword { get; set; } = true;
		[JsonPropertyName("UserCredential")]
		public bool UseUserCredential { get; set; } = true;

		private const int DefaultNumOfAccount = 3;

		public UserConfig()
		{
			AccountInfos = new List<AccountInfo>(DefaultNumOfAccount);
			AccountInfos.AddRange(new AccountInfo[DefaultNumOfAccount]);
		}

		public static UserConfig Open(string fileName)
		{
			if (!System.IO.File.Exists(fileName))
				return new UserConfig();

			string text = System.IO.File.ReadAllText(fileName);

			return JsonSerializer.Deserialize<UserConfig>(text);
		}

		public void Save(string fileName)
		{
			if (!SavePassword)
				AccountInfos.Where(x => !string.IsNullOrEmpty(x.EncryptedPassword)).ToList().ForEach(x => x.EncryptedPassword = string.Empty);
			var text = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
			System.IO.File.WriteAllText(fileName, text);
		}
	}
}
