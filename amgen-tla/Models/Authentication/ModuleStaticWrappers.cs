using System;
using Nancy;
using Nancy.Authentication.Forms;

namespace TLA.Models.Authentication
{
    public interface IModuleStaticWrappers
    {
        Response LoginAndRedirect(INancyModule module, Guid guid, DateTime? cookieExpiry, string fallbackRedirectUrl);
    }

    public class ModuleStaticWrappers : IModuleStaticWrappers
    {
        public Response LoginAndRedirect(INancyModule module, Guid guid, DateTime? cookieExpiry, string fallbackRedirectUrl)
        {
            return module.LoginAndRedirect(guid, cookieExpiry, fallbackRedirectUrl);
        }
    }
}