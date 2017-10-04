using Nancy;
using Nancy.Security;

namespace App.Controllers
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