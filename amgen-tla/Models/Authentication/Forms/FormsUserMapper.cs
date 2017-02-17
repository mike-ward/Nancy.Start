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
            UserCredentials userCredentials,
            IViewRenderer viewRenderer)
        {
            // For now accept any non empty strings.
            if (string.IsNullOrWhiteSpace(userCredentials.User) || string.IsNullOrWhiteSpace(userCredentials.Password))
            {
                nancyModule.Context.ViewBag.AuthenticationError = true;
                return viewRenderer.RenderView(nancyModule.Context, "account/login");
            }

            var guid = userMapper.AddUser(userCredentials.User, "first", "last", new string[0]);
            var url =  "~/home";
            return nancyModule.LoginAndRedirect(guid, null, url);
        }
    }
}