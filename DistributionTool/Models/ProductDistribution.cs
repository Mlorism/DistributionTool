using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributionTool.Models
{
	/// <summary>
	/// Class model representing distribution.
	/// </summary>
	class ProductDistribution
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
		/// Number of distributed items.
		/// </summary>
		public int DistributedQuantity { get; set; }
		/// <summary>
		/// Number of distributed packs.
		/// </summary>
		public int DistributedPacks { get; set; }
		/// <summary>
		///  Number of weeks of stock with current rate of sales after distibution.
		/// </summary>
		public float DistributionCover { get; set; }
		/// <summary>
		/// Quantity of stock in the store plus quantity sent previously and with current distribution.
		/// </summary>
		public int StockAfterDistribution { get; set; }


	}
}
