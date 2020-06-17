namespace DistributionTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateColumnNames : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ProductParameters", newName: "ProductParametersModels");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ProductParametersModels", newName: "ProductParameters");
        }
    }
}
