using ImgGroup.Common.Entities;
using ImgGroup.OnlineStore.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace ImgGroup.OnlineStore.WebHost.Controllers.Api
{
    public class ShoppingCartController : ApiController
    {
        public class ShoppingCartCommandModelBinder : IModelBinder
        {
            public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
            {
                var content = actionContext.Request.Content.ReadAsStringAsync().Result;

                var obj = JsonConvert.DeserializeObject(content,
                    bindingContext.ModelType,
                    new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });

                bindingContext.Model = obj;

                return obj != null;
            }
        }
        
        readonly IDictionary<Type, dynamic> commandCatalog;
        readonly IShoppingSessionRepository sessionRepository;
        public ShoppingCartController(IDomainCommandHandler<AddItemCommand> addItemCommandHandler,
            IDomainCommandHandler<UpdateItemCommand> updateItemCommandHandler,
            IDomainCommandHandler<RemoveItemCommand> removeItemCommandHandler,
            IShoppingSessionRepository sessionRepository)
        {
            System.Diagnostics.Contracts.Contract.Requires<ArgumentNullException>(addItemCommandHandler != null);
            System.Diagnostics.Contracts.Contract.Requires<ArgumentNullException>(sessionRepository != null);

            this.commandCatalog = new Dictionary<Type, dynamic>()
            {
                { typeof(AddItemCommand), addItemCommandHandler },
                { typeof(UpdateItemCommand), updateItemCommandHandler },
                { typeof(RemoveItemCommand), removeItemCommandHandler }
            };
            this.sessionRepository = sessionRepository;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Execute([ModelBinder(typeof(ShoppingCartCommandModelBinder))]IShoppingCartCommand cmd)
        {
            try
            {
                Type commandHandlerType = typeof(IDomainCommandHandler<>)
                    .MakeGenericType(cmd.GetType());                

                var commandHandler = this.commandCatalog[cmd.GetType()];

                await commandHandler.HandleAsync(cmd, CancellationToken.None);

                return Ok();

            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.Message);

                return InternalServerError(ex); // TODO normalize exception handling and messaging
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get(string id)
        {
            var session = await this.sessionRepository.RetrieveShopperSessionAsync(id, CancellationToken.None);

            if(session == null)
                return Ok(Enumerable.Empty<object>());

            return Ok(session.GetCart().Items.Select(ci => new {
                shopperId = id,
                productId = ci.ProductId,
                productName = ci.ProductName,
                quantity = ci.Quantity
            })); //TODO add price
        }
    }
}
