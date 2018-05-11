namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateUserTimeSheet : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserTimeSheets", "FillDate");
            DropColumn("dbo.UserTimeSheets", "Hours");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserTimeSheets", "Hours", c => c.Int(nullable: false));
            AddColumn("dbo.UserTimeSheets", "FillDate", c => c.DateTime(nullable: false));
        }
    }
}
