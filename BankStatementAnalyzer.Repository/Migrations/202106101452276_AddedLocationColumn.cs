namespace BankStatementAnalyzer.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLocationColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Classified", "Location", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Classified", "Location");
        }
    }
}
