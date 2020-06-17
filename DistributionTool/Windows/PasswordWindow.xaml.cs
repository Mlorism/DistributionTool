using DistributionTool.Cryptography;
using DistributionTool.Models;
using DistributionTool.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DistributionTool.Windows
{
	/// <summary>
	/// Interaction logic for PasswordWindow.xaml
	/// </summary>
	public partial class PasswordWindow
	{
		#region Commands
		public RelayCommand SavePasswordCommand { get; private set; }
		#endregion

		#region Properties
		private int userId;
		#endregion

		#region Constructor
		public PasswordWindow(string title, int Id)
		{
			InitializeComponent();
			DataContext = this;
			this.Title = title;
			this.userId = Id;

			SavePasswordCommand = new RelayCommand(SavePassword, null);
		} // PasswordWindow()
		#endregion

		#region Methods
		private void Cancel_button_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		} // Cancel_button_Click()		


		public void SavePassword(object parameters)
		{
			var values = (object[])parameters;
			var passwordText = ((PasswordBox)values[0]).Password;
			var passwordConfirmation = ((PasswordBox)values[1]).Password;
			var regexExpression = new Regex(@"!|@|#|\$|%|\^|&|\*|\(|\)|-|_|=|\+"); 

			if (passwordText.Any(char.IsDigit)|| regexExpression.IsMatch(passwordText))
			{
				if (passwordText.Length >= 8)
				{
					if (passwordText == passwordConfirmation)
					{
						User selectedUser = MainWindowViewModel.Context.Users.FirstOrDefault(u => u.Id == userId);
						selectedUser.PasswordSalt = PasswordEncryptor.GenerateSalt();
						selectedUser.Password = PasswordEncryptor.GeneratePassword(passwordProposition.Password, selectedUser.PasswordSalt);
						MainWindowViewModel.SaveContext();
						selectedUser = new User();
						this.Close();
					}
					else MainWindowViewModel.NotifyUser("Those passwords didn't match. Try again.");
				}
				else MainWindowViewModel.NotifyUser("Password is to short, shoud contain at least 8 characters.");
			}
			else MainWindowViewModel.NotifyUser("Invalid password. Password shoud contain at least one number or special character.");
		} // Save_password()		
		#endregion

		#region Validators
		
		#endregion
	}
}
