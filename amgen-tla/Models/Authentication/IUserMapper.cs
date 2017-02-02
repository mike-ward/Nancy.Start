using System;
using System.Collections.Generic;

namespace TLA.Models.Authentication
{
    public interface IUserMapper : Nancy.Authentication.Forms.IUserMapper
    {
        Guid AddUser(string userName, string firstName, string lastName, IEnumerable<string> claims);
    }
}