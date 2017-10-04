using App.Models.Account.Admin;
using App.Models.Authentication;
using App.Models.Extensions;
using Nancy;
using Nancy.ModelBinding;
using Nancy.ViewEngines;

namespace App.Controllers.Account.Admin
{
    public class AdminModule : NancyModule
    {
        public const string AdminClaim = "admin";
        public const string AdminDashboardRoute = "account/admin/dashboard";
        public const string AdminUserActionRoute = "account/admin/user";

        public const string AdminUserAddRoute = "account/admin/addUser";
        public const string AdminUserUpdateRoute = "account/admin/update";
        public const string AdminUserDeleteRoute = "account/admin/delete";

        public AdminModule(IUserRepository userRepository, IViewRenderer viewRenderer)
        {
            this.RequiresAdminClaim();

            Get["account/admin/allUsers"] = p => Response.AsJson(userRepository.GetAllUsers());

            Get[AdminDashboardRoute] = p => View[AdminDashboardRoute];

            Get[AdminUserAddRoute] = p => View[AdminUserAddRoute];
            Post[AdminUserActionRoute] = p => this.AddUser(this.Bind<UserIdentity>(), userRepository, viewRenderer);

            Get[AdminUserUpdateRoute] = p => View[AdminUserUpdateRoute];
            Put[AdminUserActionRoute] = p => this.UpdateUser(this.Bind<UserIdentity>(), userRepository, View[AdminDashboardRoute], View[AdminUserUpdateRoute]);

            Get[AdminUserDeleteRoute] = p => View[AdminUserDeleteRoute];
            Delete[AdminUserActionRoute] = p => this.DeleteUser(this.Bind<UserIdentity>(), userRepository, View[AdminDashboardRoute], View[AdminUserDeleteRoute]);
        }
    }
}