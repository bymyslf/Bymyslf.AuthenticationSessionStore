using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bymyslf.AuthenticationSessionStore.Startup))]

namespace Bymyslf.AuthenticationSessionStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}