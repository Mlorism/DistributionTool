﻿using DistributionTool.Interfaces;
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

namespace DistributionTool.ViewModels
{
	class AdminViewModel : BaseViewModel, ITab
	{
		#region Commands
		public RelayCommand ChoseCurrentUserCommand { get; private set; }
		public RelayCommand ClearDataCommand { get; set; }
		public RelayCommand SaveUserCommand { get; set; }		
		#endregion

		#region Properties
		/// <summary>
		/// Filter on UserList
		/// </summary>		

		//public User currentUser = new User();
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
		
		public ICollectionView userFilteredList { get; set; }
		#endregion

		#region Constructor
		public AdminViewModel()
		{
			TabName = "Admin panel";

			var usersSourceList = new CollectionViewSource() { Source = UsersListViewModel.Instance.UsersList };
			userFilteredList = usersSourceList.View;

			CurrentUser = new User();
			ChoseCurrentUser(UsersListViewModel.Instance.UsersList.FirstOrDefault(x => x.Id == 1));			
						
			ChoseCurrentUserCommand = new RelayCommand(ChoseCurrentUser, null);
			ClearDataCommand = new RelayCommand(ClearData, null);
			SaveUserCommand = new RelayCommand(SaveUser, null);
		} // AdminViewModel()
		/// <summary>
		/// Constructor
		/// </summary>
		#endregion

		#region Methods
		private void FilterList()
		{
			Predicate<object> Filter = new Predicate<object>(item => ((User)item).Name.ToLower().Contains(FindUserText.ToLower()));			
			userFilteredList.Filter = Filter;
			OnPropertyChange("userFilteredList");
		} // FilterList()
		/// <summary>
		/// Filter user list according to the name in FindUserText.
		/// </summary>

		public void ChoseCurrentUser(object user)
		{
			User tempUser = UsersListViewModel.Instance.UsersList.FirstOrDefault(x => x.Id == ((User)user).Id);

			CurrentUser.Id = tempUser.Id;
			CurrentUser.Name = tempUser.Name;
			CurrentUser.Type = tempUser.Type;
			CurrentUser.AccountActive = tempUser.AccountActive;

			OnPropertyChange("CurrentUser");
		} //ChoseCurrentUser()
		/// <summary>
		/// Change CurrrentUser according to selection.
		/// </summary>

		public void ClearData(object x)
		{
			CurrentUser = new User()
			{
				Id = (UsersListViewModel.Instance.UsersList.Last().Id + 1)
			};
			OnPropertyChange("CurrentUser");
		} // ClearData()
		/// <summary>
		/// Clear data from CurrentUser for new user creation.
		/// </summary>
		
		public void SaveUser(object x)
		{
			if (CurrentUser.Id == (UsersListViewModel.Instance.UsersList.Last().Id + 1))
			{				
				User tempUser = new User();

				tempUser.Id = currentUser.Id;
				tempUser.Name = currentUser.Name;
				tempUser.Type = currentUser.Type;
				tempUser.AccountActive = currentUser.AccountActive;

				MainWindowViewModel.Context.Users.Add(tempUser);
				MainWindowViewModel.Context.SaveChanges();

				UsersListViewModel.Instance.Refresh();
			} // If CurrentUser is new user, create new account.

			else
			{
				 User tempUser = UsersListViewModel.Instance.UsersList.FirstOrDefault(u => u.Id == currentUser.Id);



			} // Else edit existing user data.
		} // SaveUser
		/// <summary>
		/// Creates new user and save it in the database or edit existing user data.
		/// </summary>

		//NewUserPassword

		#endregion

	}
}
