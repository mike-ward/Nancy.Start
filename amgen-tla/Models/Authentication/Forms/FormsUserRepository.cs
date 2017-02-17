using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Web;
using Nancy.Security;
using Newtonsoft.Json;

namespace TLA.Models.Authentication.Forms
{
    public class FormsUserRepository : IUserRepository
    {
        private static readonly object LockObj = new object();

        public IUserIdentity User(Guid id)
        {
            throw new NotImplementedException();
        }

        public IUserIdentity User(string username)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IUserIdentity> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IUserIdentity> GetAdminUsers()
        {
            throw new NotImplementedException();
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
                    ? new List<UserIdentity>()
                    : JsonConvert.DeserializeObject<List<UserIdentity>>(File.ReadAllText(path));
            }
            finally
            {
                Monitor.Exit(LockObj);
            }
        }

        private static void WriteUsers(IReadOnlyCollection<UserIdentity> users)
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

                File.WriteAllText(path, JsonConvert.SerializeObject(users, Formatting.Indented));
            }
            finally
            {
                Monitor.Exit(LockObj);
            }
        }

        private static string GetFilePath()
        {
            // If no config setting, return default
            var cfg = ConfigurationManager.AppSettings["UserRepository"];
            if (cfg == null)
            {
                return HttpContext.Current.Server.MapPath("~/App_Data/Users.dat");
            }

            // If config setting is rooted, return as is
            var path = Path.IsPathRooted(cfg)
                ? cfg
                : HttpContext.Current.Server.MapPath("~/App_Data/" + cfg); // Otherwise, root the setting in the default path

            if (!File.Exists(path))
            {

            }

            return path;
        }

        public void AddUser(UserIdentity userIdentity)
        {
            if (userIdentity == null) throw new ArgumentNullException(nameof(userIdentity));
            var users = ReadUsers();
            users.Add(userIdentity);
            WriteUsers(users);
        }
    }
}