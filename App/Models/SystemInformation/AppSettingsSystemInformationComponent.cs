using System;
using System.Configuration;
using System.Linq;

namespace App.Models.SystemInformation
{
    public class AppSettingsSystemInformationComponent : ISystemInformationComponent
    {
        public int Cardinality => 5;

        public string Title()
        {
            return "App Settings";
        }

        public string Html()
        {
            return $"<pre>{GetData()}</pre>";
        }

        private static string GetData()
        {
            return ConfigurationManager.AppSettings
                .AllKeys
                .Aggregate(string.Empty, (current, key) => current + $"{key,-25}{ConfigurationManager.AppSettings[key]}{Environment.NewLine}");
        }
    }
}