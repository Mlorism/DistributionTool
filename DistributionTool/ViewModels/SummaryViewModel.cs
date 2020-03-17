using DistributionTool.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributionTool.ViewModels
{
	class SummaryViewModel : BaseViewModel, ITab
	{
		#region Constructor
		public SummaryViewModel()
		{
			TabName = "Summary";
		}
		#endregion

	}
}
