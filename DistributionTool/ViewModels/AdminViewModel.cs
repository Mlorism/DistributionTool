using DistributionTool.Interfaces;
using DistributionTool.Models;
using DistributionTool.ViewModels.Lists;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Data.Entity.Migrations;
using DistributionTool.Windows;
using MahApps.Metro.Controls;

namespace DistributionTool.ViewModels
{
	class AdminViewModel : BaseViewModel, ITab
	{
		#region Commands
		public RelayCommand ChoseCurrentUserCommand { get; private set; }
		public RelayCommand ClearDataCommand { get; private set; }
		public RelayCommand SaveUserCommand { get; private set; }	
		public RelayCommand ChangePasswordCommand { get; private set; }
		public RelayCommand DeleteUserCommand { get; private set; }
		#endregion

		#region Properties
		public User CurrentUser 
		{ 
			get
			{
				return currentUser;
			}

			set
			{
				currentUser = value;
			}
		}
		private User currentUser;		
		private string findUserText;
		public string FindUserText
		{
			get { return findUserText; }
			set 
			{ 
				findUserText = value;
				FilterList();				
			}
		}

		/// <summary>
		/// Filtered UserList
		/// </summary>
		public ICollectionView userFilteredList { get; set; }
		#endregion

		#region Constructor
		/// <summary>
		/// Constructor
		/// </summary>
		public AdminViewModel()
		{
			TabName = "Admin panel";

			var usersSourceList = new CollectionViewSource() { Source = UsersListViewModel.Instance.UsersList };
			userFilteredList = usersSourceList.View;

			CurrentUser = new User();
			ChoseCurrentUser(UsersListViewModel.Instance.UsersList.FirstOrDefault());			
						
			ChoseCurrentUserCommand = new RelayCommand(ChoseCurrentUser, null);
			ClearDataCommand = new RelayCommand(ClearData, null);
			SaveUserCommand = new RelayCommand(SaveUser, SaveUserValidation);			
			ChangePasswordCommand = new RelayCommand(ChangePassword, ChangePasswordValidation);
			DeleteUserCommand = new RelayCommand(DeleteUser, DeleteUserValitation);
		} // AdminViewModel()

		#endregion

		#region Methods
		/// <summary>
		/// Filter user list according to the name in FindUserText.
		/// </summary>
		private void FilterList()
		{
			Predicate<object> Filter = new Predicate<object>(item => ((User)item).Name.ToLower().Contains(FindUserText.ToLower()));			
			userFilteredList.Filter = Filter;
			OnPropertyChange("userFilteredList");
		} // FilterList()
		
		/// <summary>
		/// Change CurrrentUser according to selection.
		/// </summary>
		public void ChoseCurrentUser(object user)
		{
			if (user != null)
			{
				User tempUser = UsersListViewModel.Instance.UsersList.FirstOrDefault(x => x.Id == ((User)user).Id);

				if (tempUser != null)
				{
					CurrentUser.Id = tempUser.Id;
					CurrentUser.Name = tempUser.Name;
					CurrentUser.Type = tempUser.Type;
					CurrentUser.AccountActive = tempUser.AccountActive;
					CurrentUser.Password = null;
					CurrentUser.PasswordSalt = null;

					OnPropertyChange("CurrentUser");
				}

				else
				{
					return;
				}
			}

			else
			{
				return;
			}

		} //ChoseCurrentUser()		

		/// <summary>
		/// Clear data from CurrentUser for new user creation.
		/// </summary>
		public void ClearData(object x)
		{
			CurrentUser = new User()
			{
				Id = 0
			};
			OnPropertyChange("CurrentUser");
		} // ClearData()
		
		/// <summary>
		/// Creates new user and save it in the database or edit existing user account.
		/// </summary>
		public void SaveUser(object x)
		{
			ConfirmWindow confirmWindow; 

			if (CurrentUser.Id == 0)
			{
				confirmWindow = new ConfirmWindow("Create User", "Are you sure you want to create new user?");

				if (confirmWindow.AskQuestion())
				{
					var tempUser = new User();

					tempUser.Name = currentUser.Name;
					tempUser.Type = currentUser.Type;
					tempUser.AccountActive = currentUser.AccountActive;

					MainWindowViewModel.Context.Users.Add(tempUser);
					MainWindowViewModel.SaveContext();

					UsersListViewModel.Instance.Refresh();

					tempUser = MainWindowViewModel.Context.Users.FirstOrDefault(u => u.Name == CurrentUser.Name);
					ChoseCurrentUser(tempUser);
				}

				else return;
				
			} // If CurrentUser is new user, create new account.

			else
			{
				var editedUserName = MainWindowViewModel.Context.Users.FirstOrDefault(u => u.Id == CurrentUser.Id).Name;

				confirmWindow = new ConfirmWindow("Save User", "Are you sure you want to edit " + editedUserName + " data?");

				if (confirmWindow.AskQuestion())
				{
					var tempUser = MainWindowViewModel.Context.Users.FirstOrDefault(u => u.Id == currentUser.Id);

					tempUser.Name = currentUser.Name;
					tempUser.Type = currentUser.Type;
					tempUser.AccountActive = currentUser.AccountActive;

					MainWindowViewModel.SaveContext();
					UsersListViewModel.Instance.Refresh();
				}

				else 
				{
					ChoseCurrentUser(MainWindowViewModel.Context.Users.FirstOrDefault(u => u.Id == currentUser.Id));
				}
				
			} // Else edit existing user data.
		} // SaveUser()
		
		/// <summary>
		/// Delete selected user account from database.
		/// </summary>
		public void DeleteUser(object x)
		{
			ConfirmWindow confirmWindow = new ConfirmWindow("Delete User", "Are you sure you want to delete user " + CurrentUser.Name + "?");

			if (confirmWindow.AskQuestion())
			{
				var tempUser = MainWindowViewModel.Context.Users.FirstOrDefault(u => u.Id == CurrentUser.Id);

				MainWindowViewModel.Context.Users.Remove(tempUser);
				MainWindowViewModel.SaveContext();

				UsersListViewModel.Instance.Refresh();

				ClearData(new object());
			}

			else return;
		} // DeleteUser()
		
		/// <summary>
		/// Change selected user password.
		/// </summary>
		public void ChangePassword(object x)
		{
			PasswordWindow passwordWindow;
			
			if (CurrentUser.Id == 0)
			{
				passwordWindow = new PasswordWindow("Set Password", CurrentUser.Id);
			}

			else
				passwordWindow = new PasswordWindow("Change Password", CurrentUser.Id);

			passwordWindow.Show();
		} //ChangePassword()	

		#endregion
		#region Validators
		public bool SaveUserValidation(object x)
		{
			if (CurrentUser.Name == null ||CurrentUser.Name == ""||CurrentUser.Name.Length<6) return false;
			else return true;
		} // SaveUserValidation()
		public bool DeleteUserValitation(object x)
		{
			if (MainWindowViewModel.Context.Users.FirstOrDefault(u => u.Id == CurrentUser.Id) == null)
				return false;
			else 
			return true; //implement that logged in user cannot be deleted !!!
		} // DeleteUserValitation()		
		public bool ChangePasswordValidation(object x)
		{
			if (CurrentUser.Id == 0)
				return false;

			else return true;
		} // ChangePasswordValidation()		
		#endregion
	}
}
