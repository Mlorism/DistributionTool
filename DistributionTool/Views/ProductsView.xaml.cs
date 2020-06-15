using DistributionTool.ViewModels;
using DistributionTool.ViewModels.Lists;
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

namespace DistributionTool.Views
{
	/// <summary>
	/// Interaction logic for ProductsView.xaml
	/// </summary>
	public partial class ProductsView : UserControl
	{
		public ProductsView()
		{
			InitializeComponent();
			DataContext = new ProductsViewModel();
			ProductSelection();
		}

		private void ProductSelection()
		{
			if (ProductsListViewModel.Instance.ProductList.Count > 0)
			{
				int selectionIndex = ProductsListViewModel.Instance.ProductList.IndexOf(ProductsViewModel.SelectedProduct);
				ProductsDataGrid.SelectedIndex = selectionIndex;
			}

			else return;
		} // ProductSelection()
	}
}
