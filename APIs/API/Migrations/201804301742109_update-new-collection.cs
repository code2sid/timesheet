namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatenewcollection : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tasks", "TaskTypeId", "dbo.TaskTypes");
            DropIndex("dbo.Tasks", new[] { "TaskTypeId" });
            CreateTable(
                "dbo.ProjectTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        TaskId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserProjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Projects", "UserId");
            DropColumn("dbo.Tasks", "ProjectId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tasks", "ProjectId", c => c.Int(nullable: false));
            AddColumn("dbo.Projects", "UserId", c => c.Int(nullable: false));
            DropTable("dbo.UserProjects");
            DropTable("dbo.ProjectTasks");
            CreateIndex("dbo.Tasks", "TaskTypeId");
            AddForeignKey("dbo.Tasks", "TaskTypeId", "dbo.TaskTypes", "Id", cascadeDelete: true);
        }
    }
}
