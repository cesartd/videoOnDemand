using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VideoOnDemand.Web.Startup))]
namespace VideoOnDemand.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
