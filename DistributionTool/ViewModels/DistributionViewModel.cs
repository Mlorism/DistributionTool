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
		public static RelayCommand createContextCommand { get; set; }
		#endregion

		#region Properties
		public Product SelectedProduct => ProductsViewModel.SelectedProduct;

		public ObservableCollection<ProductParameters> SelectedProductParameters
		{
			get
			{
				var subList = ProductParameterListViewModel.Instance.ParametersList.Where(x => x.PLU == SelectedProduct.PLU);
				return new ObservableCollection<ProductParameters>(subList);
			}
		}

		/// <summary>
		/// DistributionListViewModel only for selected product.
		/// </summary>
		public static ObservableCollection<Distribution> SelectedProductList { 
			get 
			{
				if (DistributionListViewModel.Instance.DistributionList == null)
				{
					DistributionListViewModel.Instance.Refresh();
				}							
				
				return DistributionListViewModel.Instance.GetProduct(ProductsViewModel.SelectedProduct.PLU);
			}
		} //SelectedProductList 

		#endregion


		#region Constructor
		public DistributionViewModel()
		{
			TabName = "Distribution";
			createContextCommand = new RelayCommand(createContext, null);
			
			
		}
		#endregion

		#region Methods

		public void createContext(object x)
		{
			DistributionListViewModel.Instance.GetProduct(189977);
		}

		#endregion
	}
}
