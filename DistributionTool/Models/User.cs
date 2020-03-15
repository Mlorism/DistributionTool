using DistributionTool.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DistributionTool.Models
{
	/// <summary>
	/// Class model representing application user.
	/// </summary>
	class User
	{	/// <summary>
		/// Identification numer of a user.
		/// </summary>
		public int Id { get; set; }
		/// <summary>
		/// Name of the user. It is recommended to use the first and last name as the username, and if the number of users is large, use also the middle name.
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// Hashed password with salt.
		/// </summary>
		public byte[] Password { get; set; }
		/// <summary>
		/// Generated password salt key for additional protection.
		/// </summary>
		public byte[] PasswordSalt { get; set; }
		/// <summary>
		/// User type is associated with access to specific parts of the application.
		/// </summary>
		public UserType Type { get; set; }
		/// <summary>
		/// User account could be active or blocked.
		/// </summary>
		public bool AccountActive { get; set; }
	}
}
