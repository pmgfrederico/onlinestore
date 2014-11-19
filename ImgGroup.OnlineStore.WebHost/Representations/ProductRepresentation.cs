using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImgGroup.OnlineStore.WebHost.Representations
{
    public class ProductRepresentation
    {
        public Guid Key { get; set; }
        public string   ProductId { get; set; }
        public string Name { get; set; }        
    }
}