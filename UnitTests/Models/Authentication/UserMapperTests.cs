using System;
using App.Models.Authentication;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.Models.Authentication
{
    [TestClass]
    public class UserMapperTests
    {
        [TestMethod]
        public void AddUserShouldReturnGuid()
        {
            var userMapper = new UserMapper();
            userMapper.AddUser("user", "first", "last", new string[0]).Should().NotBeEmpty();
        }

        [TestMethod]
        public void AddedUserShouldBeRetrievable()
        {
            var userMapper = new UserMapper();
            var guid = userMapper.AddUser("user", "first", "last", new string[0]);
            var user = userMapper.GetUserFromIdentifier(guid, null);
            user.UserName.Should().Be("user");
            user.Claims.Should().BeEmpty();
        }

        [TestMethod]
        public void AuthenticateShouldNotBeImplemented()
        {
            var userMapper = new UserMapper();
            Action action = () => userMapper.Authenticate(null, null, null, null, null, null, null);
            action.Should().Throw<NotImplementedException>();
        }
    }
}