using DistributionTool.Enumerators;
using DistributionTool.Interfaces;
using DistributionTool.Models;
using DistributionTool.ViewModels.Lists;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributionTool.ViewModels
{
	class SummaryViewModel : BaseViewModel, ITab
	{
		#region Commands

		#endregion

		#region Properties and structures

		struct productSummary 
		{
			public int productNo;
			public ProductGroupEnum group;
			public ProductSubGroupEnum subgroup;
			public int qty;
			public double retail;			

			public productSummary(int PLU, ProductGroupEnum gr, ProductSubGroupEnum sgr) : this()
			{
				productNo = PLU;
				group = gr;
				subgroup = sgr;
			}
		} // product PLU, group, subgroup, distributed qty and retail

		struct groupSummary
		{
			ProductGroupEnum group;
			double retail;
		}

		struct subGroupSummary
		{
			ProductSubGroupEnum subgroup;
			double retail;
		}

		static ObservableCollection<productSummary> productSummaryList { get; set; }
		static ObservableCollection<groupSummary> groupList { get; set; }
		static ObservableCollection<subGroupSummary> subGroupList { get; set; }
		static ObservableCollection<ProductDistribution> productDistributionList { get; set; }

		#endregion


		#region Constructor
		public SummaryViewModel()
		{
			TabName = "Summary";
		}
		#endregion

		#region Methods
		public static void SaveDistribution()
		{
			var distributionList = MainWindowViewModel.Context.ProductDistribution.ToList();

			foreach (Distribution line in DistributionListViewModel.Instance.DistributionList)
			{
				distributionList.Find(x => x.PLU == line.PLU && x.StoreNumber == line.StoreNumber).DistributedPacks = line.DistributedPacks;
				distributionList.Find(x => x.PLU == line.PLU && x.StoreNumber == line.StoreNumber).DistributedQuantity = line.DistributedQuantity;
			}

			MainWindowViewModel.SaveContext();
		} // SaveDistribution() saving distribution from applicationb to database

		public void CalculateSummary(object x)
		{
			foreach (Product item in ProductsListViewModel.Instance.ProductList)
			{
				productSummaryList.Add(new productSummary(item.PLU, item.GroupName, item.SubGroup));
			} // creates productSummary for each product

			foreach (var line in productSummaryList)
			{
				foreach(var item in DistributionListViewModel.Instance.DistributionList.Where(q => q.PLU == line.productNo))
				{
					var temp = productSummaryList.Where(p => p.productNo == line.productNo).FirstOrDefault();
					temp.qty += item.DistributedQuantity;
				}
			} // sums up distribution quantity for each product in productSummaryList

			foreach (productSummary line in productSummaryList)
			{
				var temp = productSummaryList.Where(q => q.productNo == line.productNo).FirstOrDefault();
				temp.retail 
					= line.qty * ProductsListViewModel.Instance.ProductList.Where(q => q.PLU == line.productNo).FirstOrDefault().Price;
			} // calculate retail value for each product in productSummaryList


		} // CalculateSummary()
		#endregion

	}
}
