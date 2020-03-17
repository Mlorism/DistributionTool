using DistributionTool.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributionTool.ViewModels
{
	class SettingsViewModel : BaseViewModel, ITab
	{
		#region Constructor
		public SettingsViewModel()
		{
			TabName = "Settings";
		}
		#endregion
	}
}
