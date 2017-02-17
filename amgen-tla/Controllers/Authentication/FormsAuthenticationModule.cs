using Nancy.ModelBinding;
using Nancy.ViewEngines;
using TLA.Models.Authentication;

namespace TLA.Controllers.Authentication
{
    public class FormsAuthenticationModule : BaseModule
    {
        public FormsAuthenticationModule(IUserMapper userMapper, IUserRepository userRepository, IViewRenderer viewRenderer)
        {
            Get[AuthenticationRedirectUrl.Url] = p => View["account/login"];
            Post[AuthenticationRedirectUrl.Url] = p => userMapper.Authenticate(this, userMapper, userRepository, this.Bind<UserCredentials>(), viewRenderer);
        }
    }
}