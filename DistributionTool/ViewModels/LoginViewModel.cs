using DistributionTool.Cryptography;
using DistributionTool.Models;
using DistributionTool.ViewModels.Lists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DistributionTool.ViewModels
{
	class LoginViewModel : BaseViewModel
	{
		#region Commands
		public RelayCommand LoginCommand { get; private set; }
		#endregion

		#region Properties	
		public static string UserName { get; set; }
		public static string Password { get; set; }		
		#endregion
				
		#region Constructor
		public LoginViewModel()
		{
			TabName = "Login";
			UserName = "";
			Password = "";

			AdminExist();

			LoginCommand = new RelayCommand(LogInAction, null);
		}
		#endregion

		#region Methods
		/// <summary>
		/// After checking user and password existence, check if it's correct, then login user.
		/// </summary>		
		public static void LogInAction(object userPassword)
		{
			User user = MainWindowViewModel.Context.Users.FirstOrDefault(u => u.Name == UserName);
			var password = ((PasswordBox)userPassword).Password;

			if (user != null)
			{
				if (user.PasswordSalt != null)
				{
					var encryptedPassword = PasswordEncryptor.GeneratePassword(password, user.PasswordSalt);

					if (user.Password.SequenceEqual(encryptedPassword))
					{
						if (user.AccountActive == true)
						{
							MainWindowViewModel.LogIn(user);
						}

						else MainWindowViewModel.NotifyUser("Unable to sign in. The account has been locked. Please contact your administrator.");
					}
					else MainWindowViewModel.NotifyUser("Wrong login or password");
				}
				else MainWindowViewModel.NotifyUser("Unable to sign in. The account has no password assigned. Please contact your administrator.");
			}			
			else MainWindowViewModel.NotifyUser("Wrong login or password");
		} // LogInAction()

		/// <summary>
		/// Safeguard if developer forget user password. In case of not existing admin1 account new one is created. 
		/// </summary>
		public static void AdminExist()
		{
			User user = MainWindowViewModel.Context.Users.FirstOrDefault(u => u.Name == "Admin1");

			if (user == null)
			{
				MainWindowViewModel.NotifyUser("Admin1 accoutn doesn't exist.");

				var tempUser = new User();

				tempUser.Name = "Admin1";
				tempUser.PasswordSalt = PasswordEncryptor.GenerateSalt();
				tempUser.Password = PasswordEncryptor.GeneratePassword("Sauron666", tempUser.PasswordSalt);				
				tempUser.Type = Enumerators.UserType.Admin;
				tempUser.AccountActive = true;

				MainWindowViewModel.Context.Users.Add(tempUser);
				MainWindowViewModel.SaveContext();
				UsersListViewModel.Instance.Refresh();
			}

			else return;
	
		}
		#endregion

	

		#region Methods validation

		#endregion

	}
}
