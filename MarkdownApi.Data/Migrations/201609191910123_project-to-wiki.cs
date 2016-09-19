namespace MarkdownApi.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class projecttowiki : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Projects", newName: "Wikis");
            RenameColumn(table: "dbo.Documents", name: "ProjectId", newName: "WikiId");
            RenameIndex(table: "dbo.Documents", name: "IX_ProjectId", newName: "IX_WikiId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Documents", name: "IX_WikiId", newName: "IX_ProjectId");
            RenameColumn(table: "dbo.Documents", name: "WikiId", newName: "ProjectId");
            RenameTable(name: "dbo.Wikis", newName: "Projects");
        }
    }
}
