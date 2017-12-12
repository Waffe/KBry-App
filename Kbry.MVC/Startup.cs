using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Kbry.MVC.Startup))]
namespace Kbry.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
