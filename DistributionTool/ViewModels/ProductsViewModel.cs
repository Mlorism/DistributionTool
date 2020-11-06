using DistributionTool.Interfaces;
using DistributionTool.Models;
using DistributionTool.ViewModels.Lists;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
		/// Selected product from ProductsFilteredList.
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
		private static ObservableCollection<Product> productsFilteredList;
		public static ObservableCollection<Product> ProductsFilteredList
		{
			get { return productsFilteredList; }
			set 
			{	
				productsFilteredList = value;
				RaiseStaticPropertyChanged("ProductsFilteredList");
			}
		}

		#endregion

		#region Constructor
		public ProductsViewModel()
		{
			TabName = "Products";

			if (SelectedProduct == null)
			{
				SelectedProduct = ProductsListViewModel.Instance.ProductList.FirstOrDefault();
			}
			
			if (ProductsFilteredList == null)
			{				
				ProductsFilteredList = ProductsListViewModel.Instance.ProductList;
			}				
						
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
		public void ApplyChanges(object o)
		{
			Product contextProduct = new Product();
			Product viewProduct = new Product();
			List<Product> tempList = productsFilteredList.Cast<Product>().ToList();

			foreach(Product row in productsFilteredList)
			{
				contextProduct = MainWindowViewModel.Context.Products.Where(x => x.PLU == row.PLU).FirstOrDefault();
				viewProduct = tempList.Where(p => p.PLU == contextProduct.PLU).FirstOrDefault();
				contextProduct.MondayDistribution = viewProduct.MondayDistribution;
				contextProduct.TuesdayDistribution = viewProduct.TuesdayDistribution;
				contextProduct.WednesdayDistribution = viewProduct.WednesdayDistribution;
				contextProduct.ThursdayDistribution = viewProduct.ThursdayDistribution;
				contextProduct.FridayDistribution = viewProduct.FridayDistribution;
			}

			MainWindowViewModel.SaveContext();			
		} // ApplyChanges()

		/// <summary>
		/// Reload product list.
		/// </summary>		
		public void Refresh(object x)
		{
			ProductsListViewModel.Instance.Refresh();
			DistributedPLUPacksViewModel.Instance.Refresh();

			ProductsFilteredList = ProductsListViewModel.Instance.ProductList;

			CollectionViewSource.GetDefaultView(ProductsFilteredList).Refresh();

			RaiseStaticPropertyChanged("SelectedProduct");
		} // Refresh()
		public void ProductDistribution(object x)
		{
			DistributionCalculator.CalculateDistribution(SelectedProduct.PLU);			
		} // ProductDistribution()				

		#endregion

		#region Validators

		#endregion
	}
}
