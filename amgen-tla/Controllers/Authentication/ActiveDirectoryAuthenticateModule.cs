using TLA.Models.Authentication;

namespace TLA.Controllers.Authentication
{
    public class ActiveDirectoryAuthenticateModule : BaseModule
    {
        public ActiveDirectoryAuthenticateModule(IUserMapper userMapper)
        {
            Get[AuthenticationRedirectUrl.Url] = p => userMapper.Authenticate(this, userMapper, null, null);
        }
    }
}