using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributionTool.Models
{
	/// <summary>
	/// Class containing product PLU and Distribution Cover
	/// </summary>
	public class DistributionCover
	{
		public int PLU { get; set; }
		public float DisCover { get; set; }

		public DistributionCover(int p, float d)
		{
			PLU = p;
			DisCover = d;
		}
	}
}
