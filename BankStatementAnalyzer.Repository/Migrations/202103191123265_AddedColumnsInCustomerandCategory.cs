namespace BankStatementAnalyzer.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedColumnsInCustomerandCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Category", "IconId", c => c.Int(nullable: false));
            AddColumn("dbo.Category", "IsSpecialCategory", c => c.Boolean(nullable: false));
            AddColumn("dbo.Customer", "Gender", c => c.Int(nullable: false));
            AddColumn("dbo.Customer", "MaritalStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customer", "MaritalStatus");
            DropColumn("dbo.Customer", "Gender");
            DropColumn("dbo.Category", "IsSpecialCategory");
            DropColumn("dbo.Category", "IconId");
        }
    }
}
