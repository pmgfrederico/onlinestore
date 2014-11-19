namespace ImgGroup.OnlineStore.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCartData : DbMigration
    {
        public override void Up()
        {
            AddColumn("ShoppingCart.ShoppingSessions", "CartData", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("ShoppingCart.ShoppingSessions", "CartData");
        }
    }
}
