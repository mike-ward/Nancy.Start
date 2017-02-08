using Nancy.Security;
using TLA.Models.Authentication;

namespace TLA.Controllers
{
    public class HomeModule : BaseModule
    {
        public HomeModule(IAuthenticationReirectUrl authenticationReirectUrl)
            : base(authenticationReirectUrl)
        {
            this.RequiresAuthentication();

            Get["/home"] = p => View["home"];
        }
    }
}