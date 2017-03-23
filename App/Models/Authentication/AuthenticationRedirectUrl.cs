namespace App.Models.Authentication
{
    public class AuthenticationRedirectUrl : IAuthenticationRedirectUrl
    {
        public const string Url = "account/user/login";
        public string GetUrl => $"~/{Url}";
    }
}