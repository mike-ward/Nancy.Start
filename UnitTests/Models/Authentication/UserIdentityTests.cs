using System;
using App.Models.Authentication;
using App.Models.Utilities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.Models.Authentication
{
    [TestClass]
    public class UserIdentityTests
    {
        [TestMethod]
        public void UserIdentityConstructorParameterTests()
        {
            Action action = () => new UserIdentity(null, null, null, null, null);
            action.Should().Throw<ArgumentException>().WithMessage("*userName");

            action = () => new UserIdentity("name", null, null, null, null);
            action.Should().Throw<ArgumentException>().WithMessage("*password");

            action = () => new UserIdentity("name", "password", null, null, null);
            action.Should().Throw<ArgumentException>().WithMessage("*claims");

            action = () => new UserIdentity("name", "password", new string[0], null, null);
            action.Should().Throw<ArgumentException>().WithMessage("*firstName");

            action = () => new UserIdentity("name", "password", new string[0], "firstName", null);
            action.Should().Throw<ArgumentException>().WithMessage("*lastName");

            action = () => new UserIdentity("name", "password", new string[0], "firstName", "lastName");
            action.Should().NotThrow();
        }

        [TestMethod]
        public void NewUserIdentityShouldHaveValidNamesAndDates()
        {
            var now = DateTime.UtcNow.Subtract(TimeSpan.FromMinutes(1));
            var id = new UserIdentity("name", "password", new string[0], "firstName", "lastName");

            id.Id.Should().NotBe(Guid.Empty);
            id.UserName.Should().Be("name");
            id.Password.Should().Be(Encryption.ComputeHash("password", id.Id));
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
        public void HashPasswordParameterTests()
        {
            Action action = () => Encryption.ComputeHash(null, Guid.Empty);
            action.Should().Throw<ArgumentException>().WithMessage("*text");

            action = () => Encryption.ComputeHash("password", Guid.Empty);
            action.Should().Throw<ArgumentException>().WithMessage("salt is not a valid GUID");

            action = () => Encryption.ComputeHash("password", Guid.NewGuid());
            action.Should().NotThrow();
        }

        [TestMethod]
        public void EncryptPasswordShouldBePure()
        {
            var salt = Guid.NewGuid();
            var pw1 = Encryption.ComputeHash("password", salt);
            var pw2 = Encryption.ComputeHash("password", salt);
            pw1.Should().Be(pw2);
        }
    }
}