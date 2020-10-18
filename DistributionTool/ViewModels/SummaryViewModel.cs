using DistributionTool.Enumerators;
using DistributionTool.Interfaces;
using DistributionTool.Models;
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
			int productNo;
			ProductGroupEnum group;
			ProductSubGroupEnum subgroup;
			int qty;
			double retail;
		}

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
		static ObservableCollection<ProductDistribution> ProductDistributionList { get; set; }

		#endregion


		#region Constructor
		public SummaryViewModel()
		{
			TabName = "Summary";
		}
		#endregion

		#region Methods
		public void CalculateSummary(object x)
		{

		} // CalculateSummary()
		#endregion

	}
}
