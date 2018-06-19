using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Lab_9.Startup))]
namespace Lab_9
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
