using DistributionTool.Enumerators;
using DistributionTool.Interfaces;
using DistributionTool.Models;
using DistributionTool.ViewModels.DataSets;
using DistributionTool.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using DistributionTool.Method_Extensions;
using DistributionTool.ViewModels.Lists;

namespace DistributionTool.ViewModels
{
	class MainWindowViewModel : BaseViewModel
	{
		#region Commands	
		public RelayCommand LogOutCommand { get; private set; }
		public RelayCommand ChangePasswordCommand { get; private set; }
		public RelayCommand LoadDataToDatabaseCommand { get; set; }
		#endregion

		#region Properties
		/// <summary>
		/// Tabs containing ViewModels to be loaded according to user's permissions
		/// </summary>
		public static IList<ITab> Tabs { get; set; }		

		private static ApplicationDbContext context = new ApplicationDbContext();
		
		public static ApplicationDbContext Context 
		{
			get
			{
				return context;
				///implement try catch 
			}

			set => context = value;
		}

		public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged;
		
		private static User loggedInUser;
		public static User LoggedInUser
		{
			get { return loggedInUser; }
			set 
			{ 				
				loggedInUser = value;
				RaiseStaticPropertyChanged("LoggedInUser");				
			}
		}				

		private static string notificationText;
		public static string NotificationText
		{
			get { return notificationText; }
			set 
			{ 
				notificationText = value;

				RaiseStaticPropertyChanged("NotificationText");
			}
		}		
		#endregion

		#region Constructor
		public MainWindowViewModel()
		{			
			LoadLoginPage();

			LogOutCommand = new RelayCommand(LogOut, null);
			ChangePasswordCommand = new RelayCommand(ChangePassword, null);
			LoadDataToDatabaseCommand = new RelayCommand(LoadDataToDatabase, null);
		}
		#endregion

		#region Methods		
		public static void LoadLoginPage()
		{
			if (Tabs == null)
			{
				Tabs = new ObservableCollection<ITab>();
			}

			if (Tabs.Count > 0)
			{
				Tabs.Clear();
			}		

			Tabs.Add(new LoginViewModel());			

			RaiseStaticPropertyChanged("Tabs");
		} // LoadTabs() create and load login page to the tab collection
		public static void LoadTabs()
		{
			if (Tabs == null)
			{
				Tabs = new ObservableCollection<ITab>();
			}

			if (Tabs.Count > 0)
			{
				Tabs.Clear();
			}

			Tabs.Add(new AdminViewModel());
			Tabs.Add(new ProductsViewModel());			
			Tabs.Add(new DistributionViewModel());
			Tabs.Add(new SummaryViewModel());
			Tabs.Add(new SettingsViewModel());

			RaiseStaticPropertyChanged("Tabs");			
		} // LoadTabs() method loads specific tabs to the collection
		public static void SaveContext()
		{
			Context.SaveChanges();
		} // SaveContext()
		public static void LogIn(object x)
		{
			LoggedInUser = (User)x;
			LoadTabs();
			//ClearData();
		} // LogIn()
		public static void LogOut(object x)
		{
			LoggedInUser = null;
			LoadLoginPage();			
		} // LogOut() current user		
		public static void ChangePassword(object x)
		{
			PasswordWindow passwordWindow = new PasswordWindow("Change Password", LoggedInUser.Id);
			passwordWindow.Show();
		} //ChangePassword()
		public static async void NotifyUser(string notification)
		{
			await ShowNotification(notification);
		} // NotifyUser()
		public static void RaiseStaticPropertyChanged(string PropertyName)
		{
			StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(PropertyName));
		} // RaiseStaticPropertyChanged()
		/// <summary>
		/// First load data from excel file, next export it to DistributionTool database tables.
		/// </summary>		
		public static void LoadDataToDatabase(object x)
		{						
			DataSet data = ExcelConnection.ImportFile("DataBaseData.xlsx");			
			TableToDbExtraction.ExportToDatabase(data);
		} // LoadDataToDatabase
		public static void ReloadContext()
		{
			context = new ApplicationDbContext();
		} // ReloadContext()

		public static void ClearData()
		{
			foreach (Distribution store in DistributionListViewModel.Instance.DistributionList)
			{
				store.DistributedPacks = 0;
				store.DistributedQuantity = 0;
				store.StockAfterDistribution = store.EffectiveStock;
				store.DistributionCover = store.EffectiveStock / store.AverageSales;
				Context.SaveChanges();
			}
		} // Clears all data created during previous session
		#endregion

		#region Tasks
		private static Task ShowNotification(string text)
		{
			return Task.Factory.StartNew(() =>
			{
				NotificationText = text;
				Thread.Sleep(6000);
				NotificationText = "";
			});
		} // ShowNotification()
		#endregion
	}

}
