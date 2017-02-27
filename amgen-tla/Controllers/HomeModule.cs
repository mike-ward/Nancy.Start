using Nancy;
using Nancy.Security;

namespace TLA.Controllers
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            this.RequiresAuthentication();

            Get["/home"] = p => View["home"];
        }
    }
}