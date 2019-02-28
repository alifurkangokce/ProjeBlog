using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Blog.Admin.Startup))]
namespace Blog.Admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
