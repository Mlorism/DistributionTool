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
	/// ObservableCollection model containing packs that are distributed from total amount in distribution center
	/// </summary>
	public class DistributedPLUPacksViewModel
	{
		#region Properties
		private readonly ObservableCollection<DistributedPLUPacks> distributedPacksList = new ObservableCollection<DistributedPLUPacks>();
		public ObservableCollection<DistributedPLUPacks> DistributedPacksList => distributedPacksList;
		#endregion

		#region Constructor
		private static DistributedPLUPacksViewModel instance = new DistributedPLUPacksViewModel();
		public static DistributedPLUPacksViewModel Instance => instance;

		static DistributedPLUPacksViewModel() => Instance.Refresh();	   
		#endregion

		#region Methods
		public void Refresh()
		{
			if (DistributedPacksList.Count > 0)
			{
				DistributedPacksList.Clear();
			}

			foreach (Product item in ProductsListViewModel.Instance.ProductList)
			{
				DistributedPacksList.Add(new DistributedPLUPacks(item.PLU, 0, item.WarehouseDistributedQty/item.PackSize));
			} // creates productSummary for each product

			foreach (var line in DistributedPacksList)
			{
				foreach (var item in DistributionListViewModel.Instance.DistributionList.Where(q => q.PLU == line.PLU))
				{
					line.DistributedPc += item.DistributedPacks;
				}
			}
		} // Refresh()

		#endregion
	}
}
