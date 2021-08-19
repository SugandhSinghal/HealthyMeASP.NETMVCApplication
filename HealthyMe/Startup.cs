using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HealthyMe.Startup))]
namespace HealthyMe
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
