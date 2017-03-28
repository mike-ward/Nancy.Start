using System;
using System.Linq;
using App.Models.Authentication;
using App.Models.Authentication.Forms;
using App.Models.System;
using App.Models.Utilities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTests.Models.Authentication.Forms
{
    [TestClass]
    public class FormsUserRepositoryTests
    {
        private Mock<IConfiguration> _configuration;
        private Mock<IFile> _file;
        private Mock<IPath> _path;
        private FormsUserRepository _userRepository;

        [TestInitialize]
        public void Init()
        {
            _path = new Mock<IPath>();
            _path.Setup(p => p.IsPathRooted(It.IsAny<string>())).Returns(true);
            _path.Setup(p => p.GetDirectoryName(It.IsAny<string>())).Returns("/");

            _file = new Mock<IFile>();
            _file.Setup(f => f.Exists(It.IsAny<string>())).Returns(true);
            _file.Setup(f => f.ReadAllText(It.IsAny<string>())).Returns(
                @"[
  {
    ""Id"": ""8e2b3a59-b210-4e91-8a72-8e7179c61e20"",
    ""UserName"": ""admin@admin.com"",
    ""Password"": ""ec205fc189684fe62f687eac68c8bde9"",
    ""Claims"": [
      ""admin""
    ],
    ""FirstName"": ""my"",
    ""LastName"": ""admin"",
    ""CreatedOn"": ""2017-02-21T18:01:20.8779595Z"",
    ""LastModified"": ""2017-02-21T18:01:20.8779595Z"",
    ""LastPasswordChange"": ""2017-02-21T18:01:20.8779595Z"",
    ""LastLogin"": ""2017-02-21T20:22:39.0324982Z""
  },
  {
    ""Id"": ""E342C3E2-EFB3-4ED4-8B36-9582235A53CF"",
    ""UserName"": ""user@user.com"",
    ""Password"": ""ec205fc189684fe62f687eac68c8bde9"",
    ""Claims"": [ ],
    ""FirstName"": ""my"",
    ""LastName"": ""user"",
    ""CreatedOn"": ""2017-02-21T18:01:20.8779595Z"",
    ""LastModified"": ""2017-02-21T18:01:20.8779595Z"",
    ""LastPasswordChange"": ""2017-02-21T18:01:20.8779595Z"",
    ""LastLogin"": ""2017-02-21T20:22:39.0324982Z""
  }
]");

            _configuration = new Mock<IConfiguration>();

            _userRepository = new FormsUserRepository(_path.Object, _file.Object, _configuration.Object);
        }

        [TestMethod]
        public void UserIdentityById()
        {
            var user = _userRepository.User(new Guid("8e2b3a59-b210-4e91-8a72-8e7179c61e20"));
            user.UserName.Should().Be("admin@admin.com");
            user.Password.Should().Be("ec205fc189684fe62f687eac68c8bde9");
            user.Claims.ShouldBeEquivalentTo(new[] {"admin"});
            user.FirstName.Should().Be("my");
            user.LastName.Should().Be("admin");
        }

        [TestMethod]
        public void UserIdentityByName()
        {
            var user = _userRepository.User("user@user.com");
            user.UserName.Should().Be("user@user.com");
            user.Password.Should().Be("ec205fc189684fe62f687eac68c8bde9");
            user.Claims.Should().BeEmpty();
            user.FirstName.Should().Be("my");
            user.LastName.Should().Be("user");
        }

        [TestMethod]
        public void GetAllUsers()
        {
            var users = _userRepository.GetAllUsers().ToList();
            users.Count().Should().Be(2);
            users.All(user => string.IsNullOrEmpty(user.Password)).Should().BeTrue();
        }

        [TestMethod]
        public void GetAllAdminUsers()
        {
            var users = _userRepository.GetAdminUsers().ToList();
            users.Count.Should().Be(1);
            users[0].UserName.Should().Be("admin@admin.com");
        }

        [TestMethod]
        public void AddAUser()
        {
            _userRepository.AddUser(new UserIdentity("user1", "xxx", new string[0], "user", "one"));
            _file.Verify(f => f.Move(It.IsAny<string>(), It.IsAny<string>()));
            _file.Verify(f => f.WriteAllText(It.IsAny<string>(), It.IsAny<string>()));
        }

        [TestMethod]
        public void UpdateAUser()
        {
            var user = _userRepository.User("user@user.com");
            user.FirstName = "my-my";
            _userRepository.UpdateUser(user);
            _file.Verify(f => f.Move(It.IsAny<string>(), It.IsAny<string>()));
            _file.Verify(f => f.WriteAllText(It.IsAny<string>(), It.IsAny<string>()));
        }

        [TestMethod]
        public void DeleteAUser()
        {
            _userRepository.DeleteUser(new UserIdentity("user1", "xxx", new string[0], "user", "one"));
            _file.Verify(f => f.Move(It.IsAny<string>(), It.IsAny<string>()));
            _file.Verify(f => f.WriteAllText(It.IsAny<string>(), It.IsAny<string>()));
        }
    }
}