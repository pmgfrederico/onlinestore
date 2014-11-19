using ImgGroup.Common.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImgGroup.OnlineStore.Contracts
{
    public class Order : IEntity<Guid>
    {
        //TODO Discuss IOrderItem interface, generic Quantity field an Units. Considered out of the scope of the exercise
        public class OrderItem : Composition<Order, Guid>
        {
            public OrderItem()
            {

            }

            internal OrderItem(Product product, StockKeepingUnit sku, double quantity, decimal price)
            {
                System.Diagnostics.Contracts.Contract.Requires<ArgumentNullException>(product != null);
                System.Diagnostics.Contracts.Contract.Requires<ArgumentException>(quantity > 0);
                System.Diagnostics.Contracts.Contract.Requires<ArgumentException>(price >= 0);

                this.Id = Guid.NewGuid();
                this.Product = product;
                this.Sku = sku;
                this.Quantity = quantity;
                this.Price = price;
            }

            public Guid Id { get; private set; }

            public Product Product { get; internal set; }

            public StockKeepingUnit Sku { get; internal set; }

            public double Quantity { get; internal set; }

            public decimal Price { get; internal set; }
        }

        public class OrderAddress : Composition<Order, Guid>
        {

            public OrderAddress()
            {

            }

            internal OrderAddress(AddressRole role, Address address)
            {
                System.Diagnostics.Contracts.Contract.Requires<ArgumentNullException>(address != null);

                this.Id = Guid.NewGuid();
                this.Address = address;
                this.Role = role;
            }

            public Guid Id { get; private set; }
            public AddressRole Role { get; set; }
            public Address Address { get; set; }
        }
        
        public enum Status // TODO Over simplified. A better approach would be an append only model like Event Sourcing since the Order changes its state over time.
        {
            Created,
            Payed,
            Delivered,
            Canceled
        }

        public class OrderStatus : Composition<Order, Guid>
        {
            public OrderStatus()
            {

            }

            internal OrderStatus(Status status)
            {
                this.Id = Guid.NewGuid();
                this.Status = status;
                this.InstantUtc = DateTime.UtcNow;
            }

            public Guid Id { get; private set; }
            public Status Status { get; internal set; }
            public DateTime InstantUtc { get; internal set; }
        }

        internal Order(Customer customer, 
            Address deliveryAddress, 
            Address billindAddress, 
            IEnumerable<OrderItem> items) //TODO tipify addresses, add status history and so on
        {
            System.Diagnostics.Contracts.Contract.Requires<ArgumentNullException>(customer != null);
            System.Diagnostics.Contracts.Contract.Requires<ArgumentNullException>(deliveryAddress != null);
            System.Diagnostics.Contracts.Contract.Requires<ArgumentNullException>(billindAddress != null);
            System.Diagnostics.Contracts.Contract.Requires<ArgumentNullException>(items != null);
            System.Diagnostics.Contracts.Contract.Requires<ArgumentException>(items.Count() > 0, "Order items collection size needs to be greater than zero");

            this.Customer = customer;
            this.Addresses = new List<OrderAddress>(new[] 
            { 
                new OrderAddress(AddressRole.Delivery, deliveryAddress), 
                new OrderAddress(AddressRole.Billing, billindAddress)
            });
            this.Items = new ReadOnlyCollection<OrderItem>(new List<OrderItem>(items));
        }

        #region IEntity<Guid> implementation

        public Guid Id
        {
            get;
            private set;
        }

        #endregion

        public Customer Customer { get; internal set; }
        public virtual ICollection<OrderStatus> StatusHistory { get; internal set; } // TODO Should be readonly. Tackle this concerning EF
        public virtual ICollection<OrderAddress> Addresses { get; internal set; } // TODO Should be readonly. Tackle this concerning EF
        public virtual ICollection<OrderItem> Items { get; internal set; } // TODO Should be readonly. Tackle this concerning EF
        public Currency Currency { get; internal set; }
        
        #region IEntity<Guid> implementation
        object[] IEntity<Guid>.Key()
        {
            return new object[] { this.Id };
        }

        public bool Equals(IEntity<Guid> other)
        {
            if (other == null)
            {
                return false;
            }

            return (other as Product).Id == this.Id;
        }

        #endregion

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Order);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
