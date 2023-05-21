namespace BankStatementAnalyzer.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNewColumnInVendor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vendor", "Latitude", c => c.Double(nullable: false));
            AddColumn("dbo.Vendor", "Longitude", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vendor", "Longitude");
            DropColumn("dbo.Vendor", "Latitude");
        }
    }
}
