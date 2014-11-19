using ImgGroup.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ImgGroup.OnlineStore.Contracts
{
    [DataContract]
    [KnownTypeAttribute(typeof(ShoppingCart))]
    public class ShoppingCart : IShoppingCart
    {
        public enum StockKeepingUnit
        {
            Unit,
            DecimalMeasure
        }

        public class CartItem : IEquatable<CartItem>
        {
            // TODO: Don't wan't to couple with another bounded context entity. I will have my represenation of product in the potential Shopping cart module
            // public Product Product { get; set; }
            public CartItem()
            {
            }

            internal CartItem(string productId, string productName, decimal productPrice, double quantity) //TODO Handle Sku as wel
            {
                this.ProductId = productId;
                this.ProductName = productName;
                this.ProductPrice = productPrice;
                this.Quantity = quantity;
            }

            public string ProductId { get; set; }
            public string ProductName { get; set; }
            public decimal ProductPrice { get; set; }
            public StockKeepingUnit Sku { get; set; }
            public double Quantity { get; set; }

            public bool Equals(CartItem other)
            {
                if (other == null)
                {
                    return false;
                }

                return String.Equals(this.ProductId, other.ProductId, StringComparison.OrdinalIgnoreCase);
            }

            public override bool Equals(object obj)
            {
                return this.Equals(obj as CartItem);
            }

            public override int GetHashCode()
            {
                return this.ProductId.GetHashCode();
            }
        }
                
        public ShoppingCart()
        {            
            this.Items = new HashSet<CartItem>();
        }

        [DataMember]
        public HashSet<CartItem> Items { get; private set; }

        public bool CanSerialize()
        {
            return true; // overkill implementation. 
            
            //TODO check for the presence of DataContract attribute
        }
    }
}
