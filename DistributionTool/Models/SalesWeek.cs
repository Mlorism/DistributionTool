using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributionTool.Models
{
	/// <summary>
	/// Class model representing sales week
	/// </summary>
	class SalesWeek
	{
		#region Properties
		public int Week { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime StopDate { get; set; }
		#endregion
	}
}
