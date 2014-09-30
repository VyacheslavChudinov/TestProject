using Microsoft.Owin;
using Owin;
using Service;

[assembly: OwinStartup(typeof(Startup))]

namespace Service
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
