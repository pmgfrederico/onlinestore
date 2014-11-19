using ImgGroup.Common.Infrastructure;
using ImgGroup.OnlineStore.Infrastructure;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ImgGroup.OnlineStore.WebHost
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {                                    
            InfrastructureConfig.Bootstrap(UnityConfig.GetConfiguredContainer().Resolve<IEntityFrameworkBootstrapper>());

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
