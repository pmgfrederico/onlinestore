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
        public virtual IDbSet<Customer> Customers { get; set; }
        public virtual IDbSet<Order> Orders { get; set; }

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

            modelBuilder.ComplexType<Address>();

            var orderModel = modelBuilder.Entity<Order>();
            orderModel.ToTable("Orders")
                .Property(model => model.Id)
                .HasColumnName("OrderId");
            orderModel.HasKey(model => model.Id);
            orderModel.HasRequired(model => model.Customer)
                .WithMany()
                .Map(cfg => cfg.MapKey("CustomerId"))
                .WillCascadeOnDelete(false);            
            orderModel.HasMany(model => model.StatusHistory)
                .WithRequired()                
                .HasForeignKey(model => model.AggregateId)
                .WillCascadeOnDelete(true);
            orderModel.HasMany(model => model.Addresses)
                .WithRequired()
                .HasForeignKey(model => model.AggregateId)
                .WillCascadeOnDelete(true);
            orderModel.HasMany(model => model.Items)
                .WithRequired()
                .HasForeignKey(model => model.AggregateId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Order.OrderStatus>().Property(model => model.Id).HasColumnName("OrderStatusId");
            modelBuilder.Entity<Order.OrderStatus>().HasKey(model => model.Id);
            modelBuilder.Entity<Order.OrderStatus>()
                .Property(model => model.AggregateId)
                .HasColumnName("OrderId");

            modelBuilder.Entity<Order.OrderAddress>().Property(model => model.Id).HasColumnName("OrderAddressId");
            modelBuilder.Entity<Order.OrderAddress>().HasKey(model => model.Id);
            modelBuilder.Entity<Order.OrderAddress>()
                .Property(model => model.AggregateId)
                .HasColumnName("OrderId");

            modelBuilder.Entity<Order.OrderItem>().Property(model => model.Id).HasColumnName("OrderItemId");
            modelBuilder.Entity<Order.OrderItem>().HasKey(model => model.Id);
            modelBuilder.Entity<Order.OrderItem>()
                .Property(model => model.AggregateId)
                .HasColumnName("OrderId");
            modelBuilder.Entity<Order.OrderItem>().HasRequired(model => model.Product)
                .WithMany()
                .Map(cfg => cfg.MapKey("ProductId"))
                .WillCascadeOnDelete(false);   

            var customerModel = modelBuilder.Entity<Customer>();
            customerModel.ToTable("Customers")
                .Property(model => model.Id)
                .HasColumnName("CustomerId");
            customerModel.HasKey(model => model.Id);            
        }
    }
}
