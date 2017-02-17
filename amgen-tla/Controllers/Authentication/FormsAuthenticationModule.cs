using Nancy;
using Nancy.Authentication.Forms;
using Nancy.ModelBinding;
using Nancy.ViewEngines;
using TLA.Models.Authentication;
using TLA.Models.Authentication.Forms;
using IUserMapper = TLA.Models.Authentication.IUserMapper;

namespace TLA.Controllers.Authentication
{
    public class FormsAuthenticationModule : BaseModule
    {
        public FormsAuthenticationModule(IUserMapper userMapper, IViewRenderer viewRenderer)
        {
            Get[FormsRedirectUrl.Url] = p => View["account/login"];
            Post[FormsRedirectUrl.Url] = p => Authenticate(this.Bind<UserCredentials>(), userMapper, viewRenderer);
        }

        private Response Authenticate(
            UserCredentials userCredentials,
            IUserMapper userMapper,
            IViewRenderer viewRenderer)
        {
            // For now accept any non empty strings.
            if (string.IsNullOrWhiteSpace(userCredentials.User) || string.IsNullOrWhiteSpace(userCredentials.Password))
            {
                Context.ViewBag.AuthenticationError = true;
                return viewRenderer.RenderView(Context, "account/login");
            }

            var guid = userMapper.AddUser(userCredentials.User, "first", "last", new string[0]);
            var url = Request.Query["returnUrl"] as string ?? "~/home";
            return this.LoginAndRedirect(guid, null, url);
        }
    }
}