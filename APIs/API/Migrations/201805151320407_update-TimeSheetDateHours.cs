namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateTimeSheetDateHours : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TimeSheetDateHours", "Date", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TimeSheetDateHours", "Date", c => c.DateTime(nullable: false));
        }
    }
}
