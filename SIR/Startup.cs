using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SIR.Startup))]
namespace SIR
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
