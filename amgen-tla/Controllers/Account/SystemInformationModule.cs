using App.Models.Extensions;
using Nancy;

namespace App.Controllers.Account
{
    public class SystemInformationModule : NancyModule
    {
        public SystemInformationModule()
            : base("account/admin")
        {
            this.RequiresAdminClaim();

            Get["system-information"] = p => View["system-information"];
        }
    }
}