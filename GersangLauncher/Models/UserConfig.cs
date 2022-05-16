using GersangGameManager;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GersangLauncher.Models
{
	public class UserConfig
	{
		[JsonPropertyName("Account")]
		public List<ClientInfo> ClientList { get; set; }

		[JsonPropertyName("Type")]
		public HandlerType HandlerType { get; set; }

		public bool SavePassword { get; set; } = true;

		[JsonPropertyName("UserCredential")]
		public bool UseUserCredential { get; set; } = true;

		public bool HideServerPanel { get; set; } = false;

		private const int DefaultNumOfAccount = 3;

		public UserConfig()
		{
			ClientList = new List<ClientInfo>(DefaultNumOfAccount);
			ClientList.AddRange(new ClientInfo[DefaultNumOfAccount]);
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
				ClientList.Where(x => !string.IsNullOrEmpty(x.EncryptedPassword)).ToList().ForEach(x => x.EncryptedPassword = string.Empty);
			var text = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
			System.IO.File.WriteAllText(fileName, text);
		}
	}
}
