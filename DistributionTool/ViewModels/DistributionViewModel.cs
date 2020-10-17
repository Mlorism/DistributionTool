﻿using DistributionTool.Interfaces;
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
		public  static RelayCommand ReloadParametersCommand { get; set; }
		#endregion

		#region Properties
		public static Product SelectedProduct { get; set; } = ProductsListViewModel.Instance.ProductList.FirstOrDefault();
		public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged;

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

			SaveParametersCommand = new RelayCommand(SaveParameters, null);
			ReloadParametersCommand = new RelayCommand(ReloadParameters, null);
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

			Product methodProduct = MainWindowViewModel.Context.Products.Where(p => p.PLU == SelectedProduct.PLU).FirstOrDefault();
			methodProduct.MethodOfDistribution = SelectedProduct.MethodOfDistribution;

			MainWindowViewModel.SaveContext();
			ProductParameterListViewModel.Instance.Refresh();
			ReloadParameters(new object());
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
			
			// ... code to refresh DataGrid ...

		} // CreateDistibution() calculate distribution based on store parameters, method of distribution and available stock
				
		#endregion
	}
}
