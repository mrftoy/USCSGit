//using Microsoft.Owin;
//using Owin;

//[assembly: OwinStartupAttribute(typeof(USCS.Startup))]
//namespace USCS
//{
//    public partial class Startup
//    {
//        public void Configuration(IAppBuilder app)
//        {
//            ConfigureAuth(app);
//        }
//    }
//}
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(USCS.Startup))]
namespace USCS
{

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }

}