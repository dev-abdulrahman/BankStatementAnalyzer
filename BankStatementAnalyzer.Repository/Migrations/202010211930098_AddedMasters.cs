namespace BankStatementAnalyzer.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMasters : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Payment", "OrderID", "dbo.Order");
            DropForeignKey("dbo.OrderDetail", "OrderID", "dbo.Order");
            DropPrimaryKey("dbo.Order");
            CreateTable(
                "dbo.Cloths",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
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
                "dbo.OrderType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
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
                "dbo.ServiceType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
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
                "dbo.Package",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Diemension = c.String(nullable: false, maxLength: 100),
                        Description = c.String(maxLength: 1024),
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
                "dbo.RateCard",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ClothId = c.Int(nullable: false),
                        ServiceTypeId = c.Int(nullable: false),
                        UrgetRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NormalRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                        UpdatedBy = c.String(maxLength: 1000),
                        DeletedBy = c.String(maxLength: 100),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cloths", t => t.ClothId)
                .ForeignKey("dbo.ServiceType", t => t.ServiceTypeId)
                .Index(t => t.ClothId)
                .Index(t => t.ServiceTypeId);
            
            AddColumn("dbo.OrderDetail", "ServiceTypeID", c => c.Int(nullable: false));
            AddColumn("dbo.OrderDetail", "IsUrgent", c => c.Boolean(nullable: false));
            AddColumn("dbo.OrderDetail", "TotalPayable", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Order", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Order", "CouponId", c => c.Int());
            AddPrimaryKey("dbo.Order", "ID");
            CreateIndex("dbo.OrderDetail", "ServiceTypeID");
            CreateIndex("dbo.Order", "Id");
            CreateIndex("dbo.Order", "CouponId");
            AddForeignKey("dbo.Order", "CouponId", "dbo.Coupon", "ID");
            AddForeignKey("dbo.Order", "Id", "dbo.OrderType", "ID");
            AddForeignKey("dbo.OrderDetail", "ServiceTypeID", "dbo.ServiceType", "ID");
            AddForeignKey("dbo.Payment", "OrderID", "dbo.Order", "ID");
            AddForeignKey("dbo.OrderDetail", "OrderID", "dbo.Order", "ID");
            DropColumn("dbo.OrderDetail", "ProductID");
            DropColumn("dbo.OrderDetail", "TotalSavings");
            DropColumn("dbo.OrderDetail", "Size");
            DropColumn("dbo.OrderDetail", "IsReorder");
            DropColumn("dbo.OrderDetail", "IsProductReturnAbleReturned");
            DropColumn("dbo.OrderDetail", "IsNewCanRequired");
            DropColumn("dbo.OrderDetail", "NewCanQuantity");
            DropColumn("dbo.Order", "UniqueOrderNo");
            DropColumn("dbo.Order", "WalletPoints");
            DropColumn("dbo.Order", "NoOfReturnAbleCans");
            DropColumn("dbo.Order", "TimeSlotId");
            DropColumn("dbo.Order", "OrderType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Order", "OrderType", c => c.Int(nullable: false));
            AddColumn("dbo.Order", "TimeSlotId", c => c.Int(nullable: false));
            AddColumn("dbo.Order", "NoOfReturnAbleCans", c => c.Int(nullable: false));
            AddColumn("dbo.Order", "WalletPoints", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Order", "UniqueOrderNo", c => c.String());
            AddColumn("dbo.OrderDetail", "NewCanQuantity", c => c.Int(nullable: false));
            AddColumn("dbo.OrderDetail", "IsNewCanRequired", c => c.Boolean(nullable: false));
            AddColumn("dbo.OrderDetail", "IsProductReturnAbleReturned", c => c.Boolean(nullable: false));
            AddColumn("dbo.OrderDetail", "IsReorder", c => c.Boolean(nullable: false));
            AddColumn("dbo.OrderDetail", "Size", c => c.String(maxLength: 20));
            AddColumn("dbo.OrderDetail", "TotalSavings", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.OrderDetail", "ProductID", c => c.Int(nullable: false));
            DropForeignKey("dbo.OrderDetail", "OrderID", "dbo.Order");
            DropForeignKey("dbo.Payment", "OrderID", "dbo.Order");
            DropForeignKey("dbo.RateCard", "ServiceTypeId", "dbo.ServiceType");
            DropForeignKey("dbo.RateCard", "ClothId", "dbo.Cloths");
            DropForeignKey("dbo.OrderDetail", "ServiceTypeID", "dbo.ServiceType");
            DropForeignKey("dbo.Order", "Id", "dbo.OrderType");
            DropForeignKey("dbo.Order", "CouponId", "dbo.Coupon");
            DropIndex("dbo.RateCard", new[] { "ServiceTypeId" });
            DropIndex("dbo.RateCard", new[] { "ClothId" });
            DropIndex("dbo.Order", new[] { "CouponId" });
            DropIndex("dbo.Order", new[] { "Id" });
            DropIndex("dbo.OrderDetail", new[] { "ServiceTypeID" });
            DropPrimaryKey("dbo.Order");
            AlterColumn("dbo.Order", "CouponId", c => c.Int(nullable: false));
            AlterColumn("dbo.Order", "Id", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.OrderDetail", "TotalPayable");
            DropColumn("dbo.OrderDetail", "IsUrgent");
            DropColumn("dbo.OrderDetail", "ServiceTypeID");
            DropTable("dbo.RateCard");
            DropTable("dbo.Package");
            DropTable("dbo.ServiceType");
            DropTable("dbo.OrderType");
            DropTable("dbo.Cloths");
            AddPrimaryKey("dbo.Order", "ID");
            AddForeignKey("dbo.OrderDetail", "OrderID", "dbo.Order", "ID");
            AddForeignKey("dbo.Payment", "OrderID", "dbo.Order", "ID");
        }
    }
}
