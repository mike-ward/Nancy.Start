using System;
using App.Models.Utilities;
using Nancy;
using Nancy.ViewEngines;

namespace App.Models.Authentication.Forms
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
            var validUser = userRepository.Authenticate(userCredentials.User, userCredentials.Password);

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