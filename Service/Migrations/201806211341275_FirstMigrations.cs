namespace Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigrations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerFIO = c.String(nullable: false),
                        Mail = c.String(),
                        CustomerPassword = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Entries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                        Sum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumPay = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateVisit = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderName = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderFurnitures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        FurnitureId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Furnitures", t => t.FurnitureId, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.FurnitureId);
            
            CreateTable(
                "dbo.Furnitures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FurnitureName = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderFurnitures", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.OrderFurnitures", "FurnitureId", "dbo.Furnitures");
            DropForeignKey("dbo.Entries", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Entries", "CustomerId", "dbo.Customers");
            DropIndex("dbo.OrderFurnitures", new[] { "FurnitureId" });
            DropIndex("dbo.OrderFurnitures", new[] { "OrderId" });
            DropIndex("dbo.Entries", new[] { "OrderId" });
            DropIndex("dbo.Entries", new[] { "CustomerId" });
            DropTable("dbo.Furnitures");
            DropTable("dbo.OrderFurnitures");
            DropTable("dbo.Orders");
            DropTable("dbo.Entries");
            DropTable("dbo.Customers");
        }
    }
}
