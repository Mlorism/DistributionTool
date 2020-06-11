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
				switch (method.ToString())
				{
					case "KeepMinimum":
						return "Keep Minimum";
					case "WeeksOfSales":
						return "Weeks Of Sales";
					case "SubgroupTrend":
						return "Subgroup Trend";
					case "GroupTrend":
						return "Group Trend";
					case "FinalDistribution":
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
