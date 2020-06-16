using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using DistributionTool.Models;

namespace DistributionTool.ViewModels.Lists
{
	/// <summary>
	/// ObservableCollection of all products parameters.
	/// </summary>
	class ProductParameterListViewModel
	{
		#region Properties
		private readonly ObservableCollection<ProductParameters> parametersList = new ObservableCollection<ProductParameters>();
		public ObservableCollection<ProductParameters> ParametersList => parametersList;
		#endregion

		#region Constructor
		private static readonly ProductParameterListViewModel instance = new ProductParameterListViewModel();
		public static ProductParameterListViewModel Instance => instance;
		static ProductParameterListViewModel() => Instance.Refresh();
		#endregion


		#region Methods
		public void Refresh()
		{
			if (ParametersList.Count > 0)
				ParametersList.Clear();

			var parameters = MainWindowViewModel.Context.ProductParameters.ToList();

			if (parameters != null)
			{
				foreach (var param in parameters)
				{
					ParametersList.Add(param);
				}
			}
		} // Refresh()
		#endregion
	}
}
