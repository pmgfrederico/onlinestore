namespace ImgGroup.OnlineStore.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "ShoppingCart.Products",
                c => new
                    {
                        ProductId = c.Guid(nullable: false),
                        Identifier = c.String(),
                        Name = c.String(),
                        AlternateName = c.String(),
                        Description = c.String(),
                        Sku = c.String(),
                        DefaultPrice = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "ShoppingCart.ShoppingSessions",
                c => new
                    {
                        ShoppingSessionId = c.Guid(nullable: false),
                        ShopperId = c.String(),
                        ExternalOrderId = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ShoppingSessionId);
            
        }
        
        public override void Down()
        {
            DropTable("ShoppingCart.ShoppingSessions");
            DropTable("ShoppingCart.Products");
        }
    }
}
