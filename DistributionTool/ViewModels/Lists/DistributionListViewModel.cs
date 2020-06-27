using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributionTool.ViewModels.Lists
{
	/// <summary>
	/// ObservableCollection of product stocks, sales and distribution on every store.
	/// </summary>
	class DistributionListViewModel
	{
		#region Properties
		private readonly ObservableCollection<object> distributionList = new ObservableCollection<object>();
		public ObservableCollection<object> DistributionList => distributionList;
		#endregion

		#region Constructor
		private static readonly DistributionListViewModel instance = new DistributionListViewModel();
		public static DistributionListViewModel Instance => instance;
		#endregion

		#region Methods
		public void Refresh()
		{
			if (DistributionList.Count > 0)
			{
				DistributionList.Clear();
			}

			var context = MainWindowViewModel.Context;

			var distributionTable = (from sales in context.ProductSales.AsEnumerable()

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

						 select new	{	 
							 sales.PLU,
							 grade.Grade,
							 sales.SlsLW,
							 sales.SlsLW1,
							 sales.SlsLW2,
							 sales.SlsLW3,
							 sales.AverageSales,
							 stock.EffectiveStock,
							 distribution.StockAfterDistribution,
							 parameters.Min,
							 parameters.Max,
							 parameters.Cover,
							 distribution.DistributionCover,
							 distribution.DistributedQuantity,
							 distribution.DistributedPacks
															}).ToList();

			if (distributionTable != null)
			{
				foreach(var line in distributionTable)
				{
					DistributionList.Add(line);
				}
			}

		} // Refresh()
		#endregion
	}
}
