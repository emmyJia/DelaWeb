using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DelaWeb.Startup))]
namespace DelaWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
