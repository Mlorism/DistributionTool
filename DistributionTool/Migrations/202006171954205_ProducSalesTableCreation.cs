namespace DistributionTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProducSalesTableCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductSales",
                c => new
                    {
                        PLU = c.Int(nullable: false),
                        StoreNumber = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Sales = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PLU, t.StoreNumber, t.Date });           
        }
        
        public override void Down()
        {
            DropTable("dbo.ProductSales");
        }
    }
}
