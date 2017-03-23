namespace App.Models.Authentication
{
    public interface IAuthenticationRedirectUrl
    {
        string GetUrl { get; }
    }
}