using Nancy;
using TLA.Models.Extensions;

namespace TLA.Controllers
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