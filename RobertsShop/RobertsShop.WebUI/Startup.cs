using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RobertsShop.WebUI.Startup))]
namespace RobertsShop.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
