namespace BankStatementAnalyzer.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_CR_adv_column_in_Order : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "BalanceAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Order", "AdvanceAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "AdvanceAmount");
            DropColumn("dbo.Order", "BalanceAmount");
        }
    }
}
