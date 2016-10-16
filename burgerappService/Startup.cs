using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(burgerappService.Startup))]

namespace burgerappService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}