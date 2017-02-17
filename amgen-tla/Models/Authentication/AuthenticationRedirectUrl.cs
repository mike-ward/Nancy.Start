namespace TLA.Models.Authentication
{
    public class AuthenticationRedirectUrl : IAuthenticationRedirectUrl
    {
        public const string Url = "account/authenticate";
        public string GetUrl => Url;
    }
}