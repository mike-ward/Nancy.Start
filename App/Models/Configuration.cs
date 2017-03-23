using System.Configuration;
using App.Models.Authentication.ActiveDirectory;

namespace App.Models
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