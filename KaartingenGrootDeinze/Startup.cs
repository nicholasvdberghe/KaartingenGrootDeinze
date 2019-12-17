using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KaartingenGrootDeinze.Startup))]
namespace KaartingenGrootDeinze
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
