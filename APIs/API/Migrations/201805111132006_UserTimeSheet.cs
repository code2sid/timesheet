namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserTimeSheet : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserTimeSheets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                        TaskId = c.Int(nullable: false),
                        FillDate = c.DateTime(nullable: false),
                        Hours = c.Int(nullable: false),
                        IsSaved = c.Boolean(nullable: false),
                        IsSubmitted = c.Boolean(nullable: false),
                        Comments = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserTimeSheets");
        }
    }
}
