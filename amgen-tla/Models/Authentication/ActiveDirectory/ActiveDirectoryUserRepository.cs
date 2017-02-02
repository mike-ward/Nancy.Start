using System;
using System.Collections.Generic;
using Nancy.Security;

namespace TLA.Models.Authentication.ActiveDirectory
{
    public class ActiveDirectoryUserRepository : IUserRepository
    {
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
    }
}