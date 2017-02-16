namespace TLA.Models.Authentication.ActiveDirectory
{
    public class ActiveDirectoryRedirectUrl : IAuthenticationRedirectUrl
    {
        public const string Url = "account/active-directory-authenticate";
        public string GetUrl => Url;
    }
}