using Nancy;
using TLA.Models.Authentication;

namespace TLA.Controllers
{
    public class BaseModule : NancyModule
    {
        public BaseModule(IAuthenticationRedirectUrl authenticationRedirectUrl)
        {
            Before += ctx =>
            {
                ViewBag.AuthenticationUrl = authenticationRedirectUrl.GetUrl;
                return null;
            };
        }
    }
}