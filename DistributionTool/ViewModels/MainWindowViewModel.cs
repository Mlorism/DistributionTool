using DistributionTool.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributionTool.ViewModels
{
	class MainWindowViewModel : BaseViewModel
	{
		#region Commands	

		#endregion

		#region Properties	
		public static ICollection<ITab> Tabs { get; set; }
		//Tabs containing ViewModels to be loaded according to user's permissions
		#endregion

		#region Constructor
		public MainWindowViewModel()
		{
			LoadTabs();
		}
		#endregion

		#region Methods
		public static void LoadTabs()
		{
			Tabs = new ObservableCollection<ITab>()
			{
				new LoginViewModel(),
				new ProductsViewModel(),
				new DistributionViewModel(),
				new SummaryViewModel(),
				new SettingsViewModel()
			};
		} // LoadTabs() method load specific tabs to the collection
		#endregion
	}
}
