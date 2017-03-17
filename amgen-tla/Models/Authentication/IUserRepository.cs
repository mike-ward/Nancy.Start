using System;
using System.Collections.Generic;

namespace TLA.Models.Authentication
{
    public interface IUserRepository
    {
        UserIdentity User(Guid id);
        UserIdentity User(string username);
        UserIdentity Authenticate(string username, string password);
        IEnumerable<UserIdentity> GetAllUsers();
        IEnumerable<UserIdentity> GetAdminUsers();
        void AddUser(UserIdentity userIdentity);
        void DeleteUser(UserIdentity userIdentity);
        void UpdateUser(UserIdentity userIdentity);
    }
}