namespace BankStatementAnalyzer.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedColumnsInPackageTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Package", "Heading", c => c.String(maxLength: 100));
            AddColumn("dbo.Package", "BagCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Package", "BagCount");
            DropColumn("dbo.Package", "Heading");
        }
    }
}
