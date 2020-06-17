namespace DistributionTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableColumnNames1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UserModels", newName: "Users");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Users", newName: "UserModels");
        }
    }
}
