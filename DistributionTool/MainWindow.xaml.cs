using DistributionTool.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DistributionTool
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		
		public MainWindow()
		{
			InitializeComponent();
			DataContext = new MainWindowViewModel();				
		}

		/// <summary>
		/// After change in Tabs collection if only one tab exist select it.
		/// </summary>	
		private void TabsUpdated(object sender, DataTransferEventArgs e)
		{
			if(ApplicationTabs.Items.Count == 1)
			{
				ApplicationTabs.SelectedIndex = 0;
			}
		} // ApplicationTabs()


	}
}
