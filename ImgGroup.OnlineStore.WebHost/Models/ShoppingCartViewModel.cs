using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImgGroup.OnlineStore.WebHost.Models
{
    /// <summary>
    /// Used mainly to layout the view in a strongly typed fashion
    /// </summary>
    public class ShoppingCartViewModel
    {
        public class CartItemViewModel 
        {
            public string ProductId { get; set; }
            public string ProductName { get; set; }
            public double ProductPrice { get; set; }
            public double Quantity { get; set; }
        }

        public ShoppingCartViewModel()
        {
            this.Items = new List<CartItemViewModel>();
        }

        public IEnumerable<CartItemViewModel> Items { get; set; }
    }
}