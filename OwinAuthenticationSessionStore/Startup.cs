using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OwinAuthenticationSessionStore.Startup))]
namespace OwinAuthenticationSessionStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
