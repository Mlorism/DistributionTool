using DistributionTool.Models;
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
	class QuantityToPacksConverter: IValueConverter
	{		
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{

			if (value is Product)
			{
				Product prod = (Product)value;
				int packs = prod.WarehouseFreeQty / prod.PackSize;
				return packs;
			}
			
			else return null;				
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return DependencyProperty.UnsetValue;
		}
	}
}
