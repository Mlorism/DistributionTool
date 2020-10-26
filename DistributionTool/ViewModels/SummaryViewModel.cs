using DistributionTool.Enumerators;
using DistributionTool.Interfaces;
using DistributionTool.Models;
using DistributionTool.ViewModels.Lists;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace DistributionTool.ViewModels
{
	class SummaryViewModel : BaseViewModel, ITab
	{
		#region Commands
		public static RelayCommand SaveDistributionCommand { get; set; }
		public static RelayCommand ClearDistributionCommand { get; set; }
		public static RelayCommand CalculateAllCommand { get; set; }
		
		#endregion

		#region Properties and classes

		public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged;
		public class productSummary
		{
			public int productNo { get; set; }
			public ProductGroupEnum group { get; set; }
			public ProductSubGroupEnum subgroup { get; set; }
			public int qty { get; set; }
			public double retail { get; set; }

			public productSummary(int PLU, ProductGroupEnum gr, ProductSubGroupEnum sgr)
			{
				productNo = PLU;
				group = gr;
				subgroup = sgr;
			}
		} // product PLU, group, subgroup, distributed qty and retail

		public class groupSummary
		{
			public ProductGroupEnum Group { get; set; }
			public double Retail { get; set; }
		}

		public class subGroupSummary
		{
			public ProductSubGroupEnum Subgroup { get; set; }
			public double Retail { get; set; }
		}

		public static ObservableCollection<productSummary> ProductSummaryList { get; set; }
		public static ObservableCollection<groupSummary> GroupList { get; set; }
		public static ObservableCollection<subGroupSummary> SubGroupList { get; set; }
		#endregion

		#region Constructor
		public SummaryViewModel()
		{
			TabName = "Summary";

			SaveDistributionCommand = new RelayCommand(SaveDistribution, null);
			ClearDistributionCommand = new RelayCommand(ClearDistribution, null);
			CalculateAllCommand = new RelayCommand(CalculateAll, null);
		}
		#endregion

		#region Methods
		public static void RaiseStaticPropertyChanged(string PropertyName)
		{
			StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(PropertyName));
		} // RaiseStaticPropertyChanged()
		public static void SaveDistribution(object o)
		{
			var distributionList = MainWindowViewModel.Context.ProductDistribution.ToList();

			foreach (Distribution line in DistributionListViewModel.Instance.DistributionList)
			{
				distributionList.Find(x => x.PLU == line.PLU && x.StoreNumber == line.StoreNumber).DistributedPacks = line.DistributedPacks;
				distributionList.Find(x => x.PLU == line.PLU && x.StoreNumber == line.StoreNumber).DistributedQuantity = line.DistributedQuantity;
			}

			MainWindowViewModel.SaveContext();
		} // SaveDistribution() saving distribution from application to database
		public static void ClearDistribution(object x)
		{
			var distributionList = MainWindowViewModel.Context.ProductDistribution.ToList();

			foreach (var line in distributionList)
			{
				line.DistributedPacks = 0;
				line.DistributedQuantity = 0;
			}

			MainWindowViewModel.SaveContext();

		} // ClearDistribution() clearing distribution from database

		public void CalculateSummary()
		{		
			ProductSummaryList = new ObservableCollection<productSummary>();
			
			foreach (Product item in ProductsListViewModel.Instance.ProductList)
			{
				ProductSummaryList.Add(new productSummary(item.PLU, item.GroupName, item.SubGroup));
			} // creates productSummary for each product
						
			foreach (var line in ProductSummaryList)
			{
				foreach (var item in DistributionListViewModel.Instance.DistributionList.Where(q => q.PLU == line.productNo))
				{
					productSummary temp = ProductSummaryList.Where(p => p.productNo == line.productNo).FirstOrDefault();
					temp.qty += item.DistributedQuantity;
				}
			} // sums up distribution quantity for each product in productSummaryList				

			foreach (productSummary line in ProductSummaryList)
			{
				var temp = ProductSummaryList.Where(q => q.productNo == line.productNo).FirstOrDefault();
				temp.retail
					= line.qty * ProductsListViewModel.Instance.ProductList.Where(q => q.PLU == line.productNo).FirstOrDefault().Price;
			} // calculate retail value for each product in productSummaryList
						
		} // CalculateSummary()

		public void CalculateGroupSummary()
		{
			GroupList = new ObservableCollection<groupSummary>();		

			foreach (ProductGroupEnum line in Enum.GetValues(typeof(ProductGroupEnum)))
			{
				groupSummary temp = new groupSummary();
				temp.Group = line;
				GroupList.Add(temp);
			}  // creates groupSummary for each group

			foreach (var line in ProductSummaryList)
			{
				var group = GroupList.Where(q => q.Group == line.group).FirstOrDefault();
				group.Retail += line.retail;
			} // calculate retail value for group in groupList
		} //  CalculategroupSummary()

		public void CalculateSubGroupSummary()
		{
			SubGroupList = new ObservableCollection<subGroupSummary>();

			foreach (ProductSubGroupEnum line in Enum.GetValues(typeof(ProductSubGroupEnum)))
			{
				subGroupSummary temp = new subGroupSummary();
				temp.Subgroup = line;
				SubGroupList.Add(temp);
			}  // creates subGroupSummary for each subgroup

			foreach (var line in ProductSummaryList)
			{
				var subgroup = SubGroupList.Where(q => q.Subgroup == line.subgroup).FirstOrDefault();
				subgroup.Retail += line.retail;
			} // calculate retail value for subGroup in subGroupList
		} // CalculateSubGroupSummary()

		public void CalculateAll(object x)
		{
			CalculateSummary();
			CalculateGroupSummary();
			CalculateSubGroupSummary();
			RaiseStaticPropertyChanged("ProductSummaryList");
			RaiseStaticPropertyChanged("GroupList");
			RaiseStaticPropertyChanged("SubGroupList");				
		}

		///////////////////////

		// podpięcie/przepięcie kolekcji pod grid/gridy w View

		///////////////////////

		#endregion

	}
}
