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
		#region Properties
		/// <summary>
		/// Filter on UserList
		/// </summary>		

		public User defaultUser = new User();
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

		}
		#endregion

		#region Methods
		private void FilterList()
		{
			Predicate<object> Filter = new Predicate<object>(item => ((User)item).Name.ToLower().Contains(FindUserText.ToLower()));
			// Predicate<object> Filter = new Predicate<object>(item => ((User)item).Name.ToLower().Contains(FindUserText.ToLower()));
			userFilteredList.Filter = Filter;
			OnPropertyChange("userFilteredList");
		}
		#endregion

	}
}
