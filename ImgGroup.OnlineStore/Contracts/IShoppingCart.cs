using ImgGroup.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ImgGroup.OnlineStore.Contracts
{ 
    public interface IShoppingCart : ISerializableEntity
    {
        HashSet<ShoppingCart.CartItem> Items { get; } // TODO Create interface for IShoppingCart.ICartItem
    }   
}
