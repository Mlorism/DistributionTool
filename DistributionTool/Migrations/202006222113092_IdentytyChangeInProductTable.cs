namespace DistributionTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IdentytyChangeInProductTable : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Products");
            AlterColumn("dbo.Products", "PLU", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Products", "PLU");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Products");
            AlterColumn("dbo.Products", "PLU", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Products", "PLU");
        }
    }
}
