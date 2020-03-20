using DistributionTool.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DistributionTool.ViewModels.Lists
{
	class UsersListViewModel
	{
		#region Properties
		private readonly ObservableCollection<User> usersList = new ObservableCollection<User>();
		public ObservableCollection<User> UsersList => usersList;

		ApplicationDbContext context = new ApplicationDbContext();
		#endregion

		#region Constructor
		private static readonly UsersListViewModel instance = new UsersListViewModel();
		public static UsersListViewModel Instance => instance;
		static UsersListViewModel() => Instance.Refresh();
		#endregion

		#region Methods
		public void Refresh()
		{
			if (UsersList.Count > 0) 
				UsersList.Clear();

			var users = context.Users.ToList();

			if (users != null)
				foreach (var user in users)
				{
					UsersList.Add(user);
				}		
		} // Refresh()
		#endregion
	}
}
