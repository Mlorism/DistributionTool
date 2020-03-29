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

namespace DistributionTool.ViewModels
{
	class AdminViewModel : BaseViewModel, ITab
	{
		#region Commands
		public RelayCommand ChoseCurrentUserCommand { get; private set; }
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
		}
		#endregion

		#region Methods
		private void FilterList()
		{
			Predicate<object> Filter = new Predicate<object>(item => ((User)item).Name.ToLower().Contains(FindUserText.ToLower()));			
			userFilteredList.Filter = Filter;
			OnPropertyChange("userFilteredList");
		}

		public void ChoseCurrentUser(Object user)
		{
			User tempUser = UsersListViewModel.Instance.UsersList.FirstOrDefault(x => x.Id == ((User)user).Id);

			CurrentUser.Id = tempUser.Id;
			CurrentUser.Name = tempUser.Name;
			CurrentUser.Type = tempUser.Type;
			CurrentUser.AccountActive = tempUser.AccountActive;

			OnPropertyChange("CurrentUser");
		}
		#endregion

	}
}
