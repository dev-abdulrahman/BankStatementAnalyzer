namespace BankStatementAnalyzer.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFKForCategory : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.HomeScreenLayout", new[] { "Category_ID" });
            DropIndex("dbo.HomeScreenLayout", new[] { "Color_ID" });
            RenameColumn(table: "dbo.HomeScreenLayout", name: "Category_ID", newName: "CategoryId");
            RenameColumn(table: "dbo.HomeScreenLayout", name: "Color_ID", newName: "ColorId");
            AlterColumn("dbo.HomeScreenLayout", "CategoryId", c => c.Int(nullable: false));
            AlterColumn("dbo.HomeScreenLayout", "ColorId", c => c.Int(nullable: false));
            CreateIndex("dbo.HomeScreenLayout", "ColorId");
            CreateIndex("dbo.HomeScreenLayout", "CategoryId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.HomeScreenLayout", new[] { "CategoryId" });
            DropIndex("dbo.HomeScreenLayout", new[] { "ColorId" });
            AlterColumn("dbo.HomeScreenLayout", "ColorId", c => c.Int());
            AlterColumn("dbo.HomeScreenLayout", "CategoryId", c => c.Int());
            RenameColumn(table: "dbo.HomeScreenLayout", name: "ColorId", newName: "Color_ID");
            RenameColumn(table: "dbo.HomeScreenLayout", name: "CategoryId", newName: "Category_ID");
            CreateIndex("dbo.HomeScreenLayout", "Color_ID");
            CreateIndex("dbo.HomeScreenLayout", "Category_ID");
        }
    }
}
