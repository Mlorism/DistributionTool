using DistributionTool.Interfaces;
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
		//Tabs containing ViewModels to be loaded according to user's permissions
		public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged;
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
		}
		public static void LoadLoginPage()
		{
			Tabs = new ObservableCollection<ITab>()
			{
				//new LoginViewModel(),
				new AdminViewModel()
			};
		} // LoadTabs() method loads login page to the tab collection

		public static void LoadTabs()
		{
			Tabs.Clear();			
			Tabs.Add(new AdminViewModel());
			Tabs.Add(new ProductsViewModel());
			Tabs.Add(new DistributionViewModel());
			Tabs.Add(new SummaryViewModel());
			Tabs.Add(new SettingsViewModel());

			RaiseStaticPropertyChanged("Tabs");
			
		} // LoadTabs() method loads specific tabs to the collection
		
		#endregion
	}

}
