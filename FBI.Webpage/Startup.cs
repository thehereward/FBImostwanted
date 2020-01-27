using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FBI.Webpage.Startup))]
namespace FBI.Webpage
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
