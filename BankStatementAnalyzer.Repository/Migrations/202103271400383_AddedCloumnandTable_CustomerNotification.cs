namespace BankStatementAnalyzer.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCloumnandTable_CustomerNotification : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerNotification",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NotificationId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                        UpdatedBy = c.String(maxLength: 1000),
                        DeletedBy = c.String(maxLength: 100),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Customer", t => t.CustomerId)
                .ForeignKey("dbo.PushNotification", t => t.NotificationId)
                .Index(t => t.NotificationId)
                .Index(t => t.CustomerId);
            
            AddColumn("dbo.PushNotification", "ClassifiedId", c => c.Int(nullable: false));
            AddColumn("dbo.PushNotification", "ImageURL", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerNotification", "NotificationId", "dbo.PushNotification");
            DropForeignKey("dbo.CustomerNotification", "CustomerId", "dbo.Customer");
            DropIndex("dbo.CustomerNotification", new[] { "CustomerId" });
            DropIndex("dbo.CustomerNotification", new[] { "NotificationId" });
            DropColumn("dbo.PushNotification", "ImageURL");
            DropColumn("dbo.PushNotification", "ClassifiedId");
            DropTable("dbo.CustomerNotification");
        }
    }
}
