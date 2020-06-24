using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using DataTable = System.Data.DataTable;

namespace DistributionTool.ViewModels.DataSets
{ 
	class ExcelConnection
	{	
		#region Methods
		public static DataSet ImportFile(string filename, bool headers = true)
		{		
			var path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
			path = path.Substring(6, path.Length - 6) + @"\" + filename;			

			var excelApplication = new Excel.Application();
			var workbook = excelApplication.Workbooks.Open(@path);
			var sheets = workbook.Sheets;
			DataSet dataSet = null;				

			if(sheets != null && sheets.Count > 0)
			{
				dataSet = new DataSet();

				foreach (var item in sheets)
				{					
					var sheet = (Excel.Worksheet)item;
					System.Data.DataTable table = null;

					if (sheet != null)
					{						
						table = new System.Data.DataTable();
						var columnCount = ((Excel.Range)sheet.UsedRange.Rows[1, Type.Missing]).Columns.Count;
						var rowCount = ((Excel.Range)sheet.UsedRange.Columns[1, Type.Missing]).Rows.Count - (headers ? 1: 0);			

						for (int c = 0; c < columnCount; c++)
						{
							var cell = (Excel.Range)sheet.Cells[1, c + 1];
							var column = new DataColumn(headers ? cell.Value : string.Empty);
							table.Columns.Add(column);
						}
												
						for (int r = 0; r < rowCount; r++)
						{
							var row = table.NewRow();

							for (int k = 0; k < columnCount; k++)
							{
								var cell = (Excel.Range)sheet.Cells[r + 1 + (headers ? 1 : 0), k + 1];
								row[k] = cell.Value;
							}

							table.Rows.Add(row);
						}						
					}

					dataSet.Tables.Add(table);						
				}
			}
			
			excelApplication.Quit();			
			return dataSet;
		} // ImportFile()

		#endregion
	}
}
