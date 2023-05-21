namespace BankStatementAnalyzer.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBestDealTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BestDeal",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        ClassifiedId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        Name = c.String(maxLength: 100),
                        Email = c.String(nullable: false),
                        PhoneNumber = c.String(maxLength: 15),
                        Status = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                        UpdatedBy = c.String(maxLength: 1000),
                        DeletedBy = c.String(maxLength: 100),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BestDeal");
        }
    }
}
