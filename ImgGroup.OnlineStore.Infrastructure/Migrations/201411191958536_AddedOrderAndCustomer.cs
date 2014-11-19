namespace ImgGroup.OnlineStore.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedOrderAndCustomer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "ShoppingCart.Customers",
                c => new
                    {
                        CustomerId = c.Guid(nullable: false),
                        Name = c.String(),
                        FiscalNumber = c.String(),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "ShoppingCart.Orders",
                c => new
                    {
                        OrderId = c.Guid(nullable: false),
                        Currency_Code = c.String(),
                        CustomerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("ShoppingCart.Customers", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "ShoppingCart.OrderAddresses",
                c => new
                    {
                        OrderAddressId = c.Guid(nullable: false),
                        Role = c.Int(nullable: false),
                        Address_Line = c.String(),
                        Address_ExtraLine = c.String(),
                        Address_City = c.String(),
                        Address_Region = c.String(),
                        Address_PostalCode = c.String(),
                        Address_Country = c.String(),
                        OrderId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.OrderAddressId)
                .ForeignKey("ShoppingCart.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId);
            
            CreateTable(
                "ShoppingCart.OrderItems",
                c => new
                    {
                        OrderItemId = c.Guid(nullable: false),
                        Sku = c.Int(nullable: false),
                        Quantity = c.Double(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderId = c.Guid(nullable: false),
                        ProductId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.OrderItemId)
                .ForeignKey("ShoppingCart.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("ShoppingCart.Products", t => t.ProductId)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "ShoppingCart.OrderStatus",
                c => new
                    {
                        OrderStatusId = c.Guid(nullable: false),
                        Status = c.Int(nullable: false),
                        InstantUtc = c.DateTime(nullable: false),
                        OrderId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.OrderStatusId)
                .ForeignKey("ShoppingCart.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("ShoppingCart.OrderStatus", "OrderId", "ShoppingCart.Orders");
            DropForeignKey("ShoppingCart.OrderItems", "ProductId", "ShoppingCart.Products");
            DropForeignKey("ShoppingCart.OrderItems", "OrderId", "ShoppingCart.Orders");
            DropForeignKey("ShoppingCart.Orders", "CustomerId", "ShoppingCart.Customers");
            DropForeignKey("ShoppingCart.OrderAddresses", "OrderId", "ShoppingCart.Orders");
            DropIndex("ShoppingCart.OrderStatus", new[] { "OrderId" });
            DropIndex("ShoppingCart.OrderItems", new[] { "ProductId" });
            DropIndex("ShoppingCart.OrderItems", new[] { "OrderId" });
            DropIndex("ShoppingCart.OrderAddresses", new[] { "OrderId" });
            DropIndex("ShoppingCart.Orders", new[] { "CustomerId" });
            DropTable("ShoppingCart.OrderStatus");
            DropTable("ShoppingCart.OrderItems");
            DropTable("ShoppingCart.OrderAddresses");
            DropTable("ShoppingCart.Orders");
            DropTable("ShoppingCart.Customers");
        }
    }
}
