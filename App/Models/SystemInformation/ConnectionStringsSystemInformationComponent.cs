using System;
using System.Configuration;
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

        private string GetData()
        {
            var connectionStrings = string.Empty;

            foreach (ConnectionStringSettings cs in  ConfigurationManager.ConnectionStrings)
                connectionStrings += $"{cs.Name,-25}{cs.ConnectionString}{Environment.NewLine}";

            return HttpUtility.HtmlEncode(connectionStrings);
        }
    }
}