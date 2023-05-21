namespace DeliveryManagement.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedColumnCustomerType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "CustomerType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "CustomerType");
        }
    }
}
