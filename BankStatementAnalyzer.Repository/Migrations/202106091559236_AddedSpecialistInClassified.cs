namespace BankStatementAnalyzer.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSpecialistInClassified : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.HomeScreenLayout", "SubCategory_ID", "dbo.SubCategory");
            DropIndex("dbo.HomeScreenLayout", new[] { "SubCategory_ID" });
            AddColumn("dbo.Classified", "Description", c => c.String());
            AddColumn("dbo.Classified", "SpecialistIn", c => c.String());
            AddColumn("dbo.HomeScreenLayout", "Category_ID", c => c.Int());
            CreateIndex("dbo.HomeScreenLayout", "Category_ID");
            AddForeignKey("dbo.HomeScreenLayout", "Category_ID", "dbo.Category", "ID");
            DropColumn("dbo.HomeScreenLayout", "SubCategory_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.HomeScreenLayout", "SubCategory_ID", c => c.Int());
            DropForeignKey("dbo.HomeScreenLayout", "Category_ID", "dbo.Category");
            DropIndex("dbo.HomeScreenLayout", new[] { "Category_ID" });
            DropColumn("dbo.HomeScreenLayout", "Category_ID");
            DropColumn("dbo.Classified", "SpecialistIn");
            DropColumn("dbo.Classified", "Description");
            CreateIndex("dbo.HomeScreenLayout", "SubCategory_ID");
            AddForeignKey("dbo.HomeScreenLayout", "SubCategory_ID", "dbo.SubCategory", "ID");
        }
    }
}
