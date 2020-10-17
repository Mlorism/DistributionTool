using DistributionTool.Enumerators;
using DistributionTool.Models;
using DistributionTool.ViewModels.Lists;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DistributionTool.ViewModels
{
	/// <summary>
	/// Class contain algorithms for distribution methods 
	/// </summary>
	public static class DistributionCalculator
	{
		public static void CalculateDistribution(int PLUcode)
		{
			Product distributedProduct = ProductsListViewModel.Instance.ProductList.Where(x => x.PLU == PLUcode).FirstOrDefault();			

			ObservableCollection<Distribution> SelectedProductDistributionList = DistributionListViewModel.Instance.GetProduct(PLUcode);

			foreach (Distribution store in SelectedProductDistributionList)
			{
				store.DistributedPacks = 0;
				store.DistributedQuantity = 0;
				store.StockAfterDistribution = store.EffectiveStock;
				store.DistributionCover = store.EffectiveStock / store.AverageSales;
			}

			switch (distributedProduct.MethodOfDistribution) 
			{ 
				case DistributionMethodEnum.KeepMinimum:
					KeepMinimumDistibution(SelectedProductDistributionList, distributedProduct);
				break;
					
				case DistributionMethodEnum.WeeksOfSales:
					WeeksOfSalesDistibution(SelectedProductDistributionList, distributedProduct);
				break;

				case DistributionMethodEnum.GroupTrend:
					GroupTrendDistibution(SelectedProductDistributionList, distributedProduct);
				break;

				case DistributionMethodEnum.FinalDistribution:
					FinalDistributionDistibution(SelectedProductDistributionList, distributedProduct);
				break;
			}

		} // CalculateDistribution()

		static void KeepMinimumDistibution(ObservableCollection<Distribution> distributionList, Product product)
		{

			// qty na paczki i wtedy pętla na tej podstawie z break jeśli są spełnione min
			int freePc = product.WarehouseFreeQty / product.PackSize;
			int storeStatus = 0;

			while (storeStatus < distributionList.Count)
			{

				foreach (Distribution store in distributionList.Where(x => x.Grade == StoreGradeEnum.A))
				{
					if (freePc == 0) break;

					if (store.StockAfterDistribution < store.Min)
					{
						store.StockAfterDistribution = store.EffectiveStock + product.PackSize;
						store.DistributedPacks += 1;
					}

					else if (store.EffectiveStock >= store.Min)
					{
						storeStatus++;
					}
				}

				foreach (Distribution store in distributionList.Where(x => x.Grade == StoreGradeEnum.B))
				{
					if (freePc == 0) break;

					if (store.StockAfterDistribution < store.Min)
					{
						store.StockAfterDistribution += product.PackSize;
						store.DistributedPacks += 1;
					}

					else if (store.EffectiveStock >= store.Min)
					{
						storeStatus++;
					}
				}

				foreach (Distribution store in distributionList.Where(x => x.Grade == StoreGradeEnum.C))
				{
					if (freePc == 0) break;

					if (store.StockAfterDistribution < store.Min)
					{
						store.StockAfterDistribution += product.PackSize;
						store.DistributedPacks += 1;
					}

					else if (store.EffectiveStock >= store.Min)
					{
						storeStatus++;
					}
				}

				if (freePc == 0) break;				
			}

		} // KeepMinimumDistibution() calculate distribution according to Keep Minimum method










		static void WeeksOfSalesDistibution(ObservableCollection<Distribution> distributionList, Product product)
		{

			MessageBox.Show("Method not implemented");

		} // WeeksOfSalesDistibution() calculate distribution according to Weeks Of Sales method

		static void GroupTrendDistibution(ObservableCollection<Distribution> distributionList, Product product)
		{
			MessageBox.Show("Method not implemented");


		} // GroupTrendDistibution() calculate distribution according to Group Trend method

		static void FinalDistributionDistibution(ObservableCollection<Distribution> distributionList, Product product)
		{

			MessageBox.Show("Method not implemented");

		} // FinalDistributionDistibution() calculate distribution according to Final Distribution method
	}
}
