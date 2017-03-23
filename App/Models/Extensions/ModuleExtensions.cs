using App.Controllers.Account;
using Nancy;
using Nancy.Security;

namespace App.Models.Extensions
{
    public static class ModuleExtensions
    {
        public static void RequiresAdminClaim(this INancyModule module)
        {
            module.RequiresClaims(AdminModule.AdminClaim);
        }
    }
}