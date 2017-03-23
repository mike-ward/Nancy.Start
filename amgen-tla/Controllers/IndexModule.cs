using Nancy;
using TLA.Models.Authentication;

namespace TLA.Controllers
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