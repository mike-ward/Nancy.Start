using System.Collections.Generic;
using Nancy.Security;

namespace TLA.Models.Authentication
{
    public class UserIdentity : IUserIdentity
    {
        public string UserName { get; set; }
        public IEnumerable<string> Claims { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}