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
	/// Converts ProductGroup to string.
	/// </summary>
	class ProductGroupToStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is ProductGroupEnum group)
			{
				switch (group)
				{					
					case ProductGroupEnum.CleaningStorage:
						return "Cleaning & Storage";
					case ProductGroupEnum.KitchenDining:
						return "Kitchen & Dining";					
					case ProductGroupEnum.RoomDecorations:
						return "Room Decorations";
					default:
						return value.ToString();
				}
			}

			else if (value == null) { return null; }

			else return value.ToString();

		} // Convert()

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return DependencyProperty.UnsetValue;
		}
	}
}
