using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using Newtonsoft.Json;

namespace TLA.Models.Authentication.Forms
{
    public class FormsUserRepository : IUserRepository
    {
        private static readonly object LockObj = new object();

        public UserIdentity User(Guid id)
        {
            var users = ReadUsers();
            return users.SingleOrDefault(user => user.Id == id);
        }

        public UserIdentity User(string username)
        {
            var users = ReadUsers();
            return users.SingleOrDefault(user => user.UserName.Equals(username, StringComparison.CurrentCultureIgnoreCase));
        }

        public IEnumerable<UserIdentity> GetAllUsers()
        {
            return ReadUsers();
        }

        public IEnumerable<UserIdentity> GetAdminUsers()
        {
            return ReadUsers().Where(user => user.Claims.Contains("admin")).ToList();
        }

        private static List<UserIdentity> ReadUsers()
        {
            try
            {
                if (!Monitor.TryEnter(LockObj, 2000))
                {
                    return new List<UserIdentity>();
                }

                var path = GetFilePath();

                return !File.Exists(path)
                    ? new List<UserIdentity> {new UserIdentity("admin@admin.com", "admin", new[] {"admin"}, "my", "admin")}
                    : JsonConvert.DeserializeObject<List<UserIdentity>>(File.ReadAllText(path));
            }
            finally
            {
                Monitor.Exit(LockObj);
            }
        }

        private static void WriteUsers(IEnumerable<UserIdentity> users)
        {
            try
            {
                if (!Monitor.TryEnter(LockObj, 2000))
                {
                    throw new Exception("User repository is locked, cannot update users");
                }

                var path = GetFilePath();

                var name = Path.GetFileNameWithoutExtension(path);
                var dir = Path.GetDirectoryName(path);
                if (dir == null) throw new Exception("Cannot get repository directory");
                var backup = Path.Combine(dir, name + ".bak");

                // Remove Backup
                if (File.Exists(backup)) File.Delete(backup);

                // Rename existing file
                if (File.Exists(path)) File.Move(path, backup);

                var orderedUsers = users.OrderBy(user => user.Id).ToList();
                File.WriteAllText(path, JsonConvert.SerializeObject(orderedUsers, Formatting.Indented));
            }
            finally
            {
                Monitor.Exit(LockObj);
            }
        }

        private static string GetFilePath()
        {
            // If no config setting, return default
            var cfg = Configuration.UserRepositoryPath() ?? "Users.dat";

            var path = Path.IsPathRooted(cfg)
                ? cfg
                : HttpContext.Current.Server.MapPath("~/App_Data/" + cfg);

            return path;
        }

        public void AddUser(UserIdentity userIdentity)
        {
            Require.ArgumentNotNull(userIdentity, nameof(userIdentity));
            var users = ReadUsers();
            users.Add(userIdentity);
            WriteUsers(users);
        }

        public void DeleteUser(UserIdentity userIdentity)
        {
            Require.ArgumentNotNull(userIdentity, nameof(userIdentity));
            var users = ReadUsers();
            var updatedUsers = users.Where(user => user.Id != userIdentity.Id).ToList();
            WriteUsers(updatedUsers);
        }

        public void UpdateUser(UserIdentity userIdentity)
        {
            Require.ArgumentNotNull(userIdentity, nameof(userIdentity));
            var users = ReadUsers();
            var updatedUsers = users
                .Where(user => user.Id != userIdentity.Id)
                .Concat(new[] {userIdentity})
                .ToList();
            WriteUsers(updatedUsers);
        }
    }
}