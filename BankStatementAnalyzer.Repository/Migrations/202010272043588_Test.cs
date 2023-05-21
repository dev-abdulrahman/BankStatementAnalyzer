namespace BankStatementAnalyzer.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderDetail", "ClothsID", "dbo.Cloths");
            DropForeignKey("dbo.Order", "AddressID", "dbo.Address");
            DropForeignKey("dbo.Order", "CouponId", "dbo.Coupon");
            DropForeignKey("dbo.Order", "CustomerID", "dbo.Customer");
            DropForeignKey("dbo.Order", "Id", "dbo.OrderType");
            DropForeignKey("dbo.Payment", "OrderID", "dbo.Order");
            DropForeignKey("dbo.OrderDetail", "OrderID", "dbo.Order");
            DropForeignKey("dbo.OrderDetail", "ServiceTypeID", "dbo.ServiceType");
            DropIndex("dbo.OrderDetail", new[] { "OrderID" });
            DropIndex("dbo.OrderDetail", new[] { "ServiceTypeID" });
            DropIndex("dbo.OrderDetail", new[] { "ClothsID" });
            DropIndex("dbo.Order", new[] { "CustomerID" });
            DropIndex("dbo.Order", new[] { "AddressID" });
            DropIndex("dbo.Order", new[] { "Id" });
            DropIndex("dbo.Order", new[] { "CouponId" });
            DropIndex("dbo.Payment", new[] { "OrderID" });
            DropTable("dbo.OrderDelvieryBoyMapping");
            DropTable("dbo.OrderDetail");
            DropTable("dbo.Order");
            DropTable("dbo.Address");
            DropTable("dbo.OrderType");
            DropTable("dbo.Payment");
            DropTable("dbo.OrderStatusMapping");
        }
        
        public override void Down()
        {
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
                        Id = c.Int(nullable: false),
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
                .PrimaryKey(t => t.Id);
            
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
            
            CreateIndex("dbo.Payment", "OrderID");
            CreateIndex("dbo.Order", "CouponId");
            CreateIndex("dbo.Order", "Id");
            CreateIndex("dbo.Order", "AddressID");
            CreateIndex("dbo.Order", "CustomerID");
            CreateIndex("dbo.OrderDetail", "ClothsID");
            CreateIndex("dbo.OrderDetail", "ServiceTypeID");
            CreateIndex("dbo.OrderDetail", "OrderID");
            AddForeignKey("dbo.OrderDetail", "ServiceTypeID", "dbo.ServiceType", "ID");
            AddForeignKey("dbo.OrderDetail", "OrderID", "dbo.Order", "ID");
            AddForeignKey("dbo.Payment", "OrderID", "dbo.Order", "ID");
            AddForeignKey("dbo.Order", "Id", "dbo.OrderType", "ID");
            AddForeignKey("dbo.Order", "CustomerID", "dbo.Customer", "Id");
            AddForeignKey("dbo.Order", "CouponId", "dbo.Coupon", "ID");
            AddForeignKey("dbo.Order", "AddressID", "dbo.Address", "ID");
            AddForeignKey("dbo.OrderDetail", "ClothsID", "dbo.Cloths", "ID");
        }
    }
}
