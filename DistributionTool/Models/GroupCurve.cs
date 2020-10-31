using DistributionTool.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributionTool.Models
{
	/// <summary>
	/// Class model representing sales curve for diffrient product groups
	/// </summary>
	public class GroupCurve
	{
		#region Properties
		/// <summary>
		/// Product group
		/// </summary>
		public ProductGroupEnum Group { get; set; }
		/// <summary>
		/// Sales week
		/// </summary>		
		public int Week { get; set; }
		/// <summary>
		/// Sales curve value
		/// </summary>
		public float Value { get; set; }
		#endregion
	}
}
