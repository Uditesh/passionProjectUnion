using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PassionProjUditesh.Startup))]
namespace PassionProjUditesh
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
