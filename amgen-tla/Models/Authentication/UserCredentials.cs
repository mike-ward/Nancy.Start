using System.Runtime.Serialization;

namespace TLA.Models.Authentication
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