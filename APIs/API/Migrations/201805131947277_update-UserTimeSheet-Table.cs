namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateUserTimeSheetTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserTimeSheets", "IsApproved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserTimeSheets", "IsApproved");
        }
    }
}
