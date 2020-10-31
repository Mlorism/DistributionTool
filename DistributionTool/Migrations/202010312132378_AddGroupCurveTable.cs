namespace DistributionTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGroupCurveTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GroupCurves",
                c => new
                    {
                        Group = c.Int(nullable: false),
                        Week = c.Int(nullable: false),
                        Value = c.Single(nullable: false),
                    })
                .PrimaryKey(t => new { t.Group, t.Week });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GroupCurves");
        }
    }
}
