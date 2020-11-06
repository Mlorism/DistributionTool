using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using DistributionTool.Models;
using DistributionTool.ViewModels.Lists;

namespace DistributionTool.Converters
{
	class PLUToAvailableReservedPackConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is int PLU)
			{
				Product temp = ProductsListViewModel.Instance.ProductList.FirstOrDefault(x => x.PLU == PLU);
				int distributed;
				int loop;
				
				
			}

			else return "";
		} //Convert()

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return DependencyProperty.UnsetValue;
		}
	}
}
