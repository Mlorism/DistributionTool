namespace DistributionTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateColumnTypes : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Products", newName: "ProductModels");
            RenameTable(name: "dbo.Users", newName: "UserModels");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.UserModels", newName: "Users");
            RenameTable(name: "dbo.ProductModels", newName: "Products");
        }
    }
}
