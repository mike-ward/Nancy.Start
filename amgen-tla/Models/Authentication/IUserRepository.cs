using System;
using System.Collections.Generic;
using Nancy.Security;

namespace TLA.Models.Authentication
{
    public interface IUserRepository
    {
        IUserIdentity User(Guid id);
        IUserIdentity User(string username);
        IEnumerable<IUserIdentity> GetAllUsers();
        IEnumerable<IUserIdentity> GetAdminUsers();
    }
}