namespace BankStatementAnalyzer.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedColumnRemarkInOrderDetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetail", "Remark", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderDetail", "Remark");
        }
    }
}
