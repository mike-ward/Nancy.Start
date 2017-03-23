using App.Models.Authentication;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.Models.Authentication
{
    [TestClass]
    public class AuthenticationRediretUrlTests
    {
        [TestMethod]
        public void GetUrlShouldReturnLoginUrl()
        {
            var authenticationRedirectUrl = new AuthenticationRedirectUrl();
            authenticationRedirectUrl.GetUrl.Should().Be($"~/{AuthenticationRedirectUrl.Url}");
        }
    }
}