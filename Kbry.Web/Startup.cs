using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Kbry.Web.Startup))]
namespace Kbry.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
