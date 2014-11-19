using ImgGroup.Common.Entities;
using ImgGroup.OnlineStore.Infrastructure;
using ImgGroup.OnlineStore.WebHost.Representations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace ImgGroup.OnlineStore.WebHost.Controllers.Api
{    
    public class ProductsController : ApiController
    {
        readonly IReadOnlyRepository<Product, Guid> repository;
        readonly CancellationTokenSource cts;        

        public ProductsController(IReadOnlyRepository<Product, Guid> repository)
        {
            this.repository = repository;
            this.cts = new CancellationTokenSource();                        
        }

        [HttpGet]        
        public async Task<IHttpActionResult> Get(int pageIndex = 0, int pageSize = 10)
        {                        
            try
            {
                var pagedCollection = await this.repository.PaginateWithCountAsync(q => q.OrderBy(p => p.Name), pageIndex, pageSize, cts.Token);

                return Ok(new 
                {
                    Items = pagedCollection.Select(product => product.ToRepresentation()),
                    TotalCount = pagedCollection.TotalCount
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError("This is a trace error message for illustration purposes only");

                return InternalServerError(ex);
            }
        }
    }    
}