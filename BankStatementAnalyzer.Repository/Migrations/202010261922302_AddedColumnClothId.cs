namespace BankStatementAnalyzer.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedColumnClothId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Payment", "OrderID", "dbo.Order");
            DropForeignKey("dbo.OrderDetail", "OrderID", "dbo.Order");
            DropIndex("dbo.Order", new[] { "Id" });
            DropPrimaryKey("dbo.Order");
            AddColumn("dbo.OrderDetail", "ClothsID", c => c.Int(nullable: false));
            AlterColumn("dbo.Order", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Order", "ID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Order", "ID");
            CreateIndex("dbo.OrderDetail", "ClothsID");
            CreateIndex("dbo.Order", "Id");
            AddForeignKey("dbo.OrderDetail", "ClothsID", "dbo.Cloths", "ID");
            AddForeignKey("dbo.Payment", "OrderID", "dbo.Order", "ID");
            AddForeignKey("dbo.OrderDetail", "OrderID", "dbo.Order", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetail", "OrderID", "dbo.Order");
            DropForeignKey("dbo.Payment", "OrderID", "dbo.Order");
            DropForeignKey("dbo.OrderDetail", "ClothsID", "dbo.Cloths");
            DropIndex("dbo.Order", new[] { "Id" });
            DropIndex("dbo.OrderDetail", new[] { "ClothsID" });
            DropPrimaryKey("dbo.Order");
            AlterColumn("dbo.Order", "ID", c => c.Int(nullable: false));
            AlterColumn("dbo.Order", "Id", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.OrderDetail", "ClothsID");
            AddPrimaryKey("dbo.Order", "ID");
            CreateIndex("dbo.Order", "Id");
            AddForeignKey("dbo.OrderDetail", "OrderID", "dbo.Order", "ID");
            AddForeignKey("dbo.Payment", "OrderID", "dbo.Order", "ID");
        }
    }
}
