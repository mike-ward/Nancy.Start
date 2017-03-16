using System;
using System.Linq;
using Nancy;
using Nancy.ViewEngines;

namespace TLA.Models.Authentication.Forms
{
    public class FormsUserMapper : UserMapper
    {
        public override Response Authenticate(
            INancyModule nancyModule,
            IUserMapper userMapper,
            IConfiguration configuration,
            IUserRepository userRepository,
            UserCredentials userCredentials,
            IViewRenderer viewRenderer,
            IModuleStaticWrappers moduleStaticWrappers)
        {
            var validUser = userRepository
                .GetAllUsers()
                .FirstOrDefault(user =>
                    user.UserName == userCredentials.User &&
                    user.Password == UserIdentity.EncryptPassword(userCredentials.Password, user.Id));

            if (validUser == null)
            {
                nancyModule.Context.ViewBag.AuthenticationError = Constants.AuthenticationError;
                return viewRenderer.RenderView(nancyModule.Context, AuthenticationRedirectUrl.Url);
            }

            var guid = userMapper.AddUser(userCredentials.User, validUser.FirstName, validUser.LastName, validUser.Claims);
            validUser.LastLogin = DateTime.UtcNow;
            userRepository.UpdateUser(validUser);
            return moduleStaticWrappers.LoginAndRedirect(nancyModule, guid, null, ModuleStaticWrappers.DefaultFallbackRedirectUrl);
        }
    }
}