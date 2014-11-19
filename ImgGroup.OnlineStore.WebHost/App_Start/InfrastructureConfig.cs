using ImgGroup.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImgGroup.OnlineStore.WebHost
{
    public class InfrastructureConfig
    {        
        public static void Bootstrap(IEntityFrameworkBootstrapper bootstrapper)
        {
            bootstrapper.Bootstrap();
        }
    }
}