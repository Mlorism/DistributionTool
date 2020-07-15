using DistributionTool.Interfaces;
using DistributionTool.Models;
using DistributionTool.ViewModels.Lists;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace DistributionTool.ViewModels
{
	class ProductsViewModel : BaseViewModel, ITab
	{
		#region Commands
		public RelayCommand ChoseSelectedProductCommand { get; private set; }
		public RelayCommand ApplyChangesCommand { get; private set; }
		public RelayCommand RefreshCommand { get; set; }
		public RelayCommand ProductDistributionCommand { get; set; }

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
			ApplyChangesCommand = new RelayCommand(ApplyChanges, null);
			RefreshCommand = new RelayCommand(Refresh, null);
			ProductDistributionCommand = new RelayCommand(ProductDistribution, null);
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

		/// <summary>
		/// Save changes to distribution method and days of distribution.
		/// </summary>		
		public void ApplyChanges(object x)
		{	
			MainWindowViewModel.SaveContext();			
		} // ApplyChanges()

		/// <summary>
		/// Reload product list.
		/// </summary>		
		public void Refresh(object x)
		{
			ProductsListViewModel.Instance.Refresh();			

			var productSourceList = new CollectionViewSource() { Source = ProductsListViewModel.Instance.ProductList };
			productsFilteredList = productSourceList.View;

			CollectionViewSource.GetDefaultView(productsFilteredList).Refresh();

			RaiseStaticPropertyChanged("SelectedProduct");
		} // Refresh()
		public void ProductDistribution(object x)
		{
			MessageBox.Show(SelectedProduct.MethodOfDistribution.ToString());
		} // ProductDistribution()				

		#endregion

		#region Validators

		#endregion
	}
}
