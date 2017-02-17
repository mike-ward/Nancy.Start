using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TLA.Models.Authentication;

namespace UnitTests.Models.Authentication
{
    [TestClass]
    public class UserIdentityTests
    {
        [TestMethod]
        public void UserIdentityConstructorParameterTests()
        {
            Action action = () => new UserIdentity(null, null, null, null, null);
            action.ShouldThrow<ArgumentException>().WithMessage("*userName");

            action = () => new UserIdentity("name", null, null, null, null);
            action.ShouldThrow<ArgumentException>().WithMessage("*password");

            action = () => new UserIdentity("name", "password", null, null, null);
            action.ShouldThrow<ArgumentException>().WithMessage("*claims");

            action = () => new UserIdentity("name", "password", new string[0], null, null);
            action.ShouldThrow<ArgumentException>().WithMessage("*firstName");

            action = () => new UserIdentity("name", "password", new string[0], "firstName", null);
            action.ShouldThrow<ArgumentException>().WithMessage("*lastName");

            action = () => new UserIdentity("name", "password", new string[0], "firstName", "lastName");
            action.ShouldNotThrow();
        }

        [TestMethod]
        public void NewUserIdentityShouldHaveValidNamesAndDates()
        {
            var now = DateTime.UtcNow.Subtract(TimeSpan.FromMinutes(1));
            var id = new UserIdentity("name", "password", new string[0], "firstName", "lastName");

            id.Id.Should().NotBe(Guid.Empty);
            id.UserName.Should().Be("name");
            id.Password.Should().Be(UserIdentity.EncryptPassword("password", id.Id));
            id.Claims.Should().BeEmpty();
            id.FirstName.Should().Be("firstName");
            id.LastName.Should().Be("lastName");

            var date = id.CreatedOn;
            date.Should().BeAfter(now);
            id.LastModified.Should().BeSameDateAs(date);
            id.LastPasswordChange.Should().BeSameDateAs(date);
            id.LastLogin.Should().Be(DateTime.MinValue);
        }

        [TestMethod]
        public void EncryptPasswordParameterTests()
        {
            Action action = () => UserIdentity.EncryptPassword(null, Guid.Empty);
            action.ShouldThrow<ArgumentException>().WithMessage("*password");

            action = () => UserIdentity.EncryptPassword("password", Guid.Empty);
            action.ShouldThrow<ArgumentException>().WithMessage("*salt");

            action = () => UserIdentity.EncryptPassword("password", Guid.NewGuid());
            action.ShouldNotThrow();
        }

        [TestMethod]
        public void EncryptPasswordShouldBePure()
        {
            var salt = Guid.NewGuid();
            var pw1 = UserIdentity.EncryptPassword("password", salt);
            var pw2 = UserIdentity.EncryptPassword("password", salt);
            pw1.Should().Be(pw2);
        }
    }
}