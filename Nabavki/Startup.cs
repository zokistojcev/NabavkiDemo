using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Nabavki.Startup))]
namespace Nabavki
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
