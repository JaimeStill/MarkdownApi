namespace MarkdownApi.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wikisidebar : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sidebars",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        WikiId = c.Int(nullable: false),
                        Markdown = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Wikis", t => t.Id)
                .Index(t => t.Id);
            
            AddColumn("dbo.Wikis", "SidebarId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sidebars", "Id", "dbo.Wikis");
            DropIndex("dbo.Sidebars", new[] { "Id" });
            DropColumn("dbo.Wikis", "SidebarId");
            DropTable("dbo.Sidebars");
        }
    }
}
