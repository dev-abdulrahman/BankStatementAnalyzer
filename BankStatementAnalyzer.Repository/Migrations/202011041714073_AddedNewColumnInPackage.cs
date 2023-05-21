namespace BankStatementAnalyzer.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNewColumnInPackage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Package", "UrgentRate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Package", "UrgentRate");
        }
    }
}
