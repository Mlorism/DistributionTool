using DistributionTool.Interfaces;
using DistributionTool.Models;
using DistributionTool.ViewModels.Lists;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace DistributionTool.ViewModels
{
	class DistributionViewModel : BaseViewModel, ITab
	{
		#region Commands

		#endregion

		#region Properties
		public ProductModel SelectedProduct => ProductsViewModel.SelectedProduct;
		
		public ObservableCollection<ProductParametersModel> SelectedProductParameters
		{
			get
			{
				var subList = ProductParameterListViewModel.Instance.ParametersList.Where(x => x.PLU == SelectedProduct.PLU);				
				return new ObservableCollection<ProductParametersModel>(subList);  
			}			
		}

		#endregion


		#region Constructor
		public DistributionViewModel()
		{
			TabName = "Distribution";			
		}
		#endregion

		#region Methods

		#endregion

	}
}
