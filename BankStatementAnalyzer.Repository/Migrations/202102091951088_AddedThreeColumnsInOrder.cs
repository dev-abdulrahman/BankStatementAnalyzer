namespace BankStatementAnalyzer.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedThreeColumnsInOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "Tip", c => c.Double(nullable: false));
            AddColumn("dbo.Order", "MerchantCode", c => c.String());
            AddColumn("dbo.Order", "PaymentToVendor", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "PaymentToVendor");
            DropColumn("dbo.Order", "MerchantCode");
            DropColumn("dbo.Order", "Tip");
        }
    }
}
