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

		#endregion


		#region Constructor
		public DistributionViewModel()
		{
			TabName = "Distribution";
			createContextCommand = new RelayCommand(createContext, null);
			//createContext();
		}
		#endregion

		#region Methods

		public void createContext(object x)
		{
			var context = MainWindowViewModel.Context;

			var table = (from sales in context.ProductSales.AsEnumerable()

						 join stock in context.ProductStock.AsEnumerable()
						 on (sales.PLU, sales.StoreNumber) equals (stock.PLU, stock.StoreNumber)

						 join distribution in context.ProductDistribution.AsEnumerable()
						 on (sales.PLU, sales.StoreNumber) equals (distribution.PLU, distribution.StoreNumber)

						 join product in context.Products.AsEnumerable()
						 on (sales.PLU) equals (product.PLU)

						 join grade in context.StoresGrades.AsEnumerable()
						 on (sales.StoreNumber, product.GroupName) equals (grade.StoreNumber, grade.Group)

						 join parameters in context.ProductParameters.AsEnumerable()
						 on (sales.PLU, grade.Grade) equals (parameters.PLU, parameters.Grade)

						 select new { sales.PLU, stock.EffectiveCover }).ToList();

			MessageBox.Show(table.Count().ToString());

			//var list = (from sales in context.ProductSales
			//			join stock in context.ProductStock on
			//			new
			//			{
			//				PLUKey = sales.PLU,
			//				StoreNumberKey = sales.StoreNumber
			//			}
			//			equals
			//			new
			//			{
			//				PLUKey = stock.PLU,
			//				StoreNumberKey = stock.StoreNumber
			//			}
			//			into result
			//			from r in result.DefaultIfEmpty()
			//			select new { sales.PLU, sales.SlsLW, r.EffectiveStock } //dodać numer sklepów i resztę danych
			//			).ToList();

			//MessageBox.Show(list[0].PLU.ToString() + " " + list[0].SlsLW.ToString() +" "+ list[0].Stock.ToString());


			#endregion
		}
	}
}
