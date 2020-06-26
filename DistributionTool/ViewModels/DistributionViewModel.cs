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

		#endregion


		#region Constructor
		public DistributionViewModel()
		{
			TabName = "Distribution";
			//createContext();
		}
		#endregion

		#region Methods

		public void createContext()
		{
			var context = MainWindowViewModel.Context;

			var list = (from sales in context.ProductSales
						join stock in context.ProductStock on
						new
						{
							PLUKey = sales.PLU,
							StoreNumberKey = sales.StoreNumber
						}
						equals
						new
						{
							PLUKey = stock.PLU,
							StoreNumberKey = stock.StoreNumber
						}
						into result
						from r in result.DefaultIfEmpty()
						select new {sales.PLU, sales.SlsLW, r.EffectiveStock} //dodać numer sklepów i resztę danych
						).ToList();

			//MessageBox.Show(list[0].PLU.ToString() + " " + list[0].SlsLW.ToString() +" "+ list[0].Stock.ToString());


			#endregion
		}
	}
}
