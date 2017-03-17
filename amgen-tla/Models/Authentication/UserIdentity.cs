using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using Nancy.Security;

namespace TLA.Models.Authentication
{
    [DataContract]
    public class UserIdentity : IUserIdentity
    {
        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "userName")]
        public string UserName { get; set; }

        [DataMember(Name = "password")]
        public string Password { get; set; }

        [DataMember(Name = "claims")]
        public IEnumerable<string> Claims { get; set; }

        [DataMember(Name = "firstName")]
        public string FirstName { get; set; }

        [DataMember(Name = "lastName")]
        public string LastName { get; set; }

        [DataMember(Name = "createdOn")]
        public DateTime CreatedOn { get; set; }

        [DataMember(Name = "lastModified")]
        public DateTime LastModified { get; set; }

        [DataMember(Name = "lastPasswordChange")]
        public DateTime LastPasswordChange { get; set; }

        [DataMember(Name = "lastLogin")]
        public DateTime LastLogin { get; set; }

        public UserIdentity()
        {
            Id = Guid.NewGuid();
            var now = DateTime.UtcNow;
            CreatedOn = now;
            LastModified = now;
            LastPasswordChange = now;
            LastLogin = DateTime.MinValue;
        }

        public UserIdentity(string userName, string password, IEnumerable<string> claims, string firstName, string lastName)
            : this()
        {
            Require.ArgumentNotNullEmpty(userName, nameof(userName));
            Require.ArgumentNotNullEmpty(password, nameof(password));
            Require.ArgumentNotNull(claims, nameof(claims));
            Require.ArgumentNotNullEmpty(firstName, nameof(firstName));
            Require.ArgumentNotNullEmpty(lastName, nameof(lastName));

            UserName = userName;
            Password = Encryption.ComputeHash(password, Id);
            Claims = claims.ToArray();
            FirstName = firstName;
            LastName = lastName;
        }
    }
}