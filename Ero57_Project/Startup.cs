using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ero57_Project.Startup))]
namespace Ero57_Project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
