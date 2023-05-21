namespace BankStatementAnalyzer.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTable_HomescreenLayout_Colors : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Colors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
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
                "dbo.HomeScreenLayout",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IconId = c.String(),
                        Row = c.Int(nullable: false),
                        Column = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                        UpdatedBy = c.String(maxLength: 1000),
                        DeletedBy = c.String(maxLength: 100),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                        Category_ID = c.Int(),
                        Color_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Category", t => t.Category_ID)
                .ForeignKey("dbo.Colors", t => t.Color_ID)
                .Index(t => t.Category_ID)
                .Index(t => t.Color_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HomeScreenLayout", "Color_ID", "dbo.Colors");
            DropForeignKey("dbo.HomeScreenLayout", "Category_ID", "dbo.Category");
            DropIndex("dbo.HomeScreenLayout", new[] { "Color_ID" });
            DropIndex("dbo.HomeScreenLayout", new[] { "Category_ID" });
            DropTable("dbo.HomeScreenLayout");
            DropTable("dbo.Colors");
        }
    }
}
