using ImgGroup.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImgGroup.OnlineStore.Contracts
{
    public class AddItemCommandHandler : IDomainCommandHandler<AddItemCommand>
    {
        readonly IShoppingSessionRepository sessionRepository;
        readonly IReadOnlyRepository<Product, Guid> productRepository;

        public AddItemCommandHandler(IShoppingSessionRepository sessionRepository, IReadOnlyRepository<Product, Guid> productRepository)
        {
            this.sessionRepository = sessionRepository;
            this.productRepository = productRepository;
        }

        public async Task HandleAsync(IDomainCommand command, System.Threading.CancellationToken ct)
        {
            System.Diagnostics.Contracts.Contract.Requires<ArgumentNullException>((command as AddItemCommand) != null);

            var cmd = command as AddItemCommand;

            var session = (await this.sessionRepository.RetrieveShopperSessionAsync(cmd.ShopperId, ct)) ?? new ShoppingSession(cmd.ShopperId);
            
            var product = await this.productRepository.RetrieveAsync<Product>(q => q.SingleOrDefault(p => p.ProductId == cmd.ProductId), ct);

            var cart = session.GetCart();

            if(!cart.Items.Add(new ShoppingCart.CartItem(product.ProductId, product.Name, cmd.Quantity)))
            {
                cart.Items.Single(ci => ci.ProductId == product.ProductId).Quantity += cmd.Quantity;
            }

            session.UpdateCart(cart);

            await this.sessionRepository.SaveShopperSessionAsync(session, ct);
        }
    }

    public class UpdateItemCommandHandler : IDomainCommandHandler<UpdateItemCommand>
    {
        readonly IShoppingSessionRepository sessionRepository;

        public UpdateItemCommandHandler(IShoppingSessionRepository sessionRepository)
        {
            this.sessionRepository = sessionRepository;            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException">Shopping session not found</exception>
        /// <exception cref="InvalidOperationException">If a given shopping cart item is not found</exception>
        public async Task HandleAsync(IDomainCommand command, System.Threading.CancellationToken ct)
        {
            System.Diagnostics.Contracts.Contract.Requires<ArgumentNullException>((command as UpdateItemCommand) != null);

            var cmd = command as UpdateItemCommand;

            var session = await this.sessionRepository.RetrieveShopperSessionAsync(cmd.ShopperId, ct);
                       
            try
            {
                var cart = session.GetCart();                

                cart.Items.Single(ci => ci.ProductId == cmd.ProductId).Quantity = cmd.Quantity; // TODO Handle prossible Single() error. The session needs to exist as well as the cart item

                session.UpdateCart(cart);
            }
            catch (NullReferenceException) // session not found
            {
                throw;
            }           
            catch (InvalidOperationException) // cart item not found
            {                
                throw;
            }                                   

            await this.sessionRepository.SaveShopperSessionAsync(session, ct);
                        
            // TODO Log suspicious behavior
        }
    }

    public class RemoveItemCommandHandler : IDomainCommandHandler<RemoveItemCommand>
    {
        readonly IShoppingSessionRepository sessionRepository;

        public RemoveItemCommandHandler(IShoppingSessionRepository sessionRepository)
        {
            this.sessionRepository = sessionRepository;
        }

        public async Task HandleAsync(IDomainCommand command, System.Threading.CancellationToken ct)
        {
            System.Diagnostics.Contracts.Contract.Requires<ArgumentNullException>((command as RemoveItemCommand) != null);

            var cmd = command as RemoveItemCommand;

            var session = await this.sessionRepository.RetrieveShopperSessionAsync(cmd.ShopperId, ct);

            try
            {
                var cart = session.GetCart();

                if (cart.Items.RemoveWhere(ci => ci.ProductId == cmd.ProductId) > 0)
                {
                    session.UpdateCart(cart);

                    await this.sessionRepository.SaveShopperSessionAsync(session, ct);
                }

                //session.UpdateCart(cart);                
            }
            catch (NullReferenceException) // session not found
            {
                throw;
            }                                  

            // TODO Log suspicious behavior e.g. cart item not found 
        }
    }
}
