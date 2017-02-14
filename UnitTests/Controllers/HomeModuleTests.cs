using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nancy;
using Nancy.Testing;
using TLA.Controllers;
using TLA.Models.Authentication.Forms;

namespace UnitTests.Controllers
{
    [TestClass]
    public class HomeModuleTests
    {
        [TestMethod]
        public void HomePageRequiresAuthentication()
        {
            var browser = new Browser(with =>
            {
                with.RootPathProvider(new ViewFolderRootPathProvider());
                with.Module<HomeModule>();
                with.Dependency<FormsRedirectUrl>();
            });

            var response = browser.Get("/home");
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}