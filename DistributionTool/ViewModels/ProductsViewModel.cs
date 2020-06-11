using DistributionTool.Interfaces;
using DistributionTool.ViewModels.Lists;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DistributionTool.ViewModels
{
	class ProductsViewModel : BaseViewModel, ITab
	{
		#region Commands

		#endregion

		#region Properties

		/// <summary>
		/// Filtered ProductList
		/// </summary>
		public ICollectionView productsFilteredList { get; set; }
		#endregion

		#region Constructor
		public ProductsViewModel()
		{
			TabName = "Products";

			var productSourceList = new CollectionViewSource() { Source = ProductsListViewModel.Instance.ProductList };
			productsFilteredList = productSourceList.View;
		}
		#endregion

		#region Methods

		#endregion

		#region Validators

		#endregion
	}
}
