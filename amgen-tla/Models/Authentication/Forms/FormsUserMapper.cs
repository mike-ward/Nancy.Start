using System;
using System.Linq;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.ViewEngines;

namespace TLA.Models.Authentication.Forms
{
    public class FormsUserMapper : UserMapper
    {
        public override Response Authenticate(
            INancyModule nancyModule,
            IUserMapper userMapper,
            IUserRepository userRepository,
            UserCredentials userCredentials,
            IViewRenderer viewRenderer)
        {
            var validUser = userRepository
                .GetAllUsers()
                .FirstOrDefault(user =>
                    user.UserName == userCredentials.User &&
                    user.Password == UserIdentity.EncryptPassword(userCredentials.Password, user.Id));

            if (validUser == null)
            {
                nancyModule.Context.ViewBag.AuthenticationError = "Invalid user name or password";
                return viewRenderer.RenderView(nancyModule.Context, "account/login");
            }

            var guid = userMapper.AddUser(userCredentials.User, validUser.FirstName, validUser.LastName, new string[0]);
            validUser.LastLogin = DateTime.UtcNow;
            userRepository.UpdateUser(validUser);
            return nancyModule.LoginAndRedirect(guid, null, "~/home");
        }
    }
}