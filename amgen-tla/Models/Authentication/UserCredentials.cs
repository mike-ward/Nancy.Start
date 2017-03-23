using System.Runtime.Serialization;

namespace App.Models.Authentication
{
    [DataContract]
    public class UserCredentials
    {
        [DataMember(Name = "user")]
        public string User { get; set; }

        [DataMember(Name = "password")]
        public string Password { get; set; }
    }
}