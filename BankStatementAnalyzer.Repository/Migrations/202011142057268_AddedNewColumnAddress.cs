namespace BankStatementAnalyzer.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNewColumnAddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Address", "Name", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Address", "Name");
        }
    }
}
