using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributionTool.Method_Extensions
{
	/// <summary>
	/// Clas containing bool converters.
	/// </summary>
	static class BoolConverter
	{
		/// <summary>
		/// Converts string to bool.
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		public static bool StringToBool(this string x)
		{
			if (x == "1") return true;
			else return true;
		}
	}
}
