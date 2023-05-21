namespace BankStatementAnalyzer.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedColumn_BirhtDate_Image_InUserTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customer", "BirthDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Customer", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customer", "Image");
            DropColumn("dbo.Customer", "BirthDate");
        }
    }
}
