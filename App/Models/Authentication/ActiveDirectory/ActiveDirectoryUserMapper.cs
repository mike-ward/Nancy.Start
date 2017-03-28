using System;
using System.DirectoryServices;
using System.Linq;
using System.Security.Authentication;
using System.Security.Principal;
using System.Web;
using App.Models.Extensions;
using App.Models.Utilities;
using Nancy;
using Nancy.ViewEngines;

namespace App.Models.Authentication.ActiveDirectory
{
    public class ActiveDirectoryUserMapper : UserMapper
    {
        public const string UserGroups = "activeDirectoryUserGroups";
        public const string AdminGroups = "activeDirectoryAdminGroups";

        public override Response Authenticate(
            INancyModule nancyModule,
            IUserMapper userMapper,
            IConfiguration configuration,
            IUserRepository userRepositoryNotUsed,
            UserCredentials userCredentialsNotUsed,
            IViewRenderer viewRendererNotUsed,
            IModuleStaticWrappers moduleStaticWrappers)
        {
            try
            {
                var userAndClaims = AuthenticateAndAuthorizeUser(HttpContext.Current.ApplicationInstance.User, configuration);
                var names = userAndClaims.Item1.Split(',').Reverse().ToArray();
                var userName = string.Join(" ", names);
                var guid = userMapper.AddUser(userName, names[0] ?? "", names[1] ?? "", userAndClaims.Item2);
                return moduleStaticWrappers.LoginAndRedirect(nancyModule, guid, null, ModuleStaticWrappers.DefaultFallbackRedirectUrl);
            }
            catch (Exception ex)
            {
                ex.Log();
                return $"Login from Active Directory Failed<br><br>{ex.Message}<br><br>{ex.InnerException?.Message ?? ""}";
            }
        }

        private static Tuple<string, string[]> AuthenticateAndAuthorizeUser(IPrincipal user, IConfiguration configuration)
        {
            var userRoles = configuration.ActiveDirectoryUserGroups();
            var adminRoles = configuration.ActiveDirectoryUserGroups();

            var claims = new string[0];
            if (adminRoles.Any(user.IsInRole))
            {
                claims = new[] {"admin"};
            }
            else if (userRoles.Any(user.IsInRole) == false)
            {
                throw new AuthenticationException(
                    $"Could not find valid role for {user.Identity.Name}\n\n" +
                    $"{string.Join("<br>", adminRoles.Concat(userRoles))}");
            }
            var name = user.Identity.Name.Replace('\\', '/');
            var activeDirectoryEntry = new DirectoryEntry("WinNT://" + name);
            var fullName = activeDirectoryEntry.Properties["FullName"].Value.ToString();
            return new Tuple<string, string[]>(fullName, claims);
        }
    }
}