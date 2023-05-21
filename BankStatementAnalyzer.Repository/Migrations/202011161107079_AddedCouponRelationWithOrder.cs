namespace BankStatementAnalyzer.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCouponRelationWithOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "CouponId", c => c.Int());
            CreateIndex("dbo.Order", "CouponId");
            AddForeignKey("dbo.Order", "CouponId", "dbo.Package", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Order", "CouponId", "dbo.Package");
            DropIndex("dbo.Order", new[] { "CouponId" });
            DropColumn("dbo.Order", "CouponId");
        }
    }
}
