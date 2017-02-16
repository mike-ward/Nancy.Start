﻿namespace TLA.Models.Authentication.Forms
{
    public class FormsRedirectUrl : IAuthenticationRedirectUrl
    {
        public const string Url = "account/forms-authenticate";
        public string GetUrl => Url;
    }
}