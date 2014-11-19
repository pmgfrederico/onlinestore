using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImgGroup.OnlineStore.Contracts
{
    public interface IShoppingSession
    {        
        IShoppingCart GetCart();
        void UpdateCart(IShoppingCart cart);
    }    
}
