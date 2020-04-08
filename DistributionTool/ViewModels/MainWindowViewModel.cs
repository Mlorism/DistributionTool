using DistributionTool.Interfaces;
using DistributionTool.Models;
using DistributionTool.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DistributionTool.ViewModels
{
	class MainWindowViewModel : BaseViewModel
	{
		#region Commands	

		#endregion

		#region Properties
		public static ICollection<ITab> Tabs { get; set; }
		/// <summary>
		/// Tabs containing ViewModels to be loaded according to user's permissions
		/// </summary>

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
				LoadTabs();
			}
		}


		#endregion

		#region Constructor
		public MainWindowViewModel()
		{
			LoadLoginPage();
		}
		#endregion

		#region Methods
		public static void RaiseStaticPropertyChanged (string PropertyName)
		{
			StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(PropertyName));
		} // RaiseStaticPropertyChanged()
		public static void LoadLoginPage()
		{
			Tabs = new ObservableCollection<ITab>()
			{
				new LoginViewModel()				
			};
		} // LoadTabs() create and load login page to the tab collection
		public static void LoadTabs()
		{
			if (Tabs != null)
				Tabs.Clear();

			else Tabs = new ObservableCollection<ITab>();

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

		#endregion
	}

}
