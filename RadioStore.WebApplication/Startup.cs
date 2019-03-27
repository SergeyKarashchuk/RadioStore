using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RadioStore.WebApplication.Startup))]
namespace RadioStore.WebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
