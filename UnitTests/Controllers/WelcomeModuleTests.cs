using App.Controllers;
using App.Controllers.Authentication;
using App.Models.Authentication;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nancy;
using Nancy.Testing;

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
                with.Module<IndexModule>();
                with.Dependency<FormsAuthenticationModule>();
                with.Dependency<ModuleStaticWrappers>();
            });

            var response = browser.Get("/");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}