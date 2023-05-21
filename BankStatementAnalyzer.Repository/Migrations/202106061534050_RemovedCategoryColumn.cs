namespace BankStatementAnalyzer.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedCategoryColumn : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.HomeScreenLayout", "Category_ID", "dbo.Category");
            DropIndex("dbo.HomeScreenLayout", new[] { "Category_ID" });
            AddColumn("dbo.HomeScreenLayout", "SubCategory_ID", c => c.Int());
            CreateIndex("dbo.HomeScreenLayout", "SubCategory_ID");
            AddForeignKey("dbo.HomeScreenLayout", "SubCategory_ID", "dbo.SubCategory", "ID");
            DropColumn("dbo.HomeScreenLayout", "Category_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.HomeScreenLayout", "Category_ID", c => c.Int());
            DropForeignKey("dbo.HomeScreenLayout", "SubCategory_ID", "dbo.SubCategory");
            DropIndex("dbo.HomeScreenLayout", new[] { "SubCategory_ID" });
            DropColumn("dbo.HomeScreenLayout", "SubCategory_ID");
            CreateIndex("dbo.HomeScreenLayout", "Category_ID");
            AddForeignKey("dbo.HomeScreenLayout", "Category_ID", "dbo.Category", "ID");
        }
    }
}
