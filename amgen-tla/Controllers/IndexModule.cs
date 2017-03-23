using App.Models.Authentication;
using Nancy;

namespace App.Controllers
{
    public class IndexModule : NancyModule
    {
        public IndexModule(IModuleStaticWrappers moduleStaticWrappers)
        {
            Get["/"] = parameters => View["welcome"];
            Get["/logout"] = p => moduleStaticWrappers.LogoutAndRedirect(this, "~/");
        }
    }
}