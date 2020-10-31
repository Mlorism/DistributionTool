namespace DistributionTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSalesWeekTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SalesWeeks",
                c => new
                    {
                        Week = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        StopDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.Week, t.StartDate });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SalesWeeks");
        }
    }
}
