using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnlineServices.Startup))]
namespace OnlineServices
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
