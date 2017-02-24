using Nancy.ModelBinding;
using Nancy.ViewEngines;
using TLA.Models;
using TLA.Models.Authentication;

namespace TLA.Controllers.Authentication
{
    public class FormsAuthenticationModule : BaseModule
    {
        public const string  AccountUserLogin = "account/user/login";
        public const string  AccountUserAdd = "account/user/add";
        public const string  AccountUserUpdate = "account/user/update";
        public const string  AccountUserDelete = "account/user/delete";

        public FormsAuthenticationModule(
            IUserMapper userMapper, 
            IConfiguration configuration, 
            IUserRepository userRepository, 
            IViewRenderer viewRenderer, 
            IModuleStaticWrappers moduleStaticWrappers)
        {
            Get[AuthenticationRedirectUrl.Url] = p => View[AccountUserLogin];
            Post[AuthenticationRedirectUrl.Url] = p => userMapper.Authenticate(
                this, 
                userMapper, 
                configuration, 
                userRepository, 
                this.Bind<UserCredentials>(), 
                viewRenderer, 
                moduleStaticWrappers);

            Get[AccountUserAdd] = p => View[AccountUserAdd];
            Post[AccountUserAdd] = p => "";

            Get[AccountUserUpdate] = p => View[AccountUserUpdate];
            Post[AccountUserUpdate] = p => "";

            Get[AccountUserDelete] = p => View[AccountUserDelete];
            Post[AccountUserDelete] = p => "";
        }
    }
}