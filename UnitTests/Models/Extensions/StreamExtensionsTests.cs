using System.IO;
using System.Text;
using App.Models.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.Models.Extensions
{
    [TestClass]
    public class StreamExtensionsTests
    {
        [TestMethod]
        public void AsTextShouldReturnSameText()
        {
            var stream = new MemoryStream(Encoding.UTF8.GetBytes("This is some text"));
            stream.AsText().Should().Be("This is some text");
        }
    }
}