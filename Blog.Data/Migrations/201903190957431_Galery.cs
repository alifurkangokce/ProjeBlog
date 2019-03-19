namespace Blog.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Galery : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GaleryFiles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FileName = c.String(),
                        GaleryId = c.Guid(nullable: false),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Galeries", t => t.GaleryId, cascadeDelete: true)
                .Index(t => t.GaleryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GaleryFiles", "GaleryId", "dbo.Galeries");
            DropIndex("dbo.GaleryFiles", new[] { "GaleryId" });
            DropTable("dbo.GaleryFiles");
        }
    }
}
