using System;
using System.IO;
using System.Linq;
using System.Text;
using App.Models.Extensions;
using App.Models.SystemInformation;
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

            Get["download-system-information"] = p =>
            {
                var text = ((ISystemInformationComponent[]) ViewBag.SystemInformationComponents)
                    .Select(sic => $"\t\t{sic.Title().StripHtml().ToUpperInvariant()}\r\n{sic.Html().StripHtml()}\r\n\r\n")
                    .Aggregate($"\t\tSYSTEM INFORMTION\r\n\t\t{DateTime.UtcNow:u}", (a, s) => a + s);

                return Response.AsAttachment(new MemoryStream(Encoding.UTF8.GetBytes(text)), "system-information.txt");
            };
        }
    }
}