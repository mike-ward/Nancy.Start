using System.Net;
using System.Text.RegularExpressions;

namespace App.Models.Extensions
{
    public static class StringExtensions
    {
        public static string StripHtml(this string html, bool decode = true)
        {
            var reg = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
            var stripped = reg.Replace(html, "");
            return decode ? WebUtility.HtmlDecode(stripped) : stripped;
        }
    }
}