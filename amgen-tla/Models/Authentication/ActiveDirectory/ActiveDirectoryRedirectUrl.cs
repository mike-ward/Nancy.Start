namespace TLA.Models.Authentication.ActiveDirectory
{
    public class ActiveDirectoryRedirectUrl : IAuthenticationReirectUrl
    {
        public const string Url = "/account/active-directory-authenticate";
        public string GetUrl => $"~{Url}";
    }
}