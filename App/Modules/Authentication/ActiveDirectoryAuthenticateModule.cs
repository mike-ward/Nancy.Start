﻿using App.Models.Authentication;
using App.Models.Utilities;
using Nancy;

namespace App.Controllers.Authentication
{
    public class ActiveDirectoryAuthenticateModule : NancyModule
    {
        public ActiveDirectoryAuthenticateModule(IUserMapper userMapper, IConfiguration configuration, IModuleStaticWrappers moduleStaticWrappers)
        {
            Get[AuthenticationRedirectUrl.Url] = p => userMapper.Authenticate(this, userMapper, configuration, null, null, null, moduleStaticWrappers);
        }
    }
}