using System;
using System.Linq;
using Nancy;
using Nancy.Responses.Negotiation;
using Nancy.ViewEngines;
using TLA.Controllers.Account;
using TLA.Models.Authentication;

namespace TLA.Models.Account.Admin
{
    public static class AdminModel
    {
        public static Response AddUser(this INancyModule module, UserIdentity userIdentity, IUserRepository userRepository, IViewRenderer viewRenderer)
        {
            try
            {
                var allUsers = userRepository.GetAllUsers();
                if (allUsers.Any(user => string.Equals(user.UserName, userIdentity.UserName, StringComparison.OrdinalIgnoreCase)))
                {
                    module.Context.ViewBag.AuthenticationError = Constants.AdminUserAlreadyExistsError;
                    return viewRenderer.RenderView(module.Context, AdminModule.AdminUserAddRoute);
                }

                userRepository.AddUser(userIdentity);
                module.Context.ViewBag.AuthenticationError = Constants.AdminUserAdded;
                return module.Response.AsRedirect($"~/{AdminModule.AdminDashboardRoute}");
            }
            catch (Exception e)
            {
                module.Context.ViewBag.AuthenticationError = e.Message;
                return viewRenderer.RenderView(module.Context, AdminModule.AdminUserAddRoute);
            }
        }

        public static Negotiator UpdateUser(this INancyModule module, UserIdentity userIdentity, IUserRepository userRepository, Negotiator success, Negotiator error)
        {
            return null;
        }

        public static Negotiator DeleteUser(this INancyModule module, UserIdentity userIdentity, IUserRepository userRepository, Negotiator success, Negotiator error)
        {
            return null;
        }
    }
}