using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ImgGroup.OnlineStore.WebHost.Startup))]
namespace ImgGroup.OnlineStore.WebHost
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
