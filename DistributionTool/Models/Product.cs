using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributionTool.Models
{
    /// <summary>
    /// Class model representing product.
    /// </summary>
    class Product
    {
        /// <summary>
        /// Price look-up code.
        /// </summary>
        public int PLU {get; set;}
        /// <summary>
        /// Product name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Product pack size, how many items are in single package.
        /// </summary>
        public int PackSize { get; set; }
        /// <summary>
        /// Names of the promotions assigned to the product.
        /// </summary>
        public string Promotion { get; set; }
        /// <summary>
        /// Product group name.
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// Product subgropup name.
        /// </summary>
        public string SubGroup { get; set; }
        /// <summary>
        /// Product quantity available in warehouse for distribution to stores.
        /// </summary>
        public int WhFreeQty { get; set; }
        /// <summary>
        /// Packages available in warehouse for distribution to stores.
        /// </summary>
        public int WhFreePc { get; set; }
        /// <summary>
        /// Number of stores with smaller quantity of product than the set minimum. 
        /// </summary>
        public int StoresBelowMin { get; set; }
        /// <summary>
        /// Number of weeks of stock with current sales.
        /// </summary>
        public float StoresCover { get; set; }
        /// <summary>
        /// Number of weeks of stock with current sales with addditional shipped stock.
        /// </summary>
        public float StoresEffectiveCover { get; set; }
        /// <summary>
        /// Will a distribution be created on a given day of the week.
        /// </summary>
        public bool MondayDistribution { get; set; }
        /// <summary>
        /// Will a distribution be created on a given day of the week.
        /// </summary>
        public bool TuesdayDistribution { get; set; }
        /// <summary>
        /// Will a distribution be created on a given day of the week.
        /// </summary>
        public bool WednesdayDistribution { get; set; }
        /// <summary>
        /// Will a distribution be created on a given day of the week.
        /// </summary>
        public bool ThursdayDistribution { get; set; }
        /// <summary>
        /// Will a distribution be created on a given day of the week.
        /// </summary>
        public bool FridayDistribution { get; set; }
        /// <summary>
        /// Selected method used to calculate distribution.
        /// </summary>
        public int DistributionMethod { get; set; }

    }
}


              
              
                
                
               