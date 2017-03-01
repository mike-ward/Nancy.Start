using System;
using Nancy;
using Nancy.Authentication.Forms;

namespace TLA.Models.Authentication
{

    public interface IModuleStaticWrappers
    {
        Response LoginAndRedirect(INancyModule module, Guid guid, DateTime? cookieExpiry, string fallbackRedirectUrl);
        Response LogoutAndRedirect(INancyModule module, string redirectUrl);
    }

    public class ModuleStaticWrappers : IModuleStaticWrappers
    {
        public const string DefaultFallbackRedirectUrl = "~/home";

        public Response LoginAndRedirect(INancyModule module, Guid guid, DateTime? cookieExpiry, string fallbackRedirectUrl)
        {
            return module.LoginAndRedirect(guid, cookieExpiry, fallbackRedirectUrl);
        }

        public Response LogoutAndRedirect(INancyModule module, string redirectUrl)
        {
            return module.LogoutAndRedirect(redirectUrl);
        }
    }
}