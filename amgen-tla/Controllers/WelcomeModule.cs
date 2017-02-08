
using TLA.Models.Authentication;

namespace TLA.Controllers
{
    public class WelcomeModule : BaseModule
    {
        public WelcomeModule(IAuthenticationReirectUrl authenticationReirectUrl)
            : base(authenticationReirectUrl)
        {
            Get["/"] = parameters => View["welcome"];
        }
    }
}