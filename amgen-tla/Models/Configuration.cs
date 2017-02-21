using System.Configuration;
using TLA.Models.Authentication.ActiveDirectory;

namespace TLA.Models
{
    public class Configuration : IConfiguration
    {
        public string[] ActiveDirectoryUserGroups()
        {
            var groups = ConfigurationManager.AppSettings[ActiveDirectoryUserMapper.UserGroups];
            return string.IsNullOrWhiteSpace(groups) ? new string[0] : groups.Split('|');
        }

        public string[] ActiveDirectoryAdminGroups()
        {
            var groups = ConfigurationManager.AppSettings[ActiveDirectoryUserMapper.AdminGroups];
            return string.IsNullOrWhiteSpace(groups) ? new string[0] : groups.Split('|');
        }

        public string UserRepositoryPath()
        {
            var path = ConfigurationManager.AppSettings["UserRepository"];
            return path;
        }
    }
}