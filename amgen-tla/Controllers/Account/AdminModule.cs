using Nancy;
using Nancy.Security;

namespace TLA.Controllers.Account
{
    public class AdminModule : NancyModule
    {
        public const string AdminClaim = "admin";
        public const string AccountUserAdd = "account/user/add";
        public const string AccountUserUpdate = "account/user/update";
        public const string AccountUserDelete = "account/user/delete";

        public AdminModule()
        {
            this.RequiresAuthentication();
            this.RequiresClaims(AdminClaim);

            Get[AccountUserAdd] = p => View[AccountUserAdd];
            Post[AccountUserAdd] = p => "";

            Get[AccountUserUpdate] = p => View[AccountUserUpdate];
            Post[AccountUserUpdate] = p => "";

            Get[AccountUserDelete] = p => View[AccountUserDelete];
            Post[AccountUserDelete] = p => "";
        }
    }
}