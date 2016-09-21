namespace MarkdownApi.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Wikis", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Wikis", "CategoryId");
            AddForeignKey("dbo.Wikis", "CategoryId", "dbo.Categories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Wikis", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Wikis", new[] { "CategoryId" });
            DropColumn("dbo.Wikis", "CategoryId");
            DropTable("dbo.Categories");
        }
    }
}
