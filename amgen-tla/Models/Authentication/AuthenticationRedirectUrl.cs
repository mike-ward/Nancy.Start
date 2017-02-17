namespace TLA.Models.Authentication
{
    public class AuthenticationRedirectUrl : IAuthenticationRedirectUrl
    {
        public const string Url = "login";
        public string GetUrl => Url;
    }
}