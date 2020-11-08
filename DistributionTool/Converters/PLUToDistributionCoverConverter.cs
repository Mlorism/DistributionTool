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
	public class PLUToDistributionCoverConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is int)
			{
				return DistributionCoverListViewModel.Instance.DistributionCoverList.FirstOrDefault(x => x.PLU == (int)value).DisCover;
			}
			
			else return 0;

		} // Convert()

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return DependencyProperty.UnsetValue;
		}
	}
}
