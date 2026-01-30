using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ferre2.Startup))]
namespace ferre2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
