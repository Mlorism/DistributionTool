using DistributionTool.Cryptography;
using DistributionTool.Models;
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

			LoginCommand = new RelayCommand(LogInAction, null);
		}
		#endregion

		#region Methods
		/// <summary>
		/// First checks if the user with that name exist in database, next checks if password is correct, 
		/// when loggin is succesfull loads new tabs in MainWindowViewModel.
		/// </summary>		
		public static void LogInAction(object userPassword)
		{
			User user = MainWindowViewModel.Context.Users.FirstOrDefault(u => u.Name == UserName);
			var password = ((PasswordBox)userPassword).Password;

			if (user != null)
			{
				var encryptedPassword = PasswordEncryptor.GeneratePassword(password, user.PasswordSalt);

				if (user.Password.SequenceEqual(encryptedPassword))
				{					
					if (user.AccountActive == true)
					{
						MainWindowViewModel.LoggedInUser = user;
					}

					else MessageBox.Show("Konto zablokowane przez administratora.");

				}

				else MessageBox.Show("Zły login lub hasło!");

			}

			else
			MessageBox.Show("Nie ma takiego użytkownika!");

		} // LogInAction() 
		#endregion

		#region Methods validation



		#endregion

	}
}
