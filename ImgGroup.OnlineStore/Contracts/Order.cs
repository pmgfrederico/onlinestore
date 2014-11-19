using ImgGroup.Common.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImgGroup.OnlineStore.Contracts
{
    public sealed class Order : IEntity<Guid>
    {
        public class OrderItem //TODO Discuss IOrderItem interface, generic Quantity field an Units. Considered out of the scope of the exercise
        {
            public Product Product { get; set; }
            public double Quantity { get; set; }
        }

        internal Order(Customer customer, 
            object deliveryAddress, 
            object billindAddress, 
            IEnumerable<OrderItem> items) //TODO tipify addresses, add status history and so on
        {
            System.Diagnostics.Contracts.Contract.Requires<ArgumentNullException>(customer != null);
            System.Diagnostics.Contracts.Contract.Requires<ArgumentNullException>(deliveryAddress != null);
            System.Diagnostics.Contracts.Contract.Requires<ArgumentNullException>(billindAddress != null);
            System.Diagnostics.Contracts.Contract.Requires<ArgumentNullException>(items != null);
            System.Diagnostics.Contracts.Contract.Requires<ArgumentException>(items.Count() > 0, "Order items collection size needs to be greater than zero");

            this.Customer = customer;
            this.DeliveryAddress = deliveryAddress;
            this.BillingAddress = billindAddress;
            this.Items = new ReadOnlyCollection<OrderItem>(new List<OrderItem>(items));
        }

        public Customer Customer { get; private set; }

        public object BillingAddress { get; private set; }

        public object DeliveryAddress { get; private set; }

        public IReadOnlyCollection<OrderItem> Items { get; private set; }

        #region IEntity<Guid> implementation

        public Guid Id
        {
            get { throw new NotImplementedException(); }
        }
        #endregion

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
