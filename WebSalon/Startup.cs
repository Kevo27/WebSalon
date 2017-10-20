using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebSalon.Startup))]
namespace WebSalon
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
