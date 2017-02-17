using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Nancy.Security;

namespace TLA.Models.Authentication
{
    public class UserIdentity : IUserIdentity
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public IEnumerable<string> Claims { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime LastModified { get; set; }
        public DateTime LastPasswordChange { get; set; }
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
            Password = EncryptPassword(password, Id);
            Claims = claims.ToArray();
            FirstName = firstName;
            LastName = lastName;
        }

        public static string EncryptPassword(string password, Guid salt)
        {
            Require.ArgumentNotNullEmpty(password, nameof(password));
            if (salt == Guid.Empty) throw new ArgumentException("empty", nameof(salt));

            using (var md5 = MD5.Create())
            {
                var bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
                return string.Join(null, bytes.Select(b => b.ToString("x2")));
            }
        }
    }
}