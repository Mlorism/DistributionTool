namespace DistributionTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateStoresGradesTableCorrection : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StoreGradeModels", new[] { "Grade_StoreNumber", "Grade_Group" }, "dbo.StoreGradeModels");
            DropIndex("dbo.StoreGradeModels", new[] { "Grade_StoreNumber", "Grade_Group" });
            AddColumn("dbo.StoreGradeModels", "Grade", c => c.Int(nullable: false));
            DropColumn("dbo.StoreGradeModels", "Grade_StoreNumber");
            DropColumn("dbo.StoreGradeModels", "Grade_Group");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StoreGradeModels", "Grade_Group", c => c.Int());
            AddColumn("dbo.StoreGradeModels", "Grade_StoreNumber", c => c.Int());
            DropColumn("dbo.StoreGradeModels", "Grade");
            CreateIndex("dbo.StoreGradeModels", new[] { "Grade_StoreNumber", "Grade_Group" });
            AddForeignKey("dbo.StoreGradeModels", new[] { "Grade_StoreNumber", "Grade_Group" }, "dbo.StoreGradeModels", new[] { "StoreNumber", "Group" });
        }
    }
}
