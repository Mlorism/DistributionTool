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
		public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged;

		/// <summary>
		/// Selected product from productsFilteredList.
		/// </summary>
		private static Product selectedProduct;
		public static Product SelectedProduct
		{
			get { return selectedProduct; }
			set
			{ 
				selectedProduct = value;
				RaiseStaticPropertyChanged("SelectedProduct");
			}
		}

		/// <summary>
		/// Filtered ProductList
		/// </summary>
		public ICollectionView productsFilteredList { get; set; }
		#endregion

		#region Constructor
		public ProductsViewModel()
		{
			TabName = "Products";

			if (SelectedProduct == null)
			{
				SelectedProduct = ProductsListViewModel.Instance.ProductList.FirstOrDefault();
			}
			
			var productSourceList = new CollectionViewSource() { Source = ProductsListViewModel.Instance.ProductList };
			productsFilteredList = productSourceList.View;

			
			ChoseSelectedProductCommand = new RelayCommand(ChoseSelectedProduct, null);
		}
		#endregion

		#region Methods
		public static void RaiseStaticPropertyChanged(string PropertyName)
		{
			StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(PropertyName));
		} // RaiseStaticPropertyChanged()

		public void ChoseSelectedProduct(object product)
		{
			if (product != null)
			{
				SelectedProduct = ProductsListViewModel.Instance.ProductList.FirstOrDefault(x => x.PLU == ((Product)product).PLU);
			}
		} // ChoseSelectedProduct()
		#endregion

		#region Validators

		#endregion
	}
}
