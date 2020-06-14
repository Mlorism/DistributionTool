using DistributionTool.Interfaces;
using DistributionTool.Models;
using DistributionTool.ViewModels.Lists;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DistributionTool.ViewModels
{
	class ProductsViewModel : BaseViewModel, ITab
	{
		#region Commands
		public RelayCommand ChoseSelectedProductCommand { get; private set; }

		#endregion

		#region Properties


		/// <summary>
		/// Filtered ProductList
		/// </summary>
		public ICollectionView productsFilteredList { get; set; }
		#endregion

		#region Constructor
		public ProductsViewModel()
		{
			TabName = "Products";

			var productSourceList = new CollectionViewSource() { Source = ProductsListViewModel.Instance.ProductList };
			productsFilteredList = productSourceList.View;

			ChoseSelectedProductCommand = new RelayCommand(ChoseSelectedProduct, null);
		}
		#endregion

		#region Methods
		public void ChoseSelectedProduct(object product)
		{
			MainWindowViewModel.ChangeSelectedProduct(((Product)product).PLU);
		}
		#endregion

		#region Validators

		#endregion
	}
}
