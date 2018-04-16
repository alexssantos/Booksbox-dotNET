using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AT_ASP.Web.Startup))]
namespace AT_ASP.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
