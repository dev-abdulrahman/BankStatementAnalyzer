namespace BankStatementAnalyzer.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedColumnKeyInOrderType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderType", "Key", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderType", "Key");
        }
    }
}
