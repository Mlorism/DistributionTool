using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributionTool.Models
{
	/// <summary>
	/// Class model representing product stock.
	/// </summary>
	class ProductStock
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
		/// Product quantity in store with quantity that has been sent from warehouse.
		/// </summary>
		public int EffectiveStock { get; set; }		
		/// <summary>
		/// Number of weeks of stock with current rate of sales with shipped stock.
		/// </summary>
		public float EffectiveCover { get; set; }
	}
}
