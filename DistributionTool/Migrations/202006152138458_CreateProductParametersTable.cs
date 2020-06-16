namespace DistributionTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateProductParametersTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductParameters",
                c => new
                    {
                        PLU = c.Int(nullable: false, identity: true),
                        Grade = c.String(nullable: false, maxLength: 1),
                        Min = c.Int(nullable: false),
                        Max = c.Int(nullable: false),
                        Cover = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PLU);
        }
        
        public override void Down()
        {           
            DropTable("dbo.ProductParameters");
        }
    }
}
