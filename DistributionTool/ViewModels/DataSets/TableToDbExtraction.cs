using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DistributionTool.Converters;
using DistributionTool.Method_Extensions;
using DistributionTool.Models;

namespace DistributionTool.ViewModels.DataSets
{
	static public class TableToDbExtraction
	{
		public static void ExportToDatabase(DataSet data)
		{
			MainWindowViewModel.NotifyUser("0/6 Start export to database");
			if (MainWindowViewModel.Context.Products.Count() > 0)
			{
				MainWindowViewModel.Context.Database.ExecuteSqlCommand("TRUNCATE TABLE[Products]");
			} 			
			DataTable table = data.Tables[0];
			TableToDbExtraction.LoadProducts(table);
			MainWindowViewModel.NotifyUser("1/6 Product table loaded");

			if (MainWindowViewModel.Context.ProductParameters.Count() > 0)
			{
				MainWindowViewModel.Context.Database.ExecuteSqlCommand("TRUNCATE TABLE[ProductParameters]");
			}			
			table = data.Tables[1];
			TableToDbExtraction.LoadProductParameters(table);
			MainWindowViewModel.NotifyUser("2/6 Product parameters table loaded");

			if (MainWindowViewModel.Context.StoresGrades.Count() > 0)
			{
				MainWindowViewModel.Context.Database.ExecuteSqlCommand("TRUNCATE TABLE[StoreGrades]");
			}			
			table = data.Tables[2];
			TableToDbExtraction.LoadStoreGrades(table);
			MainWindowViewModel.NotifyUser("3/6 Store grades table loaded");

			if (MainWindowViewModel.Context.ProductSales.Count() > 0)
			{
				MainWindowViewModel.Context.Database.ExecuteSqlCommand("TRUNCATE TABLE[ProductSales]");
			}			
			table = data.Tables[3];
			TableToDbExtraction.LoadProductSales(table);
			MainWindowViewModel.NotifyUser("4/6 Sales table loaded");

			if (MainWindowViewModel.Context.ProductStock.Count() > 0)
			{
				MainWindowViewModel.Context.Database.ExecuteSqlCommand("TRUNCATE TABLE[ProductStocks]");
			}			
			table = data.Tables[4];
			TableToDbExtraction.LoadStoresStocks(table);
			MainWindowViewModel.NotifyUser("5/6 Stocks table loaded");

			if (MainWindowViewModel.Context.ProductDistribution.Count() > 0)
			{
				MainWindowViewModel.Context.Database.ExecuteSqlCommand("TRUNCATE TABLE[ProductDistributions]");
			}
			table = data.Tables[5];
			TableToDbExtraction.LoadDistributionTable(table);
			MainWindowViewModel.NotifyUser("6/6 Distribution table loaded");
		} // ExportToDatabase()

		/// <summary>
		/// Load product from table to database.
		/// </summary>
		/// <param name="table"></param>
		static public void LoadProducts(DataTable table)
		{
			var productList = table.AsEnumerable().Select(Row => new Product
			{
				PLU = Convert.ToInt32(Row.Field<string>("PLU")),
				Name = Row.Field<string>("Name"),
				GroupName = StringToEnumConverter.StringNumToGroup(Row.Field<string>("GroupName")),
				SubGroup = StringToEnumConverter.StringNumToSubGroup(Row.Field<string>("SubGroup")),
				Color = Row.Field<string>("Color"),
				Price = Convert.ToSingle(Row.Field<string>("Price")),
				PackSize = Convert.ToInt16(Row.Field<string>("PackSize")),
				Promotion = Row.Field<string>("Promotion"),
				WarehouseFreeQty = Convert.ToInt16(Row.Field<string>("WarehouseFreeQty")),
				WarehouseDistributedQty = Convert.ToInt16(Row.Field<string>("WarehouseDistributedQty")),
				StoresBelowMinimum = Convert.ToInt16(Row.Field<string>("StoresBelowMinimum")),				
				StoresEffectiveCover = Convert.ToSingle(Row.Field<string>("StoresEffectiveCover")),
				MondayDistribution = StringToBoolConverter.StringToBool(Row.Field<string>("Mon")),
				TuesdayDistribution = StringToBoolConverter.StringToBool(Row.Field<string>("Tue")),
				WednesdayDistribution = StringToBoolConverter.StringToBool(Row.Field<string>("Wed")),
				ThursdayDistribution = StringToBoolConverter.StringToBool(Row.Field<string>("Thu")),
				FridayDistribution = StringToBoolConverter.StringToBool(Row.Field<string>("Fri")),
				MethodOfDistribution = StringToEnumConverter.StringNumToMethodOfDistribution(Row.Field<string>("MethodOfDistribution"))
			}).ToList();

			MainWindowViewModel.Context.Products.AddRange(productList);
			MainWindowViewModel.SaveContext();			
		} // LoadProduct()

		/// <summary>
		/// Load product parameters from table to database.
		/// </summary>		
		public static void LoadProductParameters(DataTable table)
		{
			var parameterList = table.AsEnumerable().Select(Row => new ProductParameters
			{
				PLU = Convert.ToInt32(Row.Field<string>("PLU")),
				Grade = StringToEnumConverter.StringNumToStoreGrade(Row.Field<string>("Grade")),
				Min = Convert.ToInt16(Row.Field<string>("Min")),
				Max = Convert.ToInt16(Row.Field<string>("Max")),
				Cover = Convert.ToInt16(Row.Field<string>("Cover"))
			}).ToList();

			MainWindowViewModel.Context.ProductParameters.AddRange(parameterList);
			MainWindowViewModel.SaveContext();			
		} // LoadProductParameters()

		public static void LoadStoreGrades(DataTable table)
		{
			var storeGradeList = table.AsEnumerable().Select(Row => new StoreGrade
			{
				StoreNumber = Convert.ToInt16(Row.Field<string>("StoreNumber")),
				Group = StringToEnumConverter.StringNumToGroup(Row.Field<string>("Group")),
				Grade = StringToEnumConverter.StringNumToStoreGrade(Row.Field<string>("Grade"))
			}).ToList();

			MainWindowViewModel.Context.StoresGrades.AddRange(storeGradeList);
			MainWindowViewModel.SaveContext();			
		} // LoadStoreGrades()

		public static void LoadProductSales(DataTable table)
		{
			var salesList = table.AsEnumerable().Select(Row => new ProductSales 
			{ 
				PLU = Convert.ToInt32(Row.Field<string>("PLU")),
				StoreNumber = Convert.ToInt16(Row.Field<string>("StoreNumber")),
				SlsLW = Convert.ToInt16(Row.Field<string>("SlsLW")),
				SlsLW1 = Convert.ToInt16(Row.Field<string>("SlsLW1")),
				SlsLW2 = Convert.ToInt16(Row.Field<string>("SlsLW2")),
				SlsLW3 = Convert.ToInt16(Row.Field<string>("SlsLW3")),
				AverageSales = Convert.ToSingle(Row.Field<string>("AvgSls"))
			}).ToList();

			MainWindowViewModel.Context.ProductSales.AddRange(salesList);
			MainWindowViewModel.SaveContext();			
		} // LoadProductSales()

		public static void LoadStoresStocks(DataTable table)
		{
			var storesStocksList = table.AsEnumerable().Select(Row => new ProductStock
			{
				PLU = Convert.ToInt32(Row.Field<string>("PLU")),
				StoreNumber = Convert.ToInt16(Row.Field<string>("StoreNumber")),				
				EffectiveStock = Convert.ToInt16(Row.Field<string>("EffectiveStock")),
				EffectiveCover = Convert.ToSingle(Row.Field<string>("EffectiveCover"))

			}).ToList();

			MainWindowViewModel.Context.ProductStock.AddRange(storesStocksList);
			MainWindowViewModel.SaveContext();			
		} // LoadStoreStocks()
		
		public static void LoadDistributionTable(DataTable table)
		{
			var distributionList = table.AsEnumerable().Select(Row => new ProductDistribution
			{
				PLU = Convert.ToInt32(Row.Field<string>("PLU")),
				StoreNumber = Convert.ToInt16(Row.Field<string>("StoreNumber")),
				DistributionCover = Convert.ToSingle(Row.Field<string>("DistributionCover")),
				StockAfterDistribution = Convert.ToInt16(Row.Field<string>("StockAfterDistribution")),
				DistributedQuantity = Convert.ToInt16(Row.Field<string>("DistributedQuantity")),
				DistributedPacks = Convert.ToInt16(Row.Field<string>("DistributedPacks"))
			}).ToList();

			MainWindowViewModel.Context.ProductDistribution.AddRange(distributionList);
			MainWindowViewModel.SaveContext();
		} // LoadDistributionTable()

		public static void LoadCurvesAndWeeks(DataSet data)
		{
			MainWindowViewModel.NotifyUser("Start export to database");
			
			if (MainWindowViewModel.Context.GroupCurve.Count() > 0)
			{
				MainWindowViewModel.Context.Database.ExecuteSqlCommand("TRUNCATE TABLE[GroupCurve]");
			}
			DataTable table = data.Tables[0];
			//UpdateCurvesData(table);
			MainWindowViewModel.NotifyUser("Sales curves loaded to database.");

			if (MainWindowViewModel.Context.SalesWeek.Count() > 0)
			{
				MainWindowViewModel.Context.Database.ExecuteSqlCommand("TRUNCATE TABLE[SalesWeek]");
			}
			table = data.Tables[1];
			LoadWeeksBorders(table);
			MainWindowViewModel.NotifyUser("Weeks data loaded to database.");

		} //LoadCurvesAndWeeks()

		public static void UpdateCurvesData(DataTable table)
		{
			var curvesList = table.AsEnumerable().Select(Row => new GroupCurve
			{
				Group = StringToEnumConverter.StringNumToGroup(Row.Field<string>("Group")),
				Week = Convert.ToInt32(Row.Field<string>("Week")),
				Value = Convert.ToSingle(Row.Field<string>("Value"))
			}).ToList();

			MainWindowViewModel.Context.GroupCurve.AddRange(curvesList);
			//MainWindowViewModel.SaveContext();
		} // UpdateCurvesData()

		public static void LoadWeeksBorders(DataTable table)
		{
			var weeksList = table.AsEnumerable().Select(Row => new SalesWeek
			{
				Week = Convert.ToInt32	(Row.Field<string>("Week")),
				StartDate = Convert.ToDateTime(Row.Field<string>("Start")),
				StopDate = Convert.ToDateTime(Row.Field<string>("Stop"))
			}).ToList();

			MainWindowViewModel.Context.SalesWeek.AddRange(weeksList);
			MainWindowViewModel.SaveContext();

		} // UpdateCurvesData()

}
}
