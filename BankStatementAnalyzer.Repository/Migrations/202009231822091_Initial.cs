namespace BankStatementAnalyzer.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Action",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        DisplayName = c.String(maxLength: 100),
                        Feature = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedOn = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Permission",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Feature = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedOn = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        DefaultController = c.String(nullable: false, maxLength: 100),
                        DefaultAction = c.String(nullable: false, maxLength: 100),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedOn = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Username = c.String(maxLength: 100),
                        Name = c.String(nullable: false, maxLength: 250),
                        IsSuperAdmin = c.Boolean(nullable: false),
                        IsFirstTimeLogin = c.Boolean(nullable: false),
                        Email = c.String(nullable: false, maxLength: 250),
                        Mobile = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedOn = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AppMessage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Key = c.String(nullable: false, maxLength: 100),
                        Message = c.String(nullable: false, maxLength: 2048),
                        Status = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                        UpdatedBy = c.String(maxLength: 1000),
                        DeletedBy = c.String(maxLength: 100),
                        CompanyId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.Company",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Code = c.String(nullable: false, maxLength: 30),
                        Key = c.String(maxLength: 100),
                        Status = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedOn = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AreaManager",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CityId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Pincode = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                        UpdatedBy = c.String(maxLength: 1000),
                        DeletedBy = c.String(maxLength: 100),
                        CompanyId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.City", t => t.CityId)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .Index(t => t.CityId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.City",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Code = c.String(nullable: false, maxLength: 500),
                        Status = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                        UpdatedBy = c.String(maxLength: 1000),
                        DeletedBy = c.String(maxLength: 100),
                        CompanyId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false),
                        FileId = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                        UpdatedBy = c.String(maxLength: 1000),
                        DeletedBy = c.String(maxLength: 100),
                        CompanyId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .ForeignKey("dbo.File", t => t.FileId)
                .Index(t => t.FileId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.File",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Path = c.String(nullable: false, maxLength: 250),
                        EntityName = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Extension = c.String(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                        UpdatedBy = c.String(maxLength: 1000),
                        UpdatedDate = c.DateTime(),
                        Product_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Product", t => t.Product_ID)
                .Index(t => t.Product_ID);
            
            CreateTable(
                "dbo.Complaint",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Status = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                        UpdatedBy = c.String(maxLength: 1000),
                        DeletedBy = c.String(maxLength: 100),
                        CompanyId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.Coupon",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Code = c.String(maxLength: 15),
                        DiscountType = c.Int(nullable: false),
                        MinTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Redemption = c.Int(nullable: false),
                        CustomerRedemption = c.Int(nullable: false),
                        Description = c.String(),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                        UpdatedBy = c.String(maxLength: 1000),
                        DeletedBy = c.String(maxLength: 100),
                        CompanyId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.CustomerCreditHistory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerCreditMappingId = c.Int(nullable: false),
                        AmountPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(),
                        UpdatedBy = c.String(),
                        CreatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CustomerCreditMapping",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        CreditAmount = c.Decimal(precision: 18, scale: 2),
                        AdvancePayment = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AmountDue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(),
                        UpdatedBy = c.String(),
                        CreatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CustomerRefferalCodeMapping",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        SelfCode = c.String(),
                        RefferalCode = c.String(),
                        IsCodeUsed = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 100),
                        PhoneNumber = c.String(nullable: false, maxLength: 15),
                        UID = c.String(nullable: false),
                        NickName = c.String(maxLength: 100),
                        HouseNo = c.String(maxLength: 50),
                        Pincode = c.String(maxLength: 6),
                        Area = c.String(maxLength: 50),
                        CompanyId = c.Int(nullable: false),
                        Street = c.String(maxLength: 200),
                        Email = c.String(maxLength: 150),
                        DeviceId = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ReferralCode = c.String(),
                        ReferFromCode = c.String(),
                        IsCreditApplicable = c.Boolean(nullable: false),
                        CreateBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DeliveryBoy",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Number = c.String(nullable: false, maxLength: 15),
                        Email = c.String(),
                        Address = c.String(nullable: false, maxLength: 1024),
                        VehicleNumber = c.String(maxLength: 15),
                        Status = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                        UpdatedBy = c.String(maxLength: 1000),
                        DeletedBy = c.String(maxLength: 100),
                        CompanyId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.Email",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmailFor = c.String(nullable: false, maxLength: 100),
                        EmailSubject = c.String(nullable: false, maxLength: 100),
                        Description = c.String(),
                        Status = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                        UpdatedBy = c.String(maxLength: 1000),
                        DeletedBy = c.String(maxLength: 100),
                        CompanyId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.Gallery",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        ImageCategoryId = c.Int(nullable: false),
                        FileId = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                        UpdatedBy = c.String(maxLength: 1000),
                        DeletedBy = c.String(maxLength: 100),
                        CompanyId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .ForeignKey("dbo.File", t => t.FileId)
                .ForeignKey("dbo.ImageCategory", t => t.ImageCategoryId)
                .Index(t => t.ImageCategoryId)
                .Index(t => t.FileId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.ImageCategory",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Status = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                        UpdatedBy = c.String(maxLength: 1000),
                        DeletedBy = c.String(maxLength: 100),
                        CompanyId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.Menu",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DisplayName = c.String(),
                        ActionName = c.String(),
                        Controller = c.String(),
                        QueryString = c.String(),
                        Order = c.Int(nullable: false),
                        ParentId = c.Int(),
                        Icon = c.String(maxLength: 100),
                        Separator = c.String(maxLength: 100),
                        HideFromSystemAdministrator = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                        UpdatedBy = c.String(maxLength: 1000),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Menu", t => t.ParentId)
                .Index(t => t.ParentId);
            
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
                        CompanyId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.OrderDetail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        Description = c.String(maxLength: 200),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscountedPercentage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalSavings = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Size = c.String(maxLength: 20),
                        ContactNo = c.String(maxLength: 15),
                        ExpectedDeliveryDate = c.DateTime(),
                        IsReorder = c.Boolean(nullable: false),
                        OrderStatus = c.Int(nullable: false),
                        IsProductReturnAbleReturned = c.Boolean(nullable: false),
                        IsNewCanRequired = c.Boolean(nullable: false),
                        NewCanQuantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Order", t => t.OrderID)
                .Index(t => t.OrderID);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomerID = c.Int(nullable: false),
                        UniqueOrderNo = c.String(),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaidAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WalletPoints = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TransactionID = c.String(maxLength: 200),
                        OrderStatus = c.Int(nullable: false),
                        TransactionReference = c.String(),
                        AddressID = c.Int(),
                        PaymentType = c.Int(nullable: false),
                        SystemOrderID = c.String(maxLength: 10),
                        Remark = c.String(maxLength: 1024),
                        CouponId = c.Int(nullable: false),
                        NoOfReturnAbleCans = c.Int(nullable: false),
                        DeliveryDate = c.DateTime(),
                        TimeSlotId = c.Int(nullable: false),
                        OrderType = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                        UpdatedBy = c.String(maxLength: 1000),
                        DeletedBy = c.String(maxLength: 100),
                        CompanyId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Address", t => t.AddressID)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .ForeignKey("dbo.Customer", t => t.CustomerID)
                .Index(t => t.CustomerID)
                .Index(t => t.AddressID)
                .Index(t => t.CompanyId);
            
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
                        CompanyId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .Index(t => t.CompanyId);
            
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
                        CompanyId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .ForeignKey("dbo.Order", t => t.OrderID)
                .Index(t => t.OrderID)
                .Index(t => t.CompanyId);
            
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
                "dbo.PageManager",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PageName = c.String(nullable: false, maxLength: 100),
                        PageTitle = c.String(nullable: false, maxLength: 100),
                        PageMetaKeyword = c.String(nullable: false, maxLength: 100),
                        PageMetaDescription = c.String(nullable: false, maxLength: 100),
                        PageDescription = c.String(nullable: false),
                        Status = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                        UpdatedBy = c.String(maxLength: 1000),
                        DeletedBy = c.String(maxLength: 100),
                        CompanyId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        Title = c.String(nullable: false, maxLength: 200),
                        ShortTitle = c.String(maxLength: 150),
                        Quantity = c.Int(nullable: false),
                        Code = c.String(nullable: false, maxLength: 100),
                        SKU = c.String(maxLength: 50),
                        Liters = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ActivateOn = c.DateTime(nullable: false),
                        DeactivateOn = c.DateTime(),
                        Description = c.String(),
                        DiscountType = c.Int(nullable: false),
                        SpecialPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        IsReturnAble = c.Boolean(nullable: false),
                        SubCategoryId = c.Int(),
                        UnitOfMeasureId = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                        UpdatedBy = c.String(maxLength: 1000),
                        DeletedBy = c.String(maxLength: 100),
                        CompanyId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .ForeignKey("dbo.SubCategory", t => t.SubCategoryId)
                .ForeignKey("dbo.UnitOfMeasure", t => t.UnitOfMeasureId)
                .Index(t => t.SubCategoryId)
                .Index(t => t.UnitOfMeasureId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.SubCategory",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Key = c.String(maxLength: 50),
                        Description = c.String(maxLength: 1024),
                        ParentId = c.Int(),
                        CategoryID = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                        UpdatedBy = c.String(maxLength: 1000),
                        DeletedBy = c.String(maxLength: 100),
                        CompanyId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.UnitOfMeasure",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false, maxLength: 100),
                        Status = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                        UpdatedBy = c.String(maxLength: 1000),
                        DeletedBy = c.String(maxLength: 100),
                        CompanyId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.PushNotification",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Text = c.String(),
                        CustomerName = c.String(),
                        Status = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                        UpdatedBy = c.String(maxLength: 1000),
                        DeletedBy = c.String(maxLength: 100),
                        CompanyId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.ReturnRequestDeliveryBoyMapping",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ReturnRequestId = c.Int(nullable: false),
                        DeliveryBoyId = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                        UpdatedBy = c.String(maxLength: 1000),
                        DeletedBy = c.String(maxLength: 100),
                        CompanyId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.ReturnRequest",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NoOfCans = c.Int(nullable: false),
                        ReturnRequestType = c.Int(nullable: false),
                        Remark = c.String(nullable: false),
                        AddressId = c.Int(nullable: false),
                        CustomerUID = c.String(),
                        OrderStatus = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                        UpdatedBy = c.String(maxLength: 1000),
                        DeletedBy = c.String(maxLength: 100),
                        CompanyId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.Store",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Address = c.String(maxLength: 1024),
                        PhoneNumber = c.String(maxLength: 15),
                        StoreType = c.Int(nullable: false),
                        Pincode = c.String(nullable: false, maxLength: 6),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        IsInstaServiceProvided = c.Boolean(nullable: false),
                        Status = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                        UpdatedBy = c.String(maxLength: 1000),
                        DeletedBy = c.String(maxLength: 100),
                        CompanyId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.Street",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 2024),
                        AreaId = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                        UpdatedBy = c.String(maxLength: 1000),
                        DeletedBy = c.String(maxLength: 100),
                        CompanyId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AreaManager", t => t.AreaId)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .Index(t => t.AreaId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.StyleClass",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(maxLength: 1024),
                        Status = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                        UpdatedBy = c.String(maxLength: 1000),
                        DeletedBy = c.String(maxLength: 100),
                        CompanyId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.StyleTrait",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StyleClassId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        SortOrder = c.Int(nullable: false),
                        Description = c.String(),
                        Status = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                        UpdatedBy = c.String(maxLength: 1000),
                        DeletedBy = c.String(maxLength: 100),
                        CompanyId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.StyleTraitValue",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StyleTraitId = c.Int(nullable: false),
                        Value = c.String(nullable: false),
                        Description = c.String(),
                        Status = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                        UpdatedBy = c.String(maxLength: 1000),
                        DeletedBy = c.String(maxLength: 100),
                        CompanyId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.StyleTraitValueProduct",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        StyleTraitValueId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SupportAndFAQ",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 200),
                        Description = c.String(nullable: false, maxLength: 2400),
                        FAQType = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                        UpdatedBy = c.String(maxLength: 1000),
                        DeletedBy = c.String(maxLength: 100),
                        CompanyId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.SystemSetting",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IsVisibleToAdmin = c.Boolean(nullable: false),
                        SettingTypeValue = c.String(nullable: false),
                        SettingType = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 128),
                        Value = c.String(nullable: false),
                        Comments = c.String(maxLength: 1024),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 100),
                        UpdatedBy = c.String(maxLength: 1000),
                        DeletedBy = c.String(maxLength: 100),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TimeSlot",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        From = c.DateTime(nullable: false),
                        To = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                        UpdatedBy = c.String(maxLength: 1000),
                        DeletedBy = c.String(maxLength: 100),
                        CompanyId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.UndeliveryReason",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Reason = c.String(nullable: false),
                        Status = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                        UpdatedBy = c.String(maxLength: 1000),
                        DeletedBy = c.String(maxLength: 100),
                        CompanyId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.UserCompanyMapping",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Wallet",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomerId = c.String(),
                        TransactionDetail = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Transactiontype = c.Int(nullable: false),
                        TransactionDate = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                        UpdatedBy = c.String(maxLength: 1000),
                        DeletedBy = c.String(maxLength: 100),
                        CompanyId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.PermissionAction",
                c => new
                    {
                        Permission_ID = c.Int(nullable: false),
                        Action_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Permission_ID, t.Action_ID })
                .ForeignKey("dbo.Permission", t => t.Permission_ID)
                .ForeignKey("dbo.Action", t => t.Action_ID)
                .Index(t => t.Permission_ID)
                .Index(t => t.Action_ID);
            
            CreateTable(
                "dbo.RolePermission",
                c => new
                    {
                        Role_ID = c.Int(nullable: false),
                        Permission_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_ID, t.Permission_ID })
                .ForeignKey("dbo.Role", t => t.Role_ID)
                .ForeignKey("dbo.Permission", t => t.Permission_ID)
                .Index(t => t.Role_ID)
                .Index(t => t.Permission_ID);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        User_ID = c.Int(nullable: false),
                        Role_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_ID, t.Role_ID })
                .ForeignKey("dbo.User", t => t.User_ID)
                .ForeignKey("dbo.Role", t => t.Role_ID)
                .Index(t => t.User_ID)
                .Index(t => t.Role_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Wallet", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.UndeliveryReason", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.TimeSlot", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.SupportAndFAQ", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.StyleTraitValue", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.StyleTrait", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.StyleClass", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Street", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Street", "AreaId", "dbo.AreaManager");
            DropForeignKey("dbo.Store", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.ReturnRequest", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.ReturnRequestDeliveryBoyMapping", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.PushNotification", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Product", "UnitOfMeasureId", "dbo.UnitOfMeasure");
            DropForeignKey("dbo.UnitOfMeasure", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Product", "SubCategoryId", "dbo.SubCategory");
            DropForeignKey("dbo.SubCategory", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.File", "Product_ID", "dbo.Product");
            DropForeignKey("dbo.Product", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.PageManager", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.OrderDetail", "OrderID", "dbo.Order");
            DropForeignKey("dbo.Payment", "OrderID", "dbo.Order");
            DropForeignKey("dbo.Payment", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Order", "CustomerID", "dbo.Customer");
            DropForeignKey("dbo.Order", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Order", "AddressID", "dbo.Address");
            DropForeignKey("dbo.Address", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.OrderDelvieryBoyMapping", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Menu", "ParentId", "dbo.Menu");
            DropForeignKey("dbo.Gallery", "ImageCategoryId", "dbo.ImageCategory");
            DropForeignKey("dbo.ImageCategory", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Gallery", "FileId", "dbo.File");
            DropForeignKey("dbo.Gallery", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Email", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.DeliveryBoy", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Coupon", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Complaint", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Category", "FileId", "dbo.File");
            DropForeignKey("dbo.Category", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.AreaManager", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.AreaManager", "CityId", "dbo.City");
            DropForeignKey("dbo.City", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.AppMessage", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.UserRole", "Role_ID", "dbo.Role");
            DropForeignKey("dbo.UserRole", "User_ID", "dbo.User");
            DropForeignKey("dbo.RolePermission", "Permission_ID", "dbo.Permission");
            DropForeignKey("dbo.RolePermission", "Role_ID", "dbo.Role");
            DropForeignKey("dbo.PermissionAction", "Action_ID", "dbo.Action");
            DropForeignKey("dbo.PermissionAction", "Permission_ID", "dbo.Permission");
            DropIndex("dbo.UserRole", new[] { "Role_ID" });
            DropIndex("dbo.UserRole", new[] { "User_ID" });
            DropIndex("dbo.RolePermission", new[] { "Permission_ID" });
            DropIndex("dbo.RolePermission", new[] { "Role_ID" });
            DropIndex("dbo.PermissionAction", new[] { "Action_ID" });
            DropIndex("dbo.PermissionAction", new[] { "Permission_ID" });
            DropIndex("dbo.Wallet", new[] { "CompanyId" });
            DropIndex("dbo.UndeliveryReason", new[] { "CompanyId" });
            DropIndex("dbo.TimeSlot", new[] { "CompanyId" });
            DropIndex("dbo.SupportAndFAQ", new[] { "CompanyId" });
            DropIndex("dbo.StyleTraitValue", new[] { "CompanyId" });
            DropIndex("dbo.StyleTrait", new[] { "CompanyId" });
            DropIndex("dbo.StyleClass", new[] { "CompanyId" });
            DropIndex("dbo.Street", new[] { "CompanyId" });
            DropIndex("dbo.Street", new[] { "AreaId" });
            DropIndex("dbo.Store", new[] { "CompanyId" });
            DropIndex("dbo.ReturnRequest", new[] { "CompanyId" });
            DropIndex("dbo.ReturnRequestDeliveryBoyMapping", new[] { "CompanyId" });
            DropIndex("dbo.PushNotification", new[] { "CompanyId" });
            DropIndex("dbo.UnitOfMeasure", new[] { "CompanyId" });
            DropIndex("dbo.SubCategory", new[] { "CompanyId" });
            DropIndex("dbo.Product", new[] { "CompanyId" });
            DropIndex("dbo.Product", new[] { "UnitOfMeasureId" });
            DropIndex("dbo.Product", new[] { "SubCategoryId" });
            DropIndex("dbo.PageManager", new[] { "CompanyId" });
            DropIndex("dbo.Payment", new[] { "CompanyId" });
            DropIndex("dbo.Payment", new[] { "OrderID" });
            DropIndex("dbo.Address", new[] { "CompanyId" });
            DropIndex("dbo.Order", new[] { "CompanyId" });
            DropIndex("dbo.Order", new[] { "AddressID" });
            DropIndex("dbo.Order", new[] { "CustomerID" });
            DropIndex("dbo.OrderDetail", new[] { "OrderID" });
            DropIndex("dbo.OrderDelvieryBoyMapping", new[] { "CompanyId" });
            DropIndex("dbo.Menu", new[] { "ParentId" });
            DropIndex("dbo.ImageCategory", new[] { "CompanyId" });
            DropIndex("dbo.Gallery", new[] { "CompanyId" });
            DropIndex("dbo.Gallery", new[] { "FileId" });
            DropIndex("dbo.Gallery", new[] { "ImageCategoryId" });
            DropIndex("dbo.Email", new[] { "CompanyId" });
            DropIndex("dbo.DeliveryBoy", new[] { "CompanyId" });
            DropIndex("dbo.Coupon", new[] { "CompanyId" });
            DropIndex("dbo.Complaint", new[] { "CompanyId" });
            DropIndex("dbo.File", new[] { "Product_ID" });
            DropIndex("dbo.Category", new[] { "CompanyId" });
            DropIndex("dbo.Category", new[] { "FileId" });
            DropIndex("dbo.City", new[] { "CompanyId" });
            DropIndex("dbo.AreaManager", new[] { "CompanyId" });
            DropIndex("dbo.AreaManager", new[] { "CityId" });
            DropIndex("dbo.AppMessage", new[] { "CompanyId" });
            DropTable("dbo.UserRole");
            DropTable("dbo.RolePermission");
            DropTable("dbo.PermissionAction");
            DropTable("dbo.Wallet");
            DropTable("dbo.UserCompanyMapping");
            DropTable("dbo.UndeliveryReason");
            DropTable("dbo.TimeSlot");
            DropTable("dbo.SystemSetting");
            DropTable("dbo.SupportAndFAQ");
            DropTable("dbo.StyleTraitValueProduct");
            DropTable("dbo.StyleTraitValue");
            DropTable("dbo.StyleTrait");
            DropTable("dbo.StyleClass");
            DropTable("dbo.Street");
            DropTable("dbo.Store");
            DropTable("dbo.ReturnRequest");
            DropTable("dbo.ReturnRequestDeliveryBoyMapping");
            DropTable("dbo.PushNotification");
            DropTable("dbo.UnitOfMeasure");
            DropTable("dbo.SubCategory");
            DropTable("dbo.Product");
            DropTable("dbo.PageManager");
            DropTable("dbo.OrderStatusMapping");
            DropTable("dbo.Payment");
            DropTable("dbo.Address");
            DropTable("dbo.Order");
            DropTable("dbo.OrderDetail");
            DropTable("dbo.OrderDelvieryBoyMapping");
            DropTable("dbo.Menu");
            DropTable("dbo.ImageCategory");
            DropTable("dbo.Gallery");
            DropTable("dbo.Email");
            DropTable("dbo.DeliveryBoy");
            DropTable("dbo.Customer");
            DropTable("dbo.CustomerRefferalCodeMapping");
            DropTable("dbo.CustomerCreditMapping");
            DropTable("dbo.CustomerCreditHistory");
            DropTable("dbo.Coupon");
            DropTable("dbo.Complaint");
            DropTable("dbo.File");
            DropTable("dbo.Category");
            DropTable("dbo.City");
            DropTable("dbo.AreaManager");
            DropTable("dbo.Company");
            DropTable("dbo.AppMessage");
            DropTable("dbo.User");
            DropTable("dbo.Role");
            DropTable("dbo.Permission");
            DropTable("dbo.Action");
        }
    }
}
