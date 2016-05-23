using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KRAM1.Startup))]
namespace KRAM1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
