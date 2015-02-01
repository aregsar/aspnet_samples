using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TeacherReviews.Startup))]
namespace TeacherReviews
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
