using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributionTool.Models
{
	/// <summary>
	/// Class representing model of packs that are distributed from total amount in distribution center
	/// </summary>
	public class DistributedPLUPacks
	{
		public int PLU;
		public int DistributedPc;
		public int AvailableDCPc;

		public DistributedPLUPacks(int p, int d, int a)
		{
			PLU = p;
			DistributedPc = d;
			AvailableDCPc = a;
		}
	}
}
