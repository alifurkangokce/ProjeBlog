namespace Blog.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class soruisareti : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PostFiles", "PostId", "dbo.Posts");
            DropIndex("dbo.PostFiles", new[] { "PostId" });
            AlterColumn("dbo.PostFiles", "PostId", c => c.Guid(nullable: false));
            CreateIndex("dbo.PostFiles", "PostId");
            AddForeignKey("dbo.PostFiles", "PostId", "dbo.Posts", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostFiles", "PostId", "dbo.Posts");
            DropIndex("dbo.PostFiles", new[] { "PostId" });
            AlterColumn("dbo.PostFiles", "PostId", c => c.Guid());
            CreateIndex("dbo.PostFiles", "PostId");
            AddForeignKey("dbo.PostFiles", "PostId", "dbo.Posts", "Id");
        }
    }
}
