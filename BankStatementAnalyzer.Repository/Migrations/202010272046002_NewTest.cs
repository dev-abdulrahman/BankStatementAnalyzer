namespace BankStatementAnalyzer.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewTest : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderDelvieryBoyMapping",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        DeliveryBoyId = c.Int(nullable: false),
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
                "dbo.OrderDetail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderID = c.Int(nullable: false),
                        ServiceTypeID = c.Int(nullable: false),
                        ClothsID = c.Int(nullable: false),
                        Description = c.String(maxLength: 200),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscountedPercentage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ContactNo = c.String(maxLength: 15),
                        ExpectedDeliveryDate = c.DateTime(),
                        OrderStatus = c.Int(nullable: false),
                        IsUrgent = c.Boolean(nullable: false),
                        TotalPayable = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cloths", t => t.ClothsID)
                .ForeignKey("dbo.Order", t => t.OrderID)
                .ForeignKey("dbo.ServiceType", t => t.ServiceTypeID)
                .Index(t => t.OrderID)
                .Index(t => t.ServiceTypeID)
                .Index(t => t.ClothsID);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomerID = c.Int(nullable: false),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaidAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TransactionID = c.String(maxLength: 200),
                        OrderStatus = c.Int(nullable: false),
                        TransactionReference = c.String(),
                        AddressID = c.Int(),
                        OrderTypeId = c.Int(nullable: false),
                        CouponId = c.Int(),
                        PaymentType = c.Int(nullable: false),
                        SystemOrderID = c.String(maxLength: 10),
                        Remark = c.String(maxLength: 1024),
                        DeliveryDate = c.DateTime(),
                        Status = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                        UpdatedBy = c.String(maxLength: 1000),
                        DeletedBy = c.String(maxLength: 100),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Address", t => t.AddressID)
                .ForeignKey("dbo.Coupon", t => t.CouponId)
                .ForeignKey("dbo.Customer", t => t.CustomerID)
                .ForeignKey("dbo.OrderType", t => t.OrderTypeId)
                .Index(t => t.CustomerID)
                .Index(t => t.AddressID)
                .Index(t => t.OrderTypeId)
                .Index(t => t.CouponId);
            
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Flat = c.String(maxLength: 100),
                        Area = c.String(maxLength: 100),
                        LandMark = c.String(maxLength: 100),
                        DeliveryAddress = c.String(maxLength: 1024),
                        CustomerID = c.Int(nullable: false),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        AddressType = c.Int(nullable: false),
                        Pincode = c.String(),
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
                "dbo.Payment",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrderID = c.Int(nullable: false),
                        PaymentID = c.String(),
                        Success = c.Boolean(nullable: false),
                        Signature = c.String(),
                        PaymentOrderID = c.String(),
                        IsPaymentSuccess = c.Boolean(nullable: false),
                        PaymentErrorCode = c.Int(nullable: false),
                        PaymentErrorMsg = c.String(),
                        Status = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                        UpdatedBy = c.String(maxLength: 1000),
                        DeletedBy = c.String(maxLength: 100),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Order", t => t.OrderID)
                .Index(t => t.OrderID);
            
            CreateTable(
                "dbo.OrderStatusMapping",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        OrderStatus = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetail", "ServiceTypeID", "dbo.ServiceType");
            DropForeignKey("dbo.OrderDetail", "OrderID", "dbo.Order");
            DropForeignKey("dbo.Payment", "OrderID", "dbo.Order");
            DropForeignKey("dbo.Order", "OrderTypeId", "dbo.OrderType");
            DropForeignKey("dbo.Order", "CustomerID", "dbo.Customer");
            DropForeignKey("dbo.Order", "CouponId", "dbo.Coupon");
            DropForeignKey("dbo.Order", "AddressID", "dbo.Address");
            DropForeignKey("dbo.OrderDetail", "ClothsID", "dbo.Cloths");
            DropIndex("dbo.Payment", new[] { "OrderID" });
            DropIndex("dbo.Order", new[] { "CouponId" });
            DropIndex("dbo.Order", new[] { "OrderTypeId" });
            DropIndex("dbo.Order", new[] { "AddressID" });
            DropIndex("dbo.Order", new[] { "CustomerID" });
            DropIndex("dbo.OrderDetail", new[] { "ClothsID" });
            DropIndex("dbo.OrderDetail", new[] { "ServiceTypeID" });
            DropIndex("dbo.OrderDetail", new[] { "OrderID" });
            DropTable("dbo.OrderStatusMapping");
            DropTable("dbo.Payment");
            DropTable("dbo.OrderType");
            DropTable("dbo.Address");
            DropTable("dbo.Order");
            DropTable("dbo.OrderDetail");
            DropTable("dbo.OrderDelvieryBoyMapping");
        }
    }
}
