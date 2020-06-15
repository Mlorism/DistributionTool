using DistributionTool.Interfaces;
using DistributionTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributionTool.ViewModels
{
	class DistributionViewModel : BaseViewModel, ITab
	{
		#region Commands

		#endregion

		#region Properties
		public Product SelectedProduct
		{
			get { return ProductsViewModel.SelectedProduct; }			
		}

		#endregion


		#region Constructor
		public DistributionViewModel()
		{
			TabName = "Distribution";

			
		}
		#endregion

		#region Methods

		#endregion

	}
}
