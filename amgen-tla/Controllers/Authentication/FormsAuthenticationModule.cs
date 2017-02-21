using Nancy.ModelBinding;
using Nancy.ViewEngines;
using TLA.Models;
using TLA.Models.Authentication;

namespace TLA.Controllers.Authentication
{
    public class FormsAuthenticationModule : BaseModule
    {
        public FormsAuthenticationModule(IUserMapper userMapper, IConfiguration configuration, IUserRepository userRepository, IViewRenderer viewRenderer)
        {
            Get[AuthenticationRedirectUrl.Url] = p => View["account/login"];
            Post[AuthenticationRedirectUrl.Url] = p => userMapper.Authenticate(
                this, userMapper, configuration, userRepository, this.Bind<UserCredentials>(), viewRenderer);
        }
    }
}