using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using ImgGroup.Common.Infrastructure;
using ImgGroup.OnlineStore.Infrastructure;
using ImgGroup.Common.Entities;
using ImgGroup.OnlineStore.Contracts;
using System.Collections.Generic;
using ImgGroup.OnlineStore.Infrastructure.Repositories;
using Microsoft.AspNet.Identity;
using ImgGroup.OnlineStore.WebHost.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace ImgGroup.OnlineStore.WebHost
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            
            container.RegisterType<IEntityFrameworkBootstrapper, EntityFrameworkBootstrapper>(new TransientLifetimeManager());
            container.RegisterType<OnlineStoreDbContext>(new PerRequestLifetimeManager());
            container.RegisterType(typeof(IReadOnlyRepository<,>), typeof(GenericReadOnlyRepository<,>), new PerRequestLifetimeManager());            
            container.RegisterType<IShoppingSessionRepository, ShoppingSessionRepository>(new PerRequestLifetimeManager());
            container.RegisterType<IDomainCommandHandler<AddItemCommand>, AddItemCommandHandler>(new PerRequestLifetimeManager());
            container.RegisterType<IDomainCommandHandler<UpdateItemCommand>, UpdateItemCommandHandler>(new PerRequestLifetimeManager());
            container.RegisterType<IDomainCommandHandler<RemoveItemCommand>, RemoveItemCommandHandler>(new PerRequestLifetimeManager());                                    
        }
    }
}
