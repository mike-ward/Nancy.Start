
using TLA.Models.Authentication;

namespace TLA.Controllers
{
    public class WelcomeModule : BaseModule
    {
        public WelcomeModule(IAuthenticationRedirectUrl authenticationRedirectUrl)
            : base(authenticationRedirectUrl)
        {
            Get["/"] = parameters => View["welcome"];
        }
    }
}