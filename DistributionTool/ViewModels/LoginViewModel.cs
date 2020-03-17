using DistributionTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DistributionTool.ViewModels
{
	class LoginViewModel : BaseViewModel
	{
		#region Commands	

		#endregion

		#region Properties	
		public string UserName { get; set; }
		public string Password { get; set; }
		private int logingAttempts = 0;
		#endregion

		#region Connection to the database		
		private static ApplicationDbContext dbContext = new ApplicationDbContext();
		private static ApplicationDbContext context
		{
			get 
			{
				try
				{
					return dbContext;
				}

				catch (Exception)
				{
					return null;
					throw new Exception("Database connection error.");					
				}			
			}

			set => dbContext = value;
		}
		#endregion

		#region Constructor
		public LoginViewModel()
		{
			TabName = "Login";
		}
		#endregion

		#region Methods
		public static bool LogInAction(string name, string password)
		{
			if (context.Users.FirstOrDefault(u => u.Name == name) is User user)
			{
				return true;
			} // User name exist in database

			else
			{
				return false;
			} // There is no user with that name
		}
		#endregion
	}
}
