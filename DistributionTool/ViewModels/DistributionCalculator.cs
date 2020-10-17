using DistributionTool.Enumerators;
using DistributionTool.ViewModels.Lists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributionTool.ViewModels
{
	public static class DistributionCalculator
	{

		public static void CalculateDistribution(int PLUcode)
		{
			DistributionMethodEnum method 
				= ProductsListViewModel.Instance.ProductList.Where(x => x.PLU == PLUcode).FirstOrDefault().MethodOfDistribution;
				
			switch (method) 
			{ 
				case DistributionMethodEnum.KeepMinimum:
					KeepMinimumDistibution(PLUcode);
				break;
					
				case DistributionMethodEnum.WeeksOfSales:
					WeeksOfSalesDistibution(PLUcode);
				break;

				case DistributionMethodEnum.GroupTrend:
					GroupTrendDistibution(PLUcode);
				break;

				case DistributionMethodEnum.FinalDistribution:
					FinalDistributionDistibution(PLUcode);
				break;
			}
		} // CalculateDistribution()

		static void KeepMinimumDistibution(int PLUcode)
		{

		} // KeepMinimumDistibution() calculate distribution according to Keep Minimum method

		static void WeeksOfSalesDistibution(int PLUcode)
		{

		} // WeeksOfSalesDistibution() calculate distribution according to Weeks Of Sales method

		static void GroupTrendDistibution(int PLUcode)
		{

		} // GroupTrendDistibution() calculate distribution according to Group Trend method

		static void FinalDistributionDistibution(int PLUcode)
		{

		} // FinalDistributionDistibution() calculate distribution according to Final Distribution method
	}
}
