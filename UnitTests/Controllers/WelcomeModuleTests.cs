using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nancy;
using Nancy.Testing;
using TLA.Controllers;
using TLA.Models.Authentication.Forms;

namespace UnitTests.Controllers
{
    [TestClass]
    public class WelcomeModuleTests
    {
        [TestMethod]
        public void WelcomePageShouldReturn200Ok()
        {
            var browser = new Browser(with =>
            {
                with.RootPathProvider(new ViewFolderRootPathProvider());
                with.Module<WelcomeModule>();
                with.Dependency<FormsRedirectUrl>();
            });

            var response = browser.Get("/");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}