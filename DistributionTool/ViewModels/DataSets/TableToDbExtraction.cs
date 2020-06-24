﻿using System;
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
				StoresBelowMinimum = Convert.ToInt16(Row.Field<string>("StoresBelowMinimum")),
				StoresCover = Convert.ToSingle(Row.Field<string>("StoresCover")),
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
			MessageBox.Show("Product list loaded to the database.");
		} // LoadProduct()

		/// <summary>
		/// Load product parameters from table to database.
		/// </summary>
		/// <param name="table"></param>
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
			MessageBox.Show("Parameter list loaded to the database.");
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
			MessageBox.Show("Grade list loaded to the database.");

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
				SlsLW3 = Convert.ToInt16(Row.Field<string>("SlsLW3"))
			}).ToList();

			MainWindowViewModel.Context.ProductSales.AddRange(salesList);
			MainWindowViewModel.SaveContext();
			MessageBox.Show("Sales list loaded to the database.");

		} // LoadProductSales()

	}
}