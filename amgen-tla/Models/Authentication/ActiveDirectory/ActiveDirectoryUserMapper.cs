using System;
using System.DirectoryServices;
using System.Linq;
using System.Security.Authentication;
using System.Security.Principal;
using System.Web;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.ViewEngines;
using TLA.Models.Extensions;

namespace TLA.Models.Authentication.ActiveDirectory
{
    public class ActiveDirectoryUserMapper : UserMapper
    {
        public const string UserGroups = "activeDirectoryUserGroups";
        public const string AdminGroups = "activeDirectoryAdminGroups";

        public override Response Authenticate(
            INancyModule nancyModule,
            IUserMapper userMapper,
            UserCredentials userCredentialsNotUsed,
            IViewRenderer viewRendererNotUsed)
        {
            try
            {
                var userAndClaims = AuthenticateAndAuthorizeUser(HttpContext.Current.ApplicationInstance.User);
                var names = userAndClaims.Item1.Split(',').Reverse().ToArray();
                var userName = string.Join(" ", names);
                var guid = userMapper.AddUser(userName, names[0] ?? "", names[1] ?? "", userAndClaims.Item2);
                return nancyModule.LoginAndRedirect(guid, null);
            }
            catch (Exception ex)
            {
                ex.Log();
                return $"Login from Active Directory Failed<br><br>{ex.Message}<br><br>{ex.InnerException?.Message ?? ""}";
            }
        }

        private static Tuple<string, string[]> AuthenticateAndAuthorizeUser(IPrincipal user)
        {
            var userRoles = Configuration.ActiveDirectoryUserGroups();
            var adminRoles = Configuration.ActiveDirectoryUserGroups();

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