using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImgGroup.OnlineStore.WebHost.Representations
{
    public static class RepresentationExtensions
    {
        public static ProductRepresentation ToRepresentation(this Product entity)        
        {
            System.Diagnostics.Contracts.Contract.Requires<ArgumentNullException>(entity != null);

            return new ProductRepresentation()
            {
                Key = entity.Id,
                ProductId = entity.ProductId,
                Name = entity.Name,
                Price = entity.DefaultPrice.HasValue ? entity.DefaultPrice.Value : 0
            };
        }
    }
}