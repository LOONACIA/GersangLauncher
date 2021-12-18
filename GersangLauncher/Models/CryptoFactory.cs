using System;
using System.Security.Cryptography;
using System.Text;

namespace GersangLauncher.Models
{
	public static class CryptoFactory
	{
		public static string Protect(string plainText, string entropy = null)
		{
			return Convert.ToBase64String(
				ProtectedData.Protect(
					Encoding.UTF8.GetBytes(plainText),
					entropy is null ? null : Encoding.UTF8.GetBytes(entropy),
					DataProtectionScope.CurrentUser));
		}

		public static string Unprotect(string cipherText, string entropy = null)
		{
			return Encoding.UTF8.GetString(
				ProtectedData.Unprotect(
					Convert.FromBase64String(cipherText),
					entropy is null ? null : Encoding.UTF8.GetBytes(entropy),
					DataProtectionScope.CurrentUser));
		}
	}
}
