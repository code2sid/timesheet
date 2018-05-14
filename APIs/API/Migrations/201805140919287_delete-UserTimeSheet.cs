namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteUserTimeSheet : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.UserTimeSheets");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserTimeSheets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                        TaskId = c.Int(nullable: false),
                        IsSaved = c.Boolean(nullable: false),
                        IsSubmitted = c.Boolean(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        Comments = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
