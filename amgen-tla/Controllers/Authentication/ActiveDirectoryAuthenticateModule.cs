using TLA.Models;
using TLA.Models.Authentication;

namespace TLA.Controllers.Authentication
{
    public class ActiveDirectoryAuthenticateModule : BaseModule
    {
        public ActiveDirectoryAuthenticateModule(IUserMapper userMapper, IConfiguration configuration)
        {
            Get[AuthenticationRedirectUrl.Url] = p => userMapper.Authenticate(this, userMapper, configuration, null, null, null);
        }
    }
}