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
		public RelayCommand TodayDistributionCommand { get; set; }

		#endregion

		#region Properties
		public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged;

		static bool tabCreated = false;
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
			TodayDistributionCommand = new RelayCommand(TodayDistribution, null);

			if (tabCreated == false)
			{
				ProductEffectiveCoverCalculator();
				tabCreated = true;
			}			
			
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
			DistributedPLUPacksListViewModel.Instance.Refresh();

			StoresBelowMinimumCalculator();
			ProductsDistributionCoverCalculator();

			ProductsFilteredList = ProductsListViewModel.Instance.ProductList;

			CollectionViewSource.GetDefaultView(ProductsFilteredList).Refresh();

			RaiseStaticPropertyChanged("SelectedProduct");
		} // Refresh()
		public void ProductDistribution(object x)
		{
			DistributionCalculator.CalculateDistribution(SelectedProduct.PLU);
			Refresh(null);
		} // ProductDistribution()		
		public void TodayDistribution(object x)
		{
			DateTime today = DateTime.Now;			

			foreach (var line in ProductsFilteredList)
			{
				if (line.MondayDistribution == true && today.DayOfWeek == DayOfWeek.Monday) DistributionCalculator.CalculateDistribution(line.PLU);
				else if (line.TuesdayDistribution == true && today.DayOfWeek == DayOfWeek.Tuesday) DistributionCalculator.CalculateDistribution(line.PLU);
				else if (line.WednesdayDistribution == true && today.DayOfWeek == DayOfWeek.Wednesday) DistributionCalculator.CalculateDistribution(line.PLU);
				else if (line.ThursdayDistribution == true && today.DayOfWeek == DayOfWeek.Thursday) DistributionCalculator.CalculateDistribution(line.PLU);
				else if (line.FridayDistribution == true && today.DayOfWeek == DayOfWeek.Friday) DistributionCalculator.CalculateDistribution(line.PLU);
				else if ((today.DayOfWeek == DayOfWeek.Saturday || today.DayOfWeek == DayOfWeek.Sunday) && line.FridayDistribution == true) DistributionCalculator.CalculateDistribution(line.PLU);	
			}

			Refresh(null);

			MainWindowViewModel.NotifyUser("Today distribution finished.");
		} // TodayDistribution() creates distribution foreach product with selected current day of the week (if weekend then for friday)
		public void StoresBelowMinimumCalculator()
		{
			foreach(Product line in ProductsFilteredList)
			{
				line.StoresBelowMinimum = 0;
			}

			foreach (var line in DistributionListViewModel.Instance.DistributionList)
			{
				if (line.StockAfterDistribution < line.Min) productsFilteredList.FirstOrDefault(x => x.PLU == line.PLU).StoresBelowMinimum++;
			}
		} // StoresBelowMinimumCalculator() calculate how many stores does not meet minimum qty requirement
		public void ProductsDistributionCoverCalculator()
		{
			DistributionCoverListViewModel.Instance.Refresh();
		} // StoresDistributionCoverCalculator() calculate products distribution covers
		public static void ProductEffectiveCoverCalculator()
		{
			foreach (var line in ProductsFilteredList)
			{
				line.StoresEffectiveCover = 0;
			}

			foreach (var line in ProductsFilteredList)
			{
				float sales = 0;
				int stock = 0;

				foreach (var product in DistributionListViewModel.Instance.DistributionList)
				{
					if (product.PLU == line.PLU)
					{
						sales += product.AverageSales;
						stock += product.EffectiveStock;
					}
				}

				MainWindowViewModel.Context.Products.FirstOrDefault(x => x.PLU == line.PLU).StoresEffectiveCover = (stock / sales);				
			}

			MainWindowViewModel.SaveContext();
		} // ProductEffectiveCoverCalculator() calculate products effective covers

		#endregion		
	}
}
