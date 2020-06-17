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
	/// Converts UserType to string.
	/// </summary>
	class UserTypeToStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is UserTypeEnum userType)
			{
				if (userType == UserTypeEnum.AllocationManager)
					return "Allocation Manager";
				else return userType.ToString();
			}

			else return value.ToString();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return DependencyProperty.UnsetValue;
		}
	}
}
