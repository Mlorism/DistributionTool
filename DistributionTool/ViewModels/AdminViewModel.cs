using DistributionTool.Interfaces;
using DistributionTool.ViewModels.Lists;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributionTool.ViewModels
{
	class AdminViewModel : BaseViewModel, ITab
	{
		#region Properties
		private readonly ICollectionView usersFilter;

		#endregion

		#region Constructor
		public AdminViewModel()
		{
			TabName = "Admin panel";			
		}
		#endregion

	}
}
