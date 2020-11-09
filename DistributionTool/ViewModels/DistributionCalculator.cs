using DistributionTool.Enumerators;
using DistributionTool.Models;
using DistributionTool.ViewModels.Lists;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
		#region Properties and clasess
		/// <summary>
		/// Structure that keeps information if store meets the requirements of the method
		/// </summary>
		class storeStatus
		{
			public int storeNo;
			public bool status;
			public static int summary;
			public int methodMinimum;

			public storeStatus(int storeNumber, bool stat)
			{
				storeNo = storeNumber;
				status = stat;
			}
			public storeStatus(int storeNumber, bool stat, int metMin)
			{
				storeNo = storeNumber;
				status = stat;
				methodMinimum = metMin;
			}
		} // storeStatus struct containing store number, store status and how many stores meet the requirements of the method
		#endregion

		#region CalculateDistribution() main method
		public static void CalculateDistribution(int PLUcode)
		{
			Product distributedProduct = ProductsListViewModel.Instance.ProductList.Where(x => x.PLU == PLUcode).FirstOrDefault();

			storeStatus.summary = 0; // zero because this is new distribution so each store needs to be analyzed at least once

			ObservableCollection<Distribution> SelectedProductDistributionList =
				new ObservableCollection<Distribution>(DistributionListViewModel.Instance.GetProduct(PLUcode).OrderBy(q => q.Grade));

			int effStock;
			float effCover;

			foreach (Distribution store in SelectedProductDistributionList)
			{
				effStock = store.EffectiveStock;
				effCover = store.EffectiveStock / store.AverageSales;

				store.DistributedPacks = 0;
				store.DistributedQuantity = 0;
				store.StockAfterDistribution = int.Parse(store.EffectiveStock.ToString());
				store.EffectiveCover = store.EffectiveStock / store.AverageSales;
				store.DistributionCover = float.Parse(store.EffectiveCover.ToString());
			}

			switch (distributedProduct.MethodOfDistribution)
			{
				case DistributionMethodEnum.KeepMinimum:
					KeepMinimumDistibution(SelectedProductDistributionList, distributedProduct);
					MainWindowViewModel.NotifyUser("KeepMinimum");
					break;

				case DistributionMethodEnum.WeeksOfSales:
					WeeksOfSalesDistibution(SelectedProductDistributionList, distributedProduct);
					MainWindowViewModel.NotifyUser("WeeksOfSales");
					break;

				case DistributionMethodEnum.GroupTrend:
					GroupTrendDistibution(SelectedProductDistributionList, distributedProduct);
					MainWindowViewModel.NotifyUser("GroupTrend");
					break;

				case DistributionMethodEnum.FinalDistribution:
					FinalDistributionDistibution(SelectedProductDistributionList, distributedProduct);
					MainWindowViewModel.NotifyUser("FinalDistribution");
					break;
			}

		} // CalculateDistribution()
		#endregion

		#region Distribution methods
		static void KeepMinimumDistibution(ObservableCollection<Distribution> distributionList, Product product)
		{
			int freePc = product.WarehouseFreeQty / product.PackSize; // calculate how many packs is available to distribution						

			List<storeStatus> statuses = new List<storeStatus>();

			foreach (Distribution store in distributionList)
			{
				statuses.Add(new storeStatus(store.StoreNumber, false));
			} // create new storeStatus for each store in distributionList

			if (freePc > 0)
				while (storeStatus.summary < distributionList.Count) // iterate until all stores have enought stock or there is no free stock to distribute
				{
					foreach (Distribution store in distributionList)
					{
						if (freePc == 0) break;

						if (statuses.Where(x => x.storeNo == store.StoreNumber).FirstOrDefault().status == false)
						{
							if (store.StockAfterDistribution < store.Min)
							{
								store.StockAfterDistribution += product.PackSize;
								store.DistributedQuantity += product.PackSize;
								store.DistributedPacks += 1;
								freePc -= 1;
							}

							if (store.StockAfterDistribution >= store.Min)
							{
								statuses.Where(x => x.storeNo == store.StoreNumber).FirstOrDefault().status = true;
								storeStatus.summary++;
								store.DistributionCover = store.StockAfterDistribution / store.AverageSales;
							}
						}
					} // foreach loop for all stores	

					if (freePc == 0) break;
				}

		} // KeepMinimumDistibution() calculate distribution according to Keep Minimum method

		static void WeeksOfSalesDistibution(ObservableCollection<Distribution> distributionList, Product product)
		{
			int freePc = product.WarehouseFreeQty / product.PackSize; // calculate how many packs is available to distribution

			List<storeStatus> statuses = new List<storeStatus>();

			foreach (Distribution store in distributionList)
			{
				int minMin = (int)(store.AverageSales * store.MinCover); // required store minimum based on average sales			
				if (store.Min > minMin) { minMin = store.Min; } // if minimum based on sales is smaller than store minimum then required min is based on store parameters			
				statuses.Add(new storeStatus(store.StoreNumber, false, minMin));
			} // create new storeStatus for each store in distributionList and calculate required store minimum					

			if (freePc > 0)
				while (storeStatus.summary < distributionList.Count) // iterate until all stores have enought stock or there is no free stock to distribute
				{
					foreach (Distribution store in distributionList)
					{
						if (freePc == 0) break;

						if (statuses.Where(x => x.storeNo == store.StoreNumber).FirstOrDefault().status == false)
						{
							if ((store.StockAfterDistribution + product.PackSize) <= store.Max)
							{
								if (store.StockAfterDistribution < statuses.Where(q => q.storeNo == store.StoreNumber).FirstOrDefault().methodMinimum)
								{
									store.StockAfterDistribution += product.PackSize;
									store.DistributedQuantity += product.PackSize;
									store.DistributedPacks += 1;
									freePc -= 1;
								}
							}

							if ((store.StockAfterDistribution >= statuses.Where(q => q.storeNo == store.StoreNumber).FirstOrDefault().methodMinimum)
								|| ((store.StockAfterDistribution + product.PackSize) > store.Max)
								|| (store.StockAfterDistribution >= store.Max))
							{
								statuses.Where(x => x.storeNo == store.StoreNumber).FirstOrDefault().status = true;
								storeStatus.summary++;
								store.DistributionCover = store.StockAfterDistribution / store.AverageSales;								
							}
						}
					} // foreach loop for every store				

					if (freePc == 0) break;
				}

		} // WeeksOfSalesDistibution() calculate distribution according to Weeks Of Sales method
		
		static void GroupTrendDistibution(ObservableCollection<Distribution> distributionList, Product product)
		{
			int freePc = product.WarehouseFreeQty / product.PackSize; // calculate how many packs is available to distribution

			List<storeStatus> statuses = new List<storeStatus>();			

			int week = MainWindowViewModel.Context.SalesWeek
				.Where(x => x.StartDate <= DateTime.Today && x.StopDate >= DateTime.Today).FirstOrDefault().Week; // get current week

			float previousSalesCurve = 0; // sales values for last 4 weeks from group sales curve

			for (int i = 1; i < 5; i++)
			{
				int loopWeek;
				loopWeek = week - i;
				if (loopWeek < 1) loopWeek += 52;
				if (loopWeek > 52) loopWeek -= 52;

				previousSalesCurve 
				+= MainWindowViewModel.Context.GroupCurve.Where(x => x.Group == product.GroupName && x.Week == (loopWeek)).FirstOrDefault().Value;
			} // calculate values for past 4 weeks of sales according to group curve

			foreach (Distribution store in distributionList)
			{
				float previousSalesQty = store.SlsLW + store.SlsLW1 + store.SlsLW2 + store.SlsLW3;
				
				float futureSales = 0;
				int loopWeek;				

				for (int i=0; i<store.MinCover; i++)
				{
					loopWeek = week + i;
					if (loopWeek < 1) loopWeek += 52;
					if (loopWeek > 52) loopWeek -= 52;

					futureSales
					+= MainWindowViewModel.Context.GroupCurve.Where(x => x.Group == product.GroupName && x.Week == (loopWeek)).FirstOrDefault().Value;
				}

				float minMin = previousSalesQty * futureSales / previousSalesCurve;
				if (store.Max > minMin) minMin = store.Max;
				// required store minimum calculated based on past sales and future curve	

				statuses.Add(new storeStatus(store.StoreNumber, false, (int)minMin));
			} // create new storeStatus for each store in distributionList and calculate required store minimum	
						
			while (storeStatus.summary < distributionList.Count && freePc > 0) // iterate until all stores have enought stock or there is no free stock to distribute
			{
				foreach (Distribution store in distributionList)
				{
					if (freePc == 0) break;

					if (statuses.Where(x => x.storeNo == store.StoreNumber).FirstOrDefault().status == false)
					{
						if ((store.StockAfterDistribution + product.PackSize) <= store.Max)
						{
							if (store.StockAfterDistribution < statuses.Where(q => q.storeNo == store.StoreNumber).FirstOrDefault().methodMinimum)
							{
								store.StockAfterDistribution += product.PackSize;
								store.DistributedQuantity += product.PackSize;
								store.DistributedPacks += 1;
								freePc -= 1;
							}
						}

						if ((store.StockAfterDistribution >= statuses.Where(q => q.storeNo == store.StoreNumber).FirstOrDefault().methodMinimum)
							|| ((store.StockAfterDistribution + product.PackSize) > store.Max)
							|| (store.StockAfterDistribution >= store.Max))
						{
							statuses.Where(x => x.storeNo == store.StoreNumber).FirstOrDefault().status = true;
							storeStatus.summary++;							
						}
					}
				} // foreach loop for every store				

				if (freePc == 0) break;
			}

			foreach (var store in distributionList)
			{
				store.DistributionCover = store.StockAfterDistribution / store.AverageSales;
			}
		} // GroupTrendDistibution() calculate distribution according to Group Trend method
		
		static void FinalDistributionDistibution(ObservableCollection<Distribution> distributionList, Product product)
		{
			int freePc = product.WarehouseFreeQty / product.PackSize; // calculate how many packs is available to distribution

			foreach (Distribution store in distributionList)
			{
				if (freePc == 0) break;
				if ((store.Grade == StoreGradeEnum.A || store.Grade == StoreGradeEnum.B)
					&& (store.StockAfterDistribution + product.PackSize) < store.Max)
				{
					store.StockAfterDistribution += product.PackSize;
					store.DistributedQuantity += product.PackSize;
					store.DistributedPacks += 1;
					freePc -= 1;
				}
			} // if only small amount of stock is avaible then distribute it to grade A and B

			List<storeStatus> statuses = new List<storeStatus>();

			if (freePc > 0)
			{				
				foreach (Distribution store in distributionList)
				{
					statuses.Add(new storeStatus(store.StoreNumber, false));
				} // create new storeStatus for each store in distributionList and calculate required store minimum		
			}

			while (freePc > 0 && storeStatus.summary < distributionList.Count)
			{
				foreach(Distribution store in distributionList)
				{
					if (freePc == 0) break;

					if ((store.StockAfterDistribution + product.PackSize) > store.Max)
					{
						statuses.Where(x => x.storeNo == store.StoreNumber).FirstOrDefault().status = true;
						storeStatus.summary ++;
					}

					if ((store.StockAfterDistribution + product.PackSize) <= store.Max)
					{
						if (store.Grade == StoreGradeEnum.A && freePc > 0)
						{
							for (int i=0; i<3; i++)
							{
								if (freePc == 0) break;

								if ((store.StockAfterDistribution + product.PackSize) <= store.Max)
								{
									store.StockAfterDistribution += product.PackSize;
									store.DistributedQuantity += product.PackSize;
									store.DistributedPacks += 1;
									freePc -= 1;
								}							
							}							
						} // Grade A

						if (store.Grade == StoreGradeEnum.B && freePc > 0)
						{
							for (int i = 0; i < 2; i++)
							{
								if (freePc == 0) break;

								if ((store.StockAfterDistribution + product.PackSize) <= store.Max)
								{
									store.StockAfterDistribution += product.PackSize;
									store.DistributedQuantity += product.PackSize;
									store.DistributedPacks += 1;
									freePc -= 1;
								}
							}
						} // Grade B

						if (store.Grade == StoreGradeEnum.C && freePc > 0)
						{	
							if ((store.StockAfterDistribution + product.PackSize) <= store.Max)
							{
								store.StockAfterDistribution += product.PackSize;
								store.DistributedQuantity += product.PackSize;
								store.DistributedPacks += 1;
								freePc -= 1;
							}
							
						} // Grade C
					} // if ((store.StockAfterDistribution + product.PackSize) <= store.Max) then store status true

				}
			} // while (freePc > 0 && storeStatus.summary < distributionList.Count)

			while (freePc > 0)
			{
				foreach (Distribution store in distributionList)
				{
					if (freePc == 0) break;									
										
					if (store.Grade == StoreGradeEnum.A)
					{
						for (int i = 0; i < 3; i++)
						{
							if (freePc == 0) break;				
							
							store.StockAfterDistribution += product.PackSize;
							store.DistributedQuantity += product.PackSize;
							store.DistributedPacks += 1;
							freePc -= 1;							
						}
					} // Grade A

					if (store.Grade == StoreGradeEnum.B)
					{
						for (int i = 0; i < 2; i++)
						{
							if (freePc == 0) break;
							
							store.StockAfterDistribution += product.PackSize;
							store.DistributedQuantity += product.PackSize;
							store.DistributedPacks += 1;
							freePc -= 1;							
						}
					} // Grade B

					if (store.Grade == StoreGradeEnum.C)
					{
						if (freePc == 0) break;						
						
						store.StockAfterDistribution += product.PackSize;
						store.DistributedQuantity += product.PackSize;
						store.DistributedPacks += 1;
						freePc -= 1;												
					} // Grade C

				} // foreach (Distribution store in distributionList)
			} // if there are free pcs after reaching max in stores, continue distribution

			foreach(var x in distributionList)
			{
				x.DistributionCover = x.StockAfterDistribution / x.AverageSales;
			}

		} // FinalDistributionDistibution() calculate distribution according to Final Distribution method
		#endregion

		#region Methods
		
		#endregion

	}




}
