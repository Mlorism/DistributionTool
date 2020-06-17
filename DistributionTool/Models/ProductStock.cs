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
		/// Product quantity in store.
		/// </summary>
		public int Stock { get; set; }
		/// <summary>
		/// Product quantity in store with quantity that has been sent from warehouse.
		/// </summary>
		public int EffectiveStock { get; set; }
	}
}
