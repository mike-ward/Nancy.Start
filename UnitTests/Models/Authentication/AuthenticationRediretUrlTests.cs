using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TLA.Models.Authentication;

namespace UnitTests.Models.Authentication
{
    [TestClass]
    public class AuthenticationRediretUrlTests
    {
        [TestMethod]
        public void GetUrlShouldReturnLoginUrl()
        {
            var authenticationRedirectUrl = new AuthenticationRedirectUrl();
            authenticationRedirectUrl.GetUrl.Should().Be("login");
        }
    }
}