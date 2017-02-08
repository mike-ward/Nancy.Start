using Nancy;
using TLA.Models.Authentication;

namespace TLA.Controllers
{
    public class BaseModule : NancyModule
    {
        public BaseModule(IAuthenticationReirectUrl authenticationRedirectUrl)
        {
            Before += ctx =>
            {
                ViewBag.AuthenticationUrl = authenticationRedirectUrl.GetUrl;
                return null;
            };
        }
    }
}