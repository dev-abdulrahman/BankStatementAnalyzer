namespace BankStatementAnalyzer.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRelationInCloth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cloths", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Cloths", "CategoryId");
            AddForeignKey("dbo.Cloths", "CategoryId", "dbo.Category", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cloths", "CategoryId", "dbo.Category");
            DropIndex("dbo.Cloths", new[] { "CategoryId" });
            DropColumn("dbo.Cloths", "CategoryId");
        }
    }
}
