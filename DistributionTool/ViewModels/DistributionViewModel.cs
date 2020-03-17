using DistributionTool.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributionTool.ViewModels
{
	class DistributionViewModel : BaseViewModel, ITab
	{

		#region Constructor
		public DistributionViewModel()
		{
			TabName = "Distribution";
		}
		#endregion

	}
}
