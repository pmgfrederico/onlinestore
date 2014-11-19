using ImgGroup.OnlineStore.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImgGroup.OnlineStore.Infrastructure
{
    public class OnlineStoreDbContext : DbContext
    {
        /// <summary>
        /// A set of Products
        /// </summary>
        /// <remarks>
        /// Added virtual modifier to support mocking the DbSet. Since we are already coupling this type with DbContext it doesn't seem that serious to set the modifier to virtual.
        /// </remarks>
        public virtual IDbSet<Product> Products { get; set; }
        public virtual IDbSet<ShoppingSession> ShoppingSessions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // TODO Seed bootstrap data | not cool to have it hardcoded unless it's always internal to the domain problem. otherwise use and external data source

            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("ShoppingCart");

            var productModel = modelBuilder.Entity<Product>();

            productModel.ToTable("Products")
                .Property(model => model.Id)
                .HasColumnName("ProductId");
            productModel.Property(model => model.ProductId)
                .HasColumnName("Identifier");

            var shoppingSessionModel = modelBuilder.Entity<ShoppingSession>();

            shoppingSessionModel.ToTable("ShoppingSessions")
                .Property(model => model.Id)
                .HasColumnName("ShoppingSessionId");
            
        }
    }
}
