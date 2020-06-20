using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RealEstator.Startup))]
namespace RealEstator
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
