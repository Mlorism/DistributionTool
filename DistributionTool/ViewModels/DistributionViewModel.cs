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
		public  static RelayCommand ReloadParametersCommand { get; set; }
		#endregion

		#region Properties
		public Product SelectedProduct { get; set; } 
		public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged;

		public ICollectionView SelectedProductParameters { get; set; }
		
		/// <summary>
		/// DistributionListViewModel only for selected product.
		/// </summary>
		public static ObservableCollection<Distribution> SelectedProductList { 
			get 
			{				
				return DistributionListViewModel.Instance.GetProduct(ProductsViewModel.SelectedProduct.PLU);
			}
		} //SelectedProductList 
		#endregion

		#region Constructor
		public DistributionViewModel()
		{
			TabName = "Distribution";

			SelectedProduct = ProductsViewModel.SelectedProduct;

			var productParameterList = new CollectionViewSource()
			{
				Source = ProductParameterListViewModel.Instance.ParametersList
					.Where(p => p.PLU == SelectedProduct.PLU)
					.Select(c => ((ProductParameters)(c.Clone()))).ToList()
			};
			
			SelectedProductParameters = productParameterList.View;

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

			SelectedProductParameters = productParameterList.View;
			OnPropertyChange("SelectedProductParameters");
			SelectedProductParameters.Refresh();	

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


		#endregion
	}
}
