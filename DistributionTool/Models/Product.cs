using DistributionTool.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DistributionTool.Models
{
    /// <summary>
    /// Class model representing product.
    /// </summary>
    public class Product
    {
        #region Properties
        /// <summary>
        /// Price look-up code.
        /// </summary>  
        [Key]
        public int PLU {get; set;}
        /// <summary>
        /// Product name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Product group name.
        /// </summary>
        public ProductGroupEnum GroupName { get; set; }
        /// <summary>
        /// Product subgropup name.
        /// </summary>
        public ProductSubGroupEnum SubGroup { get; set; }
        /// <summary>
        /// Color of the product.
        /// </summary>
        public string Color { get; set; }
        /// <summary>
        /// Product price.
        /// </summary>
        public float Price { get; set; }
        /// <summary>
        /// Product pack size, how many items are in single package.
        /// </summary>     
        public int PackSize { get; set; }
        /// <summary>
        /// Names of the promotions assigned to the product.
        /// </summary>
        public string Promotion { get; set; }    
        /// <summary>
        /// Product quantity available in warehouse for distribution to stores.
        /// </summary>
        public int WarehouseFreeQty { get; set; }
        /// <summary>
        /// Product quantity available in warehouse for distribution to stores.
        /// </summary>
        public int WarehouseDistributedQty { get; set; }
        /// <summary>
        /// Number of stores with smaller quantity of product than the set minimum. 
        /// </summary>
        public int StoresBelowMinimum { get; set; }   
        /// <summary>
        /// Number of weeks of stock with current rate of sales with addditional shipped stock.
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
        public DistributionMethodEnum MethodOfDistribution { get; set; }
        #endregion

        #region Methods

        public Product Clone()
        {
            Product product = new Product();
            product.PLU = this.PLU;
            product.Name = this.Name;
            product.GroupName = this.GroupName;
            product.SubGroup = this.SubGroup;
            product.Color = this.Color;
            product.PackSize = this.PackSize;
            product.Promotion = this.Promotion;
            product.WarehouseFreeQty = this.WarehouseFreeQty;
            product.WarehouseDistributedQty = this.WarehouseDistributedQty;
            product.StoresBelowMinimum = this.StoresBelowMinimum;
            product.StoresEffectiveCover = this.StoresEffectiveCover;
            product.MondayDistribution = this.MondayDistribution;
            product.TuesdayDistribution = this.TuesdayDistribution;
            product.WednesdayDistribution = this.WednesdayDistribution;
            product.ThursdayDistribution = this.ThursdayDistribution;
            product.FridayDistribution = this.FridayDistribution;
            product.MethodOfDistribution = this.MethodOfDistribution;
            
            return product;
        }

        #endregion

    }
}


              
              
                
                
               