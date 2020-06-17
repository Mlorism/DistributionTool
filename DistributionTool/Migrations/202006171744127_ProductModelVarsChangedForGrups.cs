namespace DistributionTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductModelVarsChangedForGrups : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "GroupName", c => c.Int(nullable: false));
            AlterColumn("dbo.Products", "SubGroup", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "SubGroup", c => c.String());
            AlterColumn("dbo.Products", "GroupName", c => c.String());
        }
    }
}
