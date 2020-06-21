using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;

namespace DistributionTool.ViewModels.DataSets
{ 
	class ExcelConnection
	{	
		#region Methods
		public static DataSet Import(string filename, bool headers = true)
		{
			File.AppendAllLines("LogAction.txt", new string[] { "Początek Importu", DateTime.Now.ToString() });
			
			var path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
			path = path.Substring(6, path.Length - 6) + @"\" + filename;			

			var excelApplication = new Excel.Application();
			var workbook = excelApplication.Workbooks.Open(@path);
			var sheets = workbook.Sheets;
			DataSet dataSet = null;

			File.AppendAllLines("LogAction.txt", new string[] { "Przed 1 if", DateTime.Now.ToString() });			

			if(sheets != null && sheets.Count > 0)
			{
				dataSet = new DataSet();

				File.AppendAllLines("LogAction.txt", new string[] { "Po 1 if", DateTime.Now.ToString() });
			

				foreach (var item in sheets)
				{
					File.AppendAllLines("LogAction.txt", new string[] { "ForEach", DateTime.Now.ToString() });
					
					var sheet = (Excel.Worksheet)item;
					System.Data.DataTable table = null;

					File.AppendAllLines("LogAction.txt", new string[] { "Przed 2 if", DateTime.Now.ToString() });
				
					if (sheet != null)
					{
						File.AppendAllLines("LogAction.txt", new string[] { "W 2 if", DateTime.Now.ToString() });						

						table = new System.Data.DataTable();
						var columnCount = ((Excel.Range)sheet.UsedRange.Rows[1, Type.Missing]).Columns.Count;
						var rowCount = ((Excel.Range)sheet.UsedRange.Columns[1, Type.Missing]).Rows.Count;

						File.AppendAllLines("LogAction.txt", new string[] { "Przed 1 for", DateTime.Now.ToString() });						

						for (int c = 0; c < columnCount; c++)
						{
							var cell = (Excel.Range)sheet.Cells[1, c + 1];
							var column = new DataColumn(headers ? cell.Value : string.Empty);
							table.Columns.Add(column);
						}

						File.AppendAllLines("LogAction.txt", new string[] { "Po 1 for", DateTime.Now.ToString() });
												
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

						File.AppendAllLines("LogAction.txt", new string[] { "Po 2 for", DateTime.Now.ToString() });
						
					}
					File.AppendAllLines("LogAction.txt", new string[] { "Po Pętlach i 1 IF", DateTime.Now.ToString() });
					
					dataSet.Tables.Add(table);
					File.AppendAllLines("LogAction.txt", new string[] { "Table added", DateTime.Now.ToString() });
				}
			}
			File.AppendAllLines("LogAction.txt", new string[] { "Ostatnie operacje", DateTime.Now.ToString() });
			
			excelApplication.Quit();			
			return dataSet;
		}

		#endregion
	}
}
