using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using App.Controllers.Account;
using App.Models.System;
using Newtonsoft.Json;

namespace App.Models.Authentication.Forms
{
    public class FormsUserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IFile _file;
        private readonly IPath _path;

        public FormsUserRepository(IPath path, IFile file, IConfiguration configuration)
        {
            _path = path;
            _file = file;
            _configuration = configuration;
        }

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
            return ReadUsers()
                .Select(user =>
                {
                    user.Password = "";
                    return user;
                });
        }

        public IEnumerable<UserIdentity> GetAdminUsers()
        {
            return ReadUsers().Where(user => user.Claims.Contains("admin")).ToList();
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

        public UserIdentity Authenticate(string username, string password)
        {
            Require.ArgumentNotNullEmpty(username, nameof(username));
            Require.ArgumentNotNullEmpty(password, nameof(password));

            var validUser = ReadUsers()
                .SingleOrDefault(user =>
                    user.UserName == username &&
                    user.Password == Encryption.ComputeHash(password, user.Id));

            return validUser;
        }

        private List<UserIdentity> ReadUsers()
        {
            var path = GetFilePath();

            if (!_file.Exists(path))
            {
                var defaultUser = new List<UserIdentity> {new UserIdentity("admin@admin.com", "admin", new[] {AdminModule.AdminClaim}, "you", "da admin")};
                WriteUsers(defaultUser);
            }

            return JsonConvert.DeserializeObject<List<UserIdentity>>(_file.ReadAllText(path));
        }

        private void WriteUsers(IEnumerable<UserIdentity> users)
        {
            var path = GetFilePath();

            var name = _path.GetFileNameWithoutExtension(path);
            var dir = _path.GetDirectoryName(path);
            if (dir == null) throw new Exception("Cannot get repository directory");
            var backup = _path.Combine(dir, name + ".bak");

            // Remove Backup
            if (_file.Exists(backup)) _file.Delete(backup);

            // Rename existing file
            if (_file.Exists(path)) _file.Move(path, backup);

            var orderedUsers = users.OrderBy(user => user.Id).ToList();
            _file.WriteAllText(path, JsonConvert.SerializeObject(orderedUsers, Formatting.Indented));
        }

        private string GetFilePath()
        {
            // If no config setting, return default
            var cfg = _configuration.UserRepositoryPath() ?? "Users.dat";

            var path = _path.IsPathRooted(cfg)
                ? cfg
                : HttpContext.Current.Server.MapPath("~/App_Data/" + cfg);

            return path;
        }
    }
}