namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TimeSheetDateHours_add : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TimeSheetDateHours",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Hours = c.Int(nullable: false),
                        TimesheetId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TimeSheetDateHours");
        }
    }
}
