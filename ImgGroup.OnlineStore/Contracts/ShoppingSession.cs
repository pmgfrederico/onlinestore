using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImgGroup.Common.Serialization;
using ImgGroup.Common.Entities;

namespace ImgGroup.OnlineStore.Contracts
{
    public class ShoppingSession : IEntity<Guid>, IShoppingSession
    {
        public ShoppingSession()
        {            
        }

        internal ShoppingSession(string shopperId)
        {
            this.Id = Guid.NewGuid();
            this.ShopperId = shopperId;
            this.CreatedOn = DateTime.UtcNow;
        }
        
        public Guid Id { get; private set; }
        public string ShopperId { get; set; }
        /// <summary>
        /// If an OrderId is present the cart has been checked out
        /// </summary>
        public string ExternalOrderId { get; set; }
        public byte[] CartData { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime? ModifiedOn { get; set; }


        public IShoppingCart GetCart()
        {
            
            return (this.CartData == null) 
                ? new ShoppingCart()  // TODO Use a factory instead
                : this.CartData.Deserialize<ShoppingCart>();
        }

        public void UpdateCart(IShoppingCart cart)
        {
            System.Diagnostics.Contracts.Contract.Requires<ArgumentNullException>(cart != null);

            this.CartData = cart.Serialize();
            this.ModifiedOn = DateTime.UtcNow;
        }

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

            return (other as ShoppingSession).Id == this.Id;
        }

        #endregion

        public override bool Equals(object obj)
        {            
            return this.Equals(obj as ShoppingSession);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
