using System;
using System.Configuration;

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

        private string GetData()
        {
            var appSettings = string.Empty;

            foreach (var key in  ConfigurationManager.AppSettings.AllKeys)
                appSettings += $"{key,-25}{ConfigurationManager.AppSettings[key]}{Environment.NewLine}";

            return appSettings;
        }
    }
}