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
	/// Converts ProductSubGroup to string.
	/// </summary>
	class ProductSubGroupToStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is ProductSubGroupEnum subGroup)
			{
				switch (subGroup)
				{
					case ProductSubGroupEnum.BathroomAccessories:
						return "Bathroom Accessories";
					case ProductSubGroupEnum.CardboardBoxes:
						return "Cardboard Boxes";
					case ProductSubGroupEnum.KitchenTools:
						return "Kitchen Tools";
					case ProductSubGroupEnum.MugsCups:
						return "Mugs & Cups";
					case ProductSubGroupEnum.PlatesBowls:
						return "Plates & Bowls";
					case ProductSubGroupEnum.PlaticBoxes:
						return "Platic Boxes";
					case ProductSubGroupEnum.PotsPans:
						return "Pots & Pans";
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
