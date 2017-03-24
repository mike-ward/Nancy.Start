using System.IO;

namespace App.Models.Extensions
{
    public static class StreamExtensions
    {
        public static string AsText(this Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}