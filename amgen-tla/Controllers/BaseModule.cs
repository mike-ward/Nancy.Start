﻿using Nancy;
using Nancy.Authentication.Forms;
using TLA.Models.Authentication;

namespace TLA.Controllers
{
    public class BaseModule : NancyModule
    {
        public BaseModule()
            : this(string.Empty)
        {
        }

        public BaseModule(string modulePath)
            : base(modulePath)
        {
            Before += ctx =>
            {
                ViewBag.AuthenticationUrl = AuthenticationRedirectUrl.Url;
                return null;
            };

            Get["/logout"] = p => this.LogoutAndRedirect("~/");
        }
    }
}