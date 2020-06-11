using DistributionTool.Enumerators;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace DistributionTool.Converters
{
	/// <summary>
	/// Converts DistributionMethod to string.
	/// </summary>
	class DistributionMethodToStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is DistributionMethod method)
			{
				switch (method)
				{
					case DistributionMethod.KeepMinimum:
						return "Keep Minimum";
					case DistributionMethod.WeeksOfSales:
						return "Weeks Of Sales";
					case DistributionMethod.SubgroupTrend:
						return "Subgroup Trend";
					case DistributionMethod.GroupTrend:
						return "Group Trend";
					case DistributionMethod.FinalDistribution:
						return "Final Distribution";
					default:
						return value.ToString();
				}
			}

			else return value.ToString();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return DependencyProperty.UnsetValue;
		}
	}
}
