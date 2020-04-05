using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace DistributionTool.Cryptography
{
	class PasswordEncryptor
	{
		#region methods

		/// <summary>
		/// Generate hased password. Password byte array length have to be equal to the salt byte array length.
		/// </summary>	
		public static byte[] GeneratePassword(string password, byte[] salt)
		{
			Rfc2898DeriveBytes passwordGenerator = new Rfc2898DeriveBytes(password, salt);
			passwordGenerator.IterationCount = 12000;
			return passwordGenerator.GetBytes(24);
		} // GeneratePassword()

		/// <summary>
		/// Generate salt for hashing password. Salt byte array length have to be equal to the password byte array length.
		/// </summary>		
		public static byte[] GenerateSalt()
		{
			RNGCryptoServiceProvider saltGenerator = new RNGCryptoServiceProvider();
			byte[] salt = new byte[24];
			saltGenerator.GetBytes(salt);
			return salt;
		} // GenerateSalt()
		#endregion
	}
}
