using DistributionTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributionTool.ViewModels.Lists
{
	/// <summary>
	/// List containing distribution covers for products
	/// </summary>
	public class DistributionCoverListViewModel
	{
		#region Properties
		private readonly List<DistributionCover> distributionCoverList = new List<DistributionCover>();
		public List<DistributionCover> DistributionCoverList => distributionCoverList;
		#endregion

		#region Constructor
		private static readonly DistributionCoverListViewModel instance = new DistributionCoverListViewModel();
		public static DistributionCoverListViewModel Instance => instance;
		static DistributionCoverListViewModel() => Instance.Refresh();		
		#endregion
		
		#region Methods
		public void Refresh()
		{
			if (DistributionCoverList.Count > 0) DistributionCoverList.Clear();

			foreach (Product line in ProductsListViewModel.Instance.ProductList)
			{
				DistributionCoverList.Add(new DistributionCover(line.PLU, 0));
			}


			foreach (Product line in ProductsListViewModel.Instance.ProductList)
			{
				float sales = 0;
				int stock = 0;

				foreach (var distribution in DistributionListViewModel.Instance.DistributionList)
				{
					if (distribution.PLU == line.PLU)
					{
						sales += distribution.AverageSales;
						stock += distribution.StockAfterDistribution;
					} 
				}

				DistributionCoverList.FirstOrDefault(x => x.PLU == line.PLU).DisCover = (stock/sales);
			}		
		}
		#endregion
	}
}
