using System;
using System.Collections.Generic;
using App.Models.Utilities;
using Nancy;
using Nancy.ViewEngines;

namespace App.Models.Authentication
{
    public interface IUserMapper : Nancy.Authentication.Forms.IUserMapper
    {
        Guid AddUser(string userName, string firstName, string lastName, IEnumerable<string> claims);

        Response Authenticate(
            INancyModule nancyModule, 
            IUserMapper userMapper, 
            IConfiguration configuration,
            IUserRepository userRepository, 
            UserCredentials userCredentials, 
            IViewRenderer viewRenderer,
            IModuleStaticWrappers moduleStaticWrappers);
    }
}