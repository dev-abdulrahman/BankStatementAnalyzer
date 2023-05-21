namespace BankStatementAnalyzer.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_payment_status_column : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "PaymentStatus", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "PaymentStatus");
        }
    }
}
