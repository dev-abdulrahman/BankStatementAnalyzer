namespace BankStatementAnalyzer.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewTableAddedForPackageMapping : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Order", "CouponId", "dbo.Coupon");
            DropIndex("dbo.Order", new[] { "CouponId" });
            CreateTable(
                "dbo.PackageCouponMapping",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CouponId = c.Int(nullable: false),
                        PackageId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Order", "CouponId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Order", "CouponId", c => c.Int());
            DropTable("dbo.PackageCouponMapping");
            CreateIndex("dbo.Order", "CouponId");
            AddForeignKey("dbo.Order", "CouponId", "dbo.Coupon", "ID");
        }
    }
}
