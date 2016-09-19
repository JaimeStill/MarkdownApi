namespace MarkdownApi.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class projectmarkdown : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "Markdown", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "Markdown");
        }
    }
}
