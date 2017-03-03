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
        public const string AdminUserActionUrl = "account/admin/user";

        public AdminModule(IUserRepository userRepository)
        {
            this.RequiresAuthentication();
            this.RequiresClaims(AdminClaim);

            Get[AdminDashboardUrl] = p => View["account/admin/dashboard"];
            Get["account/admin/allUsers"] = p => Response.AsJson(userRepository.GetAllUsers());

            Get["account/admin/add"] = p => View["account/admin/add"];
            Post[AdminUserActionUrl] = p => Models.Account.AdminModel.AddUser(this.Bind<UserIdentity>(), userRepository);

            Get["account/admin/update"] = p => View["account/admin/update"];
            Put[AdminUserActionUrl] = p => Models.Account.AdminModel.UpdateUser(this.Bind<UserIdentity>(), userRepository);

            Get["account/admin/delete"] = p => View["account/admin/delete"];
            Delete[AdminUserActionUrl] = p => Models.Account.AdminModel.DeleteUser(this.Bind<UserIdentity>(), userRepository);
        }
    }
}