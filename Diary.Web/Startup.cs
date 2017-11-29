using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Diary.Web.Startup))]

namespace Diary.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
