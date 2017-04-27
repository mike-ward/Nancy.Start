using System;
using System.Configuration;
using System.Linq;
using Nancy.Helpers;

namespace App.Models.SystemInformation
{
    public class ConnectionStringsSystemInformationComponent : ISystemInformationComponent
    {
        public int Cardinality => 4;

        public string Title()
        {
            return "Connection Strings";
        }

        public string Html()
        {
            return $"<pre>{GetData()}</pre>";
        }

        private static string GetData()
        {
            var connectionStrings = ConfigurationManager.ConnectionStrings
                .Cast<ConnectionStringSettings>()
                .Aggregate(string.Empty, (current, cs) => current + $"{cs.Name,-25}{cs.ConnectionString}{Environment.NewLine}");

            return HttpUtility.HtmlEncode(connectionStrings);
        }
    }
}