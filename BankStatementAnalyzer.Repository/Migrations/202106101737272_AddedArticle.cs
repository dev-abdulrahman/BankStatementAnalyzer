namespace BankStatementAnalyzer.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedArticle : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClassifiedArticle",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SubHeading = c.String(),
                        Heading = c.String(),
                        ShortDescription = c.String(),
                        ContactNo = c.String(),
                        Address = c.String(),
                        Article = c.String(),
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
                "dbo.ClassifiedArticleCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClassifiedArticleId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ClassifiedArticleImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                        ClassifiedArticleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ClassifiedArticleKeywords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Keywords = c.String(),
                        ClassifiedArticleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ClassifiedArticleKeywords");
            DropTable("dbo.ClassifiedArticleImages");
            DropTable("dbo.ClassifiedArticleCategory");
            DropTable("dbo.ClassifiedArticle");
        }
    }
}
