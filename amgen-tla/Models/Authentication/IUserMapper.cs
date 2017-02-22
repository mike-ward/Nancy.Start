using System;
using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines;

namespace TLA.Models.Authentication
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