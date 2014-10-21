using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JPS_Project.Startup))]
namespace JPS_Project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
