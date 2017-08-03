using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Comp2007_Final.Startup))]
namespace Comp2007_Final
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
