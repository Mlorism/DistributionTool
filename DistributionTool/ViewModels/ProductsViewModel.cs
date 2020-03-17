﻿using DistributionTool.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributionTool.ViewModels
{
	class ProductsViewModel : BaseViewModel, ITab
	{
		#region Constructor
		public ProductsViewModel()
		{
			TabName = "Products";
		}
		#endregion
	}
}
