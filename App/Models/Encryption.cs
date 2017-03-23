using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace App.Models
{
    public static class Encryption
    {
        public static string ComputeHash(string text, Guid salt)
        {
            Require.ArgumentNotNullEmpty(text, nameof(text));
            Require.ArgumentValid(salt, s => s != Guid.Empty, $"{nameof(salt)} is not a valid GUID");

            using (var md5 = MD5.Create())
            {
                var bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(text + salt));
                return string.Join(null, bytes.Select(b => b.ToString("x2")));
            }
        }
    }
}