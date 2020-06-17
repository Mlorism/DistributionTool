using DistributionTool.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributionTool.ViewModels.Lists
{
	/// <summary>
	/// ObservableCollection of all products.
	/// </summary>
	class ProductsListViewModel
	{
		#region Properties
		private readonly ObservableCollection<ProductModel> productList = new ObservableCollection<ProductModel>();
		public ObservableCollection<ProductModel> ProductList => productList;
		#endregion

		#region Constructor
		private static readonly ProductsListViewModel instance = new ProductsListViewModel();
		public static ProductsListViewModel Instance => instance;
		static ProductsListViewModel() => Instance.Refresh();
		#endregion

		#region Methods
		public void Refresh()
		{
			if (ProductList.Count > 0)
				ProductList.Clear();

			var products = MainWindowViewModel.Context.Products.ToList();

			if (products != null)
			{
				foreach (var product in products)
				{
					ProductList.Add(product);
				}
			}
		} // Refresh()
		#endregion
	}
}
