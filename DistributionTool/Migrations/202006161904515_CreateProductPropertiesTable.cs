namespace DistributionTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateProductPropertiesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductParameters",
                c => new
                    {
                        PLU = c.Int(nullable: false),
                        Grade = c.Int(nullable: false),
                        Min = c.Int(nullable: false),
                        Max = c.Int(nullable: false),
                        Cover = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PLU, t.Grade }); 
        }
        
        public override void Down()
        {         
            DropTable("dbo.ProductParameters");
        }
    }
}
