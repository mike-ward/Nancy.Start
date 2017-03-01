using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using TLA.Models.Authentication;

namespace TLA.Controllers.Account
{
    public class AdminModule : NancyModule
    {
        public const string AdminClaim = "admin";
        public const string AdminDashboardUrl = "account/admin/dashboard";
        public const string AccountUserAdd = "account/admin/add";
        public const string AccountUserUpdate = "account/admin/update";
        public const string AccountUserDelete = "account/admin/delete";

        public AdminModule(IUserRepository userRepository)
        {
            this.RequiresAuthentication();
            this.RequiresClaims(AdminClaim);

            Get[AccountUserAdd] = p => View[AccountUserAdd];
            Post[AccountUserAdd] = p => Models.Account.AdminModel.AddUser(this.Bind<UserIdentity>(), userRepository);

            Get[AccountUserUpdate] = p => View[AccountUserUpdate];
            Post[AccountUserUpdate] = p => Models.Account.AdminModel.UpdateUser(this.Bind<UserIdentity>(), userRepository);

            Get[AccountUserDelete] = p => View[AccountUserDelete];
            Post[AccountUserDelete] = p => Models.Account.AdminModel.DeleteUser(this.Bind<UserIdentity>(), userRepository);
        }
    }
}