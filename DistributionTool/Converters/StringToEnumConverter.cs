using DistributionTool.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributionTool.Method_Extensions
{
	static class StringToEnumConverter
	{
		/// <summary>
		/// Returns ProdutGroupEnum based on number value as string.
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		public static ProductGroupEnum StringNumToGroup(string x)
		{
			switch (x)
			{
				case "0":
					return ProductGroupEnum.RoomDecorations;
				case "1":
					return ProductGroupEnum.KitchenDining;
				case "2":
					return ProductGroupEnum.Bathroom;
				case "3":
					return ProductGroupEnum.CleaningStorage;

				case "Bathroom":
					return ProductGroupEnum.Bathroom;
				case "RoomDecorations":
					return ProductGroupEnum.RoomDecorations;
				case "KitchenDining":
					return ProductGroupEnum.KitchenDining;
				case "CleaningStorage":
					return ProductGroupEnum.CleaningStorage;
				case "Other":
					return ProductGroupEnum.Other;

				default: return ProductGroupEnum.Other;
			}
		} // StringNumToGroup()

		/// <summary>
		/// Returns SubProdutGroupEnum based on number value as string.
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		public static ProductSubGroupEnum StringNumToSubGroup(string x)
		{
			switch (x)
			{
				case "0":
					return ProductSubGroupEnum.Candles;
				case "1":
					return ProductSubGroupEnum.Frames;
				case "2":
					return ProductSubGroupEnum.Vases;
				case "3":
					return ProductSubGroupEnum.Pillows;
				case "4":
					return ProductSubGroupEnum.Blankets;
				case "5":
					return ProductSubGroupEnum.PlatesBowls;
				case "6":
					return ProductSubGroupEnum.Cutlery;
				case "7":
					return ProductSubGroupEnum.KitchenTools;
				case "8":
					return ProductSubGroupEnum.PotsPans;
				case "9":
					return ProductSubGroupEnum.Glasses;
				case "10":
					return ProductSubGroupEnum.MugsCups;
				case "11":
					return ProductSubGroupEnum.BathroomAccessories;
				case "12":
					return ProductSubGroupEnum.Sponges;
				case "13":
					return ProductSubGroupEnum.Towels;
				case "14":
					return ProductSubGroupEnum.Mops;
				case "15":
					return ProductSubGroupEnum.Brushes;
				case "16":
					return ProductSubGroupEnum.Rags;
				case "17":
					return ProductSubGroupEnum.CardboardBoxes;
				case "18":
					return ProductSubGroupEnum.PlatesBowls;				
				default: 
					return ProductSubGroupEnum.Other;
			}
		} // StringNumToSubGroup()

		/// <summary>
		/// Returns DistributionMethodEnum based on number value as string.
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		public static DistributionMethodEnum StringNumToMethodOfDistribution(string x)
		{
			switch (x)
			{
				case "0":
					return DistributionMethodEnum.KeepMinimum;
				case "1":
					return DistributionMethodEnum.WeeksOfSales;
				case "2":
					return DistributionMethodEnum.GroupTrend;
				case "3":
					return DistributionMethodEnum.FinalDistribution;
				default:
					return DistributionMethodEnum.WeeksOfSales;
			}
		} // StringNumToMethodOfDistribution()

		/// <summary>
		/// Returns StoreGradeEnum based on number value as string.
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		public static StoreGradeEnum StringNumToStoreGrade(string x)
		{
			switch (x)
			{ 
				case "0":
				return StoreGradeEnum.A;
				case "1":
					return StoreGradeEnum.B;
				case "2":
					return StoreGradeEnum.C;
				default:
					return StoreGradeEnum.B;
			}
		} // StringNumToStoreGrade()



	}
}
