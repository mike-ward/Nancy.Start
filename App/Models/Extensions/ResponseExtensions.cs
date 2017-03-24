using System.IO;
using Nancy;

namespace App.Models.Extensions
{
    public static class ResponseExtensions
    {
        public static Response AsAttachment(
            this IResponseFormatter formatter, 
            Stream stream, 
            string attachmentName, 
            string contentType = "application/text")
        {
            Require.ArgumentNotNull(formatter, nameof(formatter));
            Require.ArgumentNotNullEmpty(attachmentName, nameof(attachmentName));
            Require.ArgumentNotNullEmpty(contentType, nameof(contentType));

            var response = formatter.FromStream(stream, contentType);
            response.Headers.Add("Content-Disposition", $"attachment; filename={attachmentName}");
            response.Headers.Add("Cache-Control", "no-cache");
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
    }
}