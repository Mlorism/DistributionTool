namespace DistributionTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableColumnNames : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ProductParametersModels", newName: "ProductParameters");
            RenameTable(name: "dbo.ProductModels", newName: "Products");
            RenameTable(name: "dbo.StoreGradeModels", newName: "StoreGrades");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.StoreGrades", newName: "StoreGradeModels");
            RenameTable(name: "dbo.Products", newName: "ProductModels");
            RenameTable(name: "dbo.ProductParameters", newName: "ProductParametersModels");
        }
    }
}
