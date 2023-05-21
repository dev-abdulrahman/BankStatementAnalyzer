namespace BankStatementAnalyzer.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedColumnInClassifiedRating : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClassifiedRating", "Name", c => c.String());
            AddColumn("dbo.ClassifiedRating", "Review", c => c.String());
            AddColumn("dbo.ClassifiedRating", "CategoryId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ClassifiedRating", "CategoryId");
            DropColumn("dbo.ClassifiedRating", "Review");
            DropColumn("dbo.ClassifiedRating", "Name");
        }
    }
}
