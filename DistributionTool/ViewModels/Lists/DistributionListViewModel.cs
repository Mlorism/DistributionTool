using DistributionTool.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DistributionTool.ViewModels.Lists
{
	/// <summary>
	/// ObservableCollection of product stocks, sales and distribution on every store.
	/// </summary>
	public class DistributionListViewModel
	{
		#region Properties
		private readonly ObservableCollection<Distribution> distributionList = new ObservableCollection<Distribution>();
		public ObservableCollection<Distribution> DistributionList => distributionList;
		#endregion

		#region Constructor
		private static readonly DistributionListViewModel instance = new DistributionListViewModel();
		public static DistributionListViewModel Instance => instance;
		static DistributionListViewModel() => Instance.Refresh();
		#endregion

		#region Methods
		/// <summary>
		/// Refresh data in DistributionList from database.
		/// </summary>
		public void Refresh()
		{
			if (DistributionList.Count > 0)
			{
				DistributionList.Clear();
			}

			var context = MainWindowViewModel.Context;

			// Select information from multiple tabeles to one and create list
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
							 sales.StoreNumber,
							 grade.Grade,
							 sales.SlsLW,
							 sales.SlsLW1,
							 sales.SlsLW2,
							 sales.SlsLW3,
							 sales.AverageSales,
							 stock.EffectiveStock,
							 stock.EffectiveCover,
							 distribution.StockAfterDistribution,
							 parameters.Min,
							 parameters.Max,
							 parameters.Cover,
							 distribution.DistributionCover,
							 distribution.DistributedQuantity,
							 distribution.DistributedPacks
															}).ToList();

			// Create Distribution instances and add to DistributionList
			if (distributionTable != null)
			{
				foreach(var line in distributionTable)
				{
					Distribution distribution = new Distribution();

					distribution.PLU = line.PLU;
					distribution.StoreNumber = line.StoreNumber;
					distribution.Grade = line.Grade;
					distribution.SlsLW = line.SlsLW;
					distribution.SlsLW1 = line.SlsLW1;
					distribution.SlsLW2 = line.SlsLW2;
					distribution.SlsLW3 = line.SlsLW3;
					distribution.AverageSales = line.AverageSales;
					distribution.EffectiveStock = line.EffectiveStock;
					distribution.EffectiveCover = line.EffectiveCover;
					distribution.StockAfterDistribution = line.StockAfterDistribution;
					distribution.Min = line.Min;
					distribution.Max = line.Max;
					distribution.MinCover = line.Cover;
					distribution.DistributionCover = line.DistributionCover;
					distribution.DistributedQuantity = line.DistributedQuantity;
					distribution.DistributedPacks = line.DistributedPacks;
					
					DistributionList.Add(distribution);
				}
			}

		} // Refresh()

		/// <summary>
		/// Select lines only for selected product.
		/// </summary>		
		public ObservableCollection<Distribution> GetProduct(int PLU)
		{
			ObservableCollection<Distribution> selectedDistribution = 
				new ObservableCollection<Distribution>(DistributionList.Where(x => x.PLU == PLU));
			
			return selectedDistribution;
		} // GetProduct()
		#endregion
	}
}
