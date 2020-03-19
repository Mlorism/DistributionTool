using DistributionTool.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributionTool.ViewModels
{
	class AdminViewModel : BaseViewModel, ITab
	{
		
		#region Constructor
		public AdminViewModel()
		{
			TabName = "Admin panel";
		}
		#endregion

	}
}
