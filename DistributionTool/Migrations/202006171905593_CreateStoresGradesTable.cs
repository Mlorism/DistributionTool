namespace DistributionTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateStoresGradesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StoreGradeModels",
                c => new
                    {
                        StoreNumber = c.Int(nullable: false),
                        Group = c.Int(nullable: false),
                        Grade_StoreNumber = c.Int(),
                        Grade_Group = c.Int(),
                    })
                .PrimaryKey(t => new { t.StoreNumber, t.Group })
                .ForeignKey("dbo.StoreGradeModels", t => new { t.Grade_StoreNumber, t.Grade_Group })
                .Index(t => new { t.Grade_StoreNumber, t.Grade_Group });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StoreGradeModels", new[] { "Grade_StoreNumber", "Grade_Group" }, "dbo.StoreGradeModels");
            DropIndex("dbo.StoreGradeModels", new[] { "Grade_StoreNumber", "Grade_Group" });
            DropTable("dbo.StoreGradeModels");
        }
    }
}
