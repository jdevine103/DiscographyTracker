using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DiscographyTracker.WebMVC.Startup))]
namespace DiscographyTracker.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
