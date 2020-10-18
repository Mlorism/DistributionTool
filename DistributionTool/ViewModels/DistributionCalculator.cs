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
		#region structs
		/// <summary>
		/// Structure that keeps information if store meets the requirements of the method
		/// </summary>
		struct storeStatus
		{
			public int storeNo;
			public bool status;
			public static int summary;

			public storeStatus(int storeNumber, bool v) : this()
			{
				storeNo = storeNumber;
				status = v;
			}
		} // storeStatus struct containing store number, store status and how many stores meet the requirements of the method
		#endregion

		#region CalculateDistribution() main method
		public static void CalculateDistribution(int PLUcode)
		{
			Product distributedProduct = ProductsListViewModel.Instance.ProductList.Where(x => x.PLU == PLUcode).FirstOrDefault();			

			ObservableCollection<Distribution> SelectedProductDistributionList = DistributionListViewModel.Instance.GetProduct(PLUcode);

			foreach (Distribution store in SelectedProductDistributionList)
			{
				store.DistributedPacks = 0;
				store.DistributedQuantity = 0;
				store.StockAfterDistribution = store.EffectiveStock;
				store.EffectiveCover = store.EffectiveStock / store.AverageSales;
				store.DistributionCover = store.EffectiveCover;
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
		#endregion

		#region distribution methods
		static void KeepMinimumDistibution(ObservableCollection<Distribution> distributionList, Product product)
		{		
			int freePc = product.WarehouseFreeQty / product.PackSize; // calculate how many packs is available to distribution

			storeStatus.summary = 0; // zero because this is new distribution so each store needs to be analyzed at least once

			List<storeStatus> statuses = new List<storeStatus>();

			foreach (Distribution store in distributionList)
			{
				statuses.Add(new storeStatus(store.StoreNumber, false));
			} // create new storeStatus for each store in distributionList

			while (storeStatus.summary < distributionList.Count) // iterate until all stores have enought stock or there is no free stock to distribute
			{
				foreach (Distribution store in distributionList.Where(x => x.Grade == StoreGradeEnum.A))
				{
					if (freePc == 0) break;

					if (store.StockAfterDistribution < store.Min)
					{
						store.StockAfterDistribution += product.PackSize;
						store.DistributedQuantity += product.PackSize;
						store.DistributionCover = store.StockAfterDistribution / store.AverageSales;
						store.DistributedPacks += 1;
					}

					if (statuses.Where(x => x.storeNo == store.StoreNumber).FirstOrDefault().status == false)
					{
						if (store.StockAfterDistribution >= store.Min)
						{
							var stat = statuses.Where(x => x.storeNo == store.StoreNumber).FirstOrDefault();
							stat.status = true;
							storeStatus.summary++;
						}					
					}
				} // foreach loop for stores with grade A

				foreach (Distribution store in distributionList.Where(x => x.Grade == StoreGradeEnum.B))
				{
					if (freePc == 0) break;

					if (store.StockAfterDistribution < store.Min)
					{
						store.StockAfterDistribution += product.PackSize;
						store.DistributedQuantity += product.PackSize;
						store.DistributionCover = store.StockAfterDistribution / store.AverageSales;
						store.DistributedPacks += 1;
					}

					if (statuses.Where(x => x.storeNo == store.StoreNumber).FirstOrDefault().status == false)
					{
						if (store.StockAfterDistribution >= store.Min)
						{
							var stat = statuses.Where(x => x.storeNo == store.StoreNumber).FirstOrDefault();
							stat.status = true;
							storeStatus.summary++;
						}
					}
				} // foreach loop for stores with grade B

				foreach (Distribution store in distributionList.Where(x => x.Grade == StoreGradeEnum.C))
				{
					if (freePc == 0) break;

					if (store.StockAfterDistribution < store.Min)
					{
						store.StockAfterDistribution += product.PackSize;
						store.DistributedQuantity += product.PackSize;
						store.DistributionCover = store.StockAfterDistribution / store.AverageSales;
						store.DistributedPacks += 1;
					}

					if (statuses.Where(x => x.storeNo == store.StoreNumber).FirstOrDefault().status == false)
					{
						if (store.StockAfterDistribution >= store.Min)
						{
							var stat = statuses.Where(x => x.storeNo == store.StoreNumber).FirstOrDefault();
							stat.status = true;
							storeStatus.summary++;
						}
					}
				} // foreach loop for stores with grade C

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

	#endregion
}
