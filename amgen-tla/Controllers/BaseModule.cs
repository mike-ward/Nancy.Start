using Nancy;
using Nancy.Authentication.Forms;
using TLA.Models.Authentication;

namespace TLA.Controllers
{
    public class BaseModule : NancyModule
    {
        public BaseModule(IAuthenticationRedirectUrl authenticationRedirectUrl)
            : this(string.Empty, authenticationRedirectUrl)
        {
        }

        public BaseModule(string modulePath, IAuthenticationRedirectUrl authenticationRedirectUrl)
            : base(modulePath)
        {
            Before += ctx =>
            {
                ViewBag.AuthenticationUrl = authenticationRedirectUrl.GetUrl;
                return null;
            };

            Get["/logout"] = p => this.LogoutAndRedirect("~/");
        }
    }
}