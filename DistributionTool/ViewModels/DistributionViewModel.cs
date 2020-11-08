using DistributionTool.Interfaces;
using DistributionTool.Models;
using DistributionTool.ViewModels.Lists;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace DistributionTool.ViewModels
{
	class DistributionViewModel : BaseViewModel, ITab
	{
		#region Commands
		public static RelayCommand SaveParametersCommand { get; set; }
		public static RelayCommand ReloadParametersCommand { get; set; }
		public static RelayCommand CreateDistibutionCommand { get; set; }

		public static RelayCommand ClearDistributionCommand { get; set; }

		#endregion

		#region Properties

		static bool tabCreated = false;

		public static Product selectedProduct;
		public static Product SelectedProduct 
		{
			get
			{
				return selectedProduct;
			}
			set
			{
				selectedProduct = value;
				RaiseStaticPropertyChanged("SelectedProduct");
			} 
		}
		public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged;
		public int DistributedPcs { get; set; }
		public int TotalPcs { get; set; } 
		
		public static ObservableCollection<ProductParameters> SelectedProductParameters { get; set; } = new ObservableCollection<ProductParameters>();

		/// <summary>
		/// DistributionListViewModel only for selected product.
		/// </summary>
		public static ObservableCollection<Distribution> SelectedProductList { get; set; } = new ObservableCollection<Distribution>();
		//SelectedProductList 
		#endregion

		#region Constructor
		public DistributionViewModel()
		{
			TabName = "Distribution";

			if (tabCreated == false)
			{ 
				SelectedProduct = ProductsListViewModel.Instance.ProductList.FirstOrDefault();
				tabCreated = true;
			}

			if (SelectedProduct.PLU != ProductsViewModel.SelectedProduct.PLU)
			{
				SelectedProduct = ProductsViewModel.SelectedProduct.Clone();								
			}

			if (SelectedProductParameters.Count == 0)
			{
				SelectedProductParameters = ProductParameterListViewModel.Instance.GetProductParameters(SelectedProduct.PLU);
			}

			if (SelectedProductParameters[0].PLU != ProductsViewModel.SelectedProduct.PLU)
			{
				SelectedProductParameters = ProductParameterListViewModel.Instance.GetProductParameters(SelectedProduct.PLU);
			}

			if (SelectedProductList.Count == 0)
			{
				SelectedProductList = DistributionListViewModel.Instance.GetProduct(ProductsViewModel.SelectedProduct.PLU); 
			}

			if (SelectedProductList[0].PLU != ProductsViewModel.SelectedProduct.PLU)
			{
				SelectedProductList = DistributionListViewModel.Instance.GetProduct(ProductsViewModel.SelectedProduct.PLU);
			}

			DistributedPcs = SelectedProductList.Sum(x => x.DistributedPacks);
			TotalPcs = SelectedProduct.WarehouseFreeQty / SelectedProduct.PackSize;

			SaveParametersCommand = new RelayCommand(SaveParameters, null);
			ReloadParametersCommand = new RelayCommand(ReloadParameters, null);
			CreateDistibutionCommand = new RelayCommand(CreateDistibution, null);
			ClearDistributionCommand = new RelayCommand(ClearDistribution, null);
		} // DistributionViewModel()
		#endregion

		#region Methods
		public static void RaiseStaticPropertyChanged(string PropertyName)
		{
			StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(PropertyName));
		} // RaiseStaticPropertyChanged()

		public void SaveParameters(object x)
		{
			List<ProductParameters> tempParamList = SelectedProductParameters.Cast<ProductParameters>().ToList();

			foreach (Enumerators.StoreGradeEnum g in Enum.GetValues(typeof(Enumerators.StoreGradeEnum)))
			{
				var ParameterRow = MainWindowViewModel.Context.ProductParameters
				.Where(p => p.PLU == SelectedProduct.PLU && p.Grade == g).FirstOrDefault();

				var currentParameter = tempParamList.Where(p => p.Grade == g).FirstOrDefault();

				ParameterRow.Min = currentParameter.Min;
				ParameterRow.Max = currentParameter.Max;
				ParameterRow.Cover = currentParameter.Cover;
			}

			MainWindowViewModel.Context.Products.FirstOrDefault(o => o.PLU == SelectedProduct.PLU).MethodOfDistribution
				= SelectedProduct.MethodOfDistribution;			

			MainWindowViewModel.SaveContext();

			ProductsListViewModel.Instance.Refresh();
			ProductParameterListViewModel.Instance.Refresh();

			ReloadParameters(null);
		} // SaveParameters()

		/// <summary>
		/// Reload parameters to SelectedProductParameters.
		/// </summary>		
		public void ReloadParameters(object x)
		{
			ProductParameterListViewModel.Instance.Refresh();

			var productParameterList = new CollectionViewSource()
			{
				Source = ProductParameterListViewModel.Instance.ParametersList
					.Where(p => p.PLU == SelectedProduct.PLU)
					.Select(c => ((ProductParameters)(c.Clone()))).ToList()
			};

			SelectedProductParameters = ProductParameterListViewModel.Instance.GetProductParameters(SelectedProduct.PLU);

			CollectionViewSource.GetDefaultView(SelectedProductParameters).Refresh();

			/// Reload selected product.
			var PLU = SelectedProduct.PLU;
			SelectedProduct = MainWindowViewModel.Context.Products.FirstOrDefault(p => p.PLU == PLU);
			
			//RaiseStaticPropertyChanged("SelectedProduct");
			OnPropertyChange("SelectedProduct");

		} // ReloadParameters()

		/// <summary>
		/// Returns reloaded parameters list.
		/// </summary>	
		public ObservableCollection<ProductParameters> ReloadedParametersList()
		{
			return new ObservableCollection <ProductParameters> 
				(ProductParameterListViewModel.Instance.ParametersList
					.Where(p => p.PLU == SelectedProduct.PLU)
					.Select(c => ((ProductParameters)(c.Clone()))).ToList());
		} //ReloadedParametersList()

		public void CreateDistibution(object x)
		{
			DistributionCalculator.CalculateDistribution(SelectedProduct.PLU);
			
			DistributedPcs = SelectedProductList.Sum(o => o.DistributedPacks);
			OnPropertyChange("DistributedPcs");
			CollectionViewSource.GetDefaultView(SelectedProductList).Refresh();
		} // CreateDistibution() calculate distribution based on store parameters, method of distribution and available stock

		public void ClearDistribution(object x)
		{
			foreach (Distribution store in SelectedProductList)
			{
				store.DistributedPacks = 0;
				store.DistributedQuantity = 0;
				store.StockAfterDistribution = store.EffectiveStock;
				store.EffectiveCover = store.EffectiveStock / store.AverageSales;
				store.DistributionCover = store.EffectiveCover;
			}
			DistributedPcs = 0;
			OnPropertyChange("DistributedPcs");
			MainWindowViewModel.SaveContext();
			CollectionViewSource.GetDefaultView(SelectedProductList).Refresh();
		} // ClearDistribution() 
		#endregion
	}
}
