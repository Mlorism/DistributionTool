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
		public string UserName { get; set; }
		public string Password { get; set; }		
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
			UserName = "";
			Password = "";

			LoginCommand = new RelayCommand(LogInAction, LogInActionValidation);
		}
		#endregion

		#region Methods
		public static void LogInAction(object x)
		{

			MainWindowViewModel.LoadTabs();
		}
		#endregion

		#region Methods validation

		public bool LogInActionValidation(object parameter)
		{
			PasswordBox boxPassword = parameter as PasswordBox;
			string password = boxPassword.Password;
			
			if (UserName != null & UserName.Length > 6 & password != null & password.Length > 7) return true;
			else return false;
		}

		#endregion

	}
}
