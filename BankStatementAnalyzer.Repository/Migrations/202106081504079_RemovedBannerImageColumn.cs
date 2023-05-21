namespace BankStatementAnalyzer.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedBannerImageColumn : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BannerImages", "Banners_ID", "dbo.Banners");
            DropIndex("dbo.BannerImages", new[] { "Banners_ID" });
            AddColumn("dbo.BannerImages", "BannerId", c => c.Int(nullable: false));
            DropColumn("dbo.BannerImages", "Banners_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BannerImages", "Banners_ID", c => c.Int());
            DropColumn("dbo.BannerImages", "BannerId");
            CreateIndex("dbo.BannerImages", "Banners_ID");
            AddForeignKey("dbo.BannerImages", "Banners_ID", "dbo.Banners", "ID");
        }
    }
}
