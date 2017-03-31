using System.IO;
using Nancy;
using Nancy.Helpers;

namespace App.Models.SystemInformation
{
    public class WebConfigSystemInformationComponent : ISystemInformationComponent
    {
        private readonly IRootPathProvider _rootPathProvider;

        public WebConfigSystemInformationComponent(IRootPathProvider rootPathProvider)
        {
            _rootPathProvider = rootPathProvider;
        }

        public int Cardinality => 3;

        public string Title()
        {
            return "Web.config";
        }

        public string Html()
        {
            return $"<pre>{GetData()}</pre>";
        }

        private string GetData()
        {
            var config = File.ReadAllText(_rootPathProvider.GetRootPath() + "Web.config");
            return HttpUtility.HtmlEncode(config);
        }
    }
}