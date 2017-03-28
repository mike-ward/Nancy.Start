using System;
using App.Models.Authentication;
using App.Models.Authentication.Forms;
using App.Models.Utilities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Nancy;
using Nancy.ViewEngines;

namespace UnitTests.Models.Authentication.Forms
{
    [TestClass]
    public class FormsUserMapperTests
    {
        [TestMethod]
        public void AddUserShouldReturnGuid()
        {
            var userMapper = new FormsUserMapper();
            userMapper.AddUser("user", "first", "last", new string[0]).Should().NotBeEmpty();
        }

        [TestMethod]
        public void AddedUserShouldBeRetrievable()
        {
            var userMapper = new FormsUserMapper();
            var guid = userMapper.AddUser("user", "first", "last", new string[0]);
            var user = (UserIdentity) userMapper.GetUserFromIdentifier(guid, null);
            user.UserName.Should().Be("user");
            user.FirstName.Should().Be("first");
            user.LastName.Should().Be("last");
            user.Claims.Should().BeEmpty();
        }

        [TestMethod]
        public void AuthenticateShouldPassWithValidCredentials()
        {
            var nancyModule = new Mock<INancyModule>();
            var userMapper = new FormsUserMapper();
            var configuration = new Mock<IConfiguration>();
            var userRepository = new Mock<IUserRepository>();
            var userCredentials = new UserCredentials {User = "name", Password = "xxx"};
            var viewRenderer = new Mock<IViewRenderer>();
            var moduleStaticWrappers = new Mock<IModuleStaticWrappers>();

            var user = new UserIdentity("name", "xxx", new[] {"admin"}, "first", "last");
            userRepository.Setup(ur => ur.UpdateUser(user)).Verifiable();
            userRepository.Setup(ur => ur.Authenticate(userCredentials.User, userCredentials.Password)).Returns(user).Verifiable();

            moduleStaticWrappers
                .Setup(msw => msw.LoginAndRedirect(nancyModule.Object, It.IsAny<Guid>(), null, "~/home"))
                .Returns(new Response());

            userMapper.Authenticate(
                    nancyModule.Object,
                    userMapper,
                    configuration.Object,
                    userRepository.Object,
                    userCredentials,
                    viewRenderer.Object,
                    moduleStaticWrappers.Object)
                .Should().BeOfType<Response>();

            nancyModule.Verify();
            userRepository.Verify();
        }

        [TestMethod]
        public void AuthenticateShouldFailWithBadCredentials()
        {
            var nancyModule = new Mock<INancyModule>();
            var userMapper = new FormsUserMapper();
            var configuration = new Mock<IConfiguration>();
            var userRepository = new Mock<IUserRepository>();
            var userCredentials = new UserCredentials {User = "name", Password = "xxx"};
            var viewRenderer = new Mock<IViewRenderer>();
            var moduleStaticWrappers = new Mock<IModuleStaticWrappers>();

            nancyModule
                .Setup(nm => nm.Context)
                .Returns(new NancyContext())
                .Verifiable();

            viewRenderer
                .Setup(vr => vr.RenderView(nancyModule.Object.Context, AuthenticationRedirectUrl.Url, null))
                .Verifiable();

            userMapper.Authenticate(
                nancyModule.Object,
                userMapper,
                configuration.Object,
                userRepository.Object,
                userCredentials,
                viewRenderer.Object,
                moduleStaticWrappers.Object);

            nancyModule.Verify();
            userRepository.Verify();
        }
    }
}