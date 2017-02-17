using System.Configuration;
using TLA.Controllers.Authentication;
using TLA.Models.Authentication.ActiveDirectory;

namespace TLA.Models
{
    public static class Configuration
    {
        public static string[] ActiveDirectoryUserGroups()
        {
            var groups = ConfigurationManager.AppSettings[ActiveDirectoryUserMapper.UserGroups];
            return string.IsNullOrWhiteSpace(groups) ? new string[0] : groups.Split('|');
        }

        public static string[] ActiveDirectoryAdminGroups()
        {
            var groups = ConfigurationManager.AppSettings[ActiveDirectoryUserMapper.AdminGroups];
            return string.IsNullOrWhiteSpace(groups) ? new string[0] : groups.Split('|');
        }

        public static string UserRepositoryPath()
        {
            var path = ConfigurationManager.AppSettings["UserRepository"];
            return path;
        }
    }
}