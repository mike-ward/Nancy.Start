using App.Models.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.Models.Extensions
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void StripHtmlShouldStripMarkup()
        {
            "<b>this is some bold text</b>".StripHtml().Should().Be("this is some bold text");
        }

        [TestMethod]
        public void StripHtmlShouldConvertEntities()
        {
            "&gt;".StripHtml().Should().Be(">");
            var x = "&nbsp;".StripHtml();
            "&apos;".StripHtml().Should().Be("'");
            "&quot;".StripHtml().Should().Be("\"");
            "&nbsp;".StripHtml().Should().Be("\xA0");
        }
    }
}