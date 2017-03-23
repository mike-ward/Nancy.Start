using Nancy;
using Nancy.Security;
using TLA.Controllers.Account;

namespace TLA.Models.Extensions
{
    public static class ModuleExtensions
    {
        public static void RequiresAdminClaim(this INancyModule module)
        {
            module.RequiresClaims(AdminModule.AdminClaim);
        }
    }
}