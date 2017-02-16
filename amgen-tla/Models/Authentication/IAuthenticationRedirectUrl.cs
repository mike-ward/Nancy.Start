namespace TLA.Models.Authentication
{
    public interface IAuthenticationRedirectUrl
    {
        string GetUrl { get; }
    }
}