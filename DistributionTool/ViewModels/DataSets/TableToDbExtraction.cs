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
		static public void LoadProductToDatabase(DataTable table)
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
		}
	}
}
