using Nancy.Security;

namespace TLA.Controllers
{
    public class HomeModule : BaseModule
    {
        public HomeModule()
        {
            this.RequiresAuthentication();

            Get["/home"] = p => View["home"];
        }
    }
}