using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DistributionTool.Enumerators;

namespace DistributionTool.Models
{
	/// <summary>
	/// Class model representing distribution for selected product.
	/// </summary>
	public class Distribution
	{
		/// <summary>
		/// Price look-up code.
		/// </summary>
		public int PLU { get; set; }
		/// <summary>
		/// Store number.
		/// </summary>
		public int StoreNumber { get; set; }
		/// <summary>
		/// Store group grade.
		/// </summary>
		public StoreGradeEnum Grade { get; set; }
		/// <summary>
		/// Sale qty from last 7 days.
		/// </summary>
		public int SlsLW { get; set; }
		/// <summary>
		/// Sale qty from last 7 to 14 days.
		/// </summary>
		public int SlsLW1 { get; set; }
		/// <summary>
		/// Sale qty from last 14 to 21 days.
		/// </summary>
		public int SlsLW2 { get; set; }
		/// <summary>
		/// Sale qty from last 21 to 28 days.
		/// </summary>
		public int SlsLW3 { get; set; }
		/// <summary>
		/// Average sales based on last 4 weeks.
		/// </summary>
		public float AverageSales { get; set; }
		/// <summary>
		/// Product quantity in store with quantity that has been sent from warehouse.
		/// </summary>
		public int EffectiveStock { get; set; }
		/// <summary>
		/// Number of weeks of stock with current rate of sales with shipped stock.
		/// </summary>
		public float EffectiveCover { get; set; }
		/// <summary>
		/// Quantity of stock in the store plus quantity sent previously and with current distribution.
		/// </summary>
		public int StockAfterDistribution { get; set; }
		/// <summary>
		/// Minimum quantity for the store.
		/// </summary>
		public int Min { get; set; }
		/// <summary>
		/// Maximum quantity for the store.
		/// </summary>
		public int Max { get; set; }
		/// <summary>
		/// Minimum cover for the store.
		/// </summary>
		public float MinCover { get; set; }
		/// <summary>
		/// Number of weeks of stock with current rate of sales after distibution.
		/// </summary>
		public float DistributionCover { get; set; }
		/// <summary>
		/// Number of distributed items.
		/// </summary>
		public int DistributedQuantity { get; set; }
		/// <summary>
		/// Number of distributed packs.
		/// </summary>
		public int DistributedPacks { get; set; }
	}
}
