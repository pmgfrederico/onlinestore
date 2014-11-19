namespace ImgGroup.OnlineStore.Infrastructure.Migrations
{
    using Newtonsoft.Json;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ImgGroup.OnlineStore.Infrastructure.OnlineStoreDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ImgGroup.OnlineStore.Infrastructure.OnlineStoreDbContext context)
        {
            var data = JsonConvert.DeserializeObject<SampleData.ProductSchema[]>(SampleData.Products.ProductsCollectionJson);

            foreach(var item in data)
            {
                context.Products.AddOrUpdate(product => product.ProductId, new Product(item.ProductID.ToString(), item.ProductName) { 
                    DefaultPrice = Convert.ToDecimal(item.UnitPrice)
                });
            }
        }
    }
}
