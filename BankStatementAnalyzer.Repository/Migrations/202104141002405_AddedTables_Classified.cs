namespace BankStatementAnalyzer.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTables_Classified : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classified",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Address1 = c.String(maxLength: 1024),
                        Address2 = c.String(maxLength: 1024),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        Availability = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                        UpdatedBy = c.String(maxLength: 1000),
                        DeletedBy = c.String(maxLength: 100),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ClassifiedCategory",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ClassifiedId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                        UpdatedBy = c.String(maxLength: 1000),
                        DeletedBy = c.String(maxLength: 100),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ClassifiedContacts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ClassifiedId = c.Int(nullable: false),
                        ContactNo = c.String(maxLength: 15),
                        Status = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                        UpdatedBy = c.String(maxLength: 1000),
                        DeletedBy = c.String(maxLength: 100),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ClassifiedImages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ClassifiedId = c.Int(nullable: false),
                        ImageUrl = c.String(),
                        Status = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                        UpdatedBy = c.String(maxLength: 1000),
                        DeletedBy = c.String(maxLength: 100),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ClassifiedKeywords",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ClassifiedId = c.Int(nullable: false),
                        Keyword = c.String(),
                        Status = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                        UpdatedBy = c.String(maxLength: 1000),
                        DeletedBy = c.String(maxLength: 100),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ClassifiedRating",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ClassifiedId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        Rating = c.Double(nullable: false),
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
            DropTable("dbo.ClassifiedRating");
            DropTable("dbo.ClassifiedKeywords");
            DropTable("dbo.ClassifiedImages");
            DropTable("dbo.ClassifiedContacts");
            DropTable("dbo.ClassifiedCategory");
            DropTable("dbo.Classified");
        }
    }
}
