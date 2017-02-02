using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nancy;
using Nancy.Testing;
using TLA.Controllers;

namespace UnitTests.Controllers
{
    [TestClass]
    public class IndexModuleTests
    {
        [TestMethod]
        public void IndexPageShouldReturn200Ok()
        {
            var browser = new Browser(with =>
            {
                with.RootPathProvider(new ViewFolderRootPathProvider());
                with.Module<IndexModule>();
            });

            var response = browser.Get("/");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            response.Body["#content h1"]
                .ShouldExistOnce()
                .And.ShouldContain("This space for rent");
        }
    }
}