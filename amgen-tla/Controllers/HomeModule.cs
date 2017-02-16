using Nancy.Security;
using TLA.Models.Authentication;

namespace TLA.Controllers
{
    public class HomeModule : BaseModule
    {
        public HomeModule(IAuthenticationRedirectUrl authenticationRedirectUrl)
            : base(authenticationRedirectUrl)
        {
            this.RequiresAuthentication();

            Get["/home"] = p => View["home"];
        }
    }
}