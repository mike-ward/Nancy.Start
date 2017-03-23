using Nancy;
using Nancy.ModelBinding;
using Nancy.ViewEngines;
using TLA.Models;
using TLA.Models.Authentication;

namespace TLA.Controllers.Authentication
{
    public class FormsAuthenticationModule : NancyModule
    {
        public FormsAuthenticationModule(
            IUserMapper userMapper,
            IConfiguration configuration,
            IUserRepository userRepository,
            IViewRenderer viewRenderer,
            IModuleStaticWrappers moduleStaticWrappers)
        {
            Get[AuthenticationRedirectUrl.Url] = p => View[AuthenticationRedirectUrl.Url];

            Post[AuthenticationRedirectUrl.Url] = p => userMapper.Authenticate(
                this,
                userMapper,
                configuration,
                userRepository,
                this.Bind<UserCredentials>(),
                viewRenderer,
                moduleStaticWrappers);
        }
    }
}