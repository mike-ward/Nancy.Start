using Nancy;
using TLA.Models.Authentication;

namespace TLA.Controllers
{
    public class WelcomeModule : NancyModule
    {
        public WelcomeModule(IModuleStaticWrappers moduleStaticWrappers)
        {
            Get["/"] = parameters => View["welcome"];
            Get["/logout"] = p => moduleStaticWrappers.LogoutAndRedirect(this, "~/");
        }
    }
}