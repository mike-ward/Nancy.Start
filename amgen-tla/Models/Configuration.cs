using System.Configuration;
using TLA.Controllers.Authentication;

namespace TLA.Models
{
    public static class Configuration
    {
        public static string[] ActiveDirectoryUserGroups()
        {
            var groups = ConfigurationManager.AppSettings[ActiveDirectoryAuthenticateModule.UserGroups];
            return string.IsNullOrWhiteSpace(groups) ? new string[0] : groups.Split('|');
        }

        public static string[] ActiveDirectoryAdminGroups()
        {
            var groups = ConfigurationManager.AppSettings[ActiveDirectoryAuthenticateModule.AdminGroups];
            return string.IsNullOrWhiteSpace(groups) ? new string[0] : groups.Split('|');
        }
    }
}