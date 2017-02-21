using System;
using System.Collections.Generic;
using System.Linq;
using Nancy;
using Nancy.Security;
using Nancy.ViewEngines;

namespace TLA.Models.Authentication
{
    public class UserMapper : IUserMapper
    {
        private static readonly Dictionary<Guid, IUserIdentity> Users = new Dictionary<Guid, IUserIdentity>();

        public IUserIdentity GetUserFromIdentifier(Guid identifier, NancyContext context)
        {
            IUserIdentity user;
            return Users.TryGetValue(identifier, out user) ? user : null;
        }

        public Guid AddUser(string userName, string firstName, string lastName, IEnumerable<string> claims)
        {
            var guid = Guid.NewGuid();

            Users.Add(guid, new UserIdentity
            {
                UserName = userName,
                FirstName = firstName,
                LastName = lastName,
                Claims = claims.ToArray()
            });

            return guid;
        }

        public virtual Response Authenticate(
            INancyModule nancyModule, 
            IUserMapper userMapper,
            IConfiguration configuration,
            IUserRepository userRepository, 
            UserCredentials userCredentials, 
            IViewRenderer viewRenderer)
        {
            throw new NotImplementedException();
        }
    }
}