using System;
using System.Collections.Generic;

namespace App.Models.Authentication.ActiveDirectory
{
    public class ActiveDirectoryUserRepository : IUserRepository
    {
        public UserIdentity User(Guid id)
        {
            throw new NotImplementedException();
        }

        public UserIdentity User(string username)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserIdentity> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserIdentity> GetAdminUsers()
        {
            throw new NotImplementedException();
        }

        public void AddUser(UserIdentity userIdentity)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(UserIdentity userIdentity)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(UserIdentity userIdentity)
        {
            throw new NotImplementedException();
        }

        public UserIdentity Authenticate(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}