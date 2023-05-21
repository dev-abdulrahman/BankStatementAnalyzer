namespace BankStatementAnalyzer.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedTables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Category", "FileId", "dbo.File");
            DropIndex("dbo.Category", new[] { "FileId" });
            AddColumn("dbo.RateCard", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.RateCard", "CategoryId");
            AddForeignKey("dbo.RateCard", "CategoryId", "dbo.Category", "ID");
            DropColumn("dbo.Category", "Description");
            DropColumn("dbo.Category", "FileId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Category", "FileId", c => c.Int(nullable: false));
            AddColumn("dbo.Category", "Description", c => c.String(nullable: false));
            DropForeignKey("dbo.RateCard", "CategoryId", "dbo.Category");
            DropIndex("dbo.RateCard", new[] { "CategoryId" });
            DropColumn("dbo.RateCard", "CategoryId");
            CreateIndex("dbo.Category", "FileId");
            AddForeignKey("dbo.Category", "FileId", "dbo.File", "ID");
        }
    }
}
