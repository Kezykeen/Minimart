using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MiniMart.Startup))]
namespace MiniMart
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
