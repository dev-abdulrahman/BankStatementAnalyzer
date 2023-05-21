namespace BankStatementAnalyzer.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedColumnEstimatedDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderStatusMapping", "EstimatedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderStatusMapping", "EstimatedDate");
        }
    }
}
