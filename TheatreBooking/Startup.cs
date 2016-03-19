using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TheatreBooking.Startup))]
namespace TheatreBooking
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
