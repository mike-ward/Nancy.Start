using System.Text.RegularExpressions;
using System.Web;

namespace App.Models.Extensions
{
    public static class StringExtensions
    {
        public static string StripHtml(this string html, bool decode = true)
        {
            var reg = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
            var stripped = reg.Replace(html, "");
            return decode ? HttpUtility.HtmlDecode(stripped) : stripped;
        }
    }
}