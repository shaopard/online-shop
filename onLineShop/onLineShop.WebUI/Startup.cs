using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(onLineShop.WebUI.Startup))]
namespace onLineShop.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
