namespace BankStatementAnalyzer.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNewColumnInOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "PackageId", c => c.Int());
            AddColumn("dbo.Order", "IsUrgent", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Order", "PackageId");
            AddForeignKey("dbo.Order", "PackageId", "dbo.Package", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Order", "PackageId", "dbo.Package");
            DropIndex("dbo.Order", new[] { "PackageId" });
            DropColumn("dbo.Order", "IsUrgent");
            DropColumn("dbo.Order", "PackageId");
        }
    }
}
