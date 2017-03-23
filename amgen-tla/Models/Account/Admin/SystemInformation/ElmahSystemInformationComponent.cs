using System;
using System.IO;
using System.Linq;
using System.Xml;
using Elmah;
using Nancy;

namespace TLA.Models.Account.Admin.SystemInformation
{
    public class ElmahSystemInformationComponent : ISystemInformationComponent
    {
        private readonly IRootPathProvider _rootPathProvider;

        public ElmahSystemInformationComponent(IRootPathProvider rootPathProvider)
        {
            _rootPathProvider = rootPathProvider;
        }

        public int Cardinality => 100;

        public string Title()
        {
            return "ELMAH Logs (Last 20)";
        }

        public string Html()
        {
            try
            {
                var path = Path.Combine(_rootPathProvider.GetRootPath(), "App_Data/Elmah");
                var info = new DirectoryInfo(path);
                var logs = info.GetFiles("*.xml")
                    .OrderByDescending(f => f.CreationTime)
                    .Select(f =>
                    {
                        using (var xmlReader = XmlReader.Create(new StreamReader(f.FullName)))
                        {
                            var error = ErrorXml.Decode(xmlReader);
                            return $"\n--- {f.Name}\n{XmlErrorToString(error)}";
                        }
                    })
                    .Take(20);

                return $"<pre>{string.Join("\n", logs)}</pre>";
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                return $"<pre>{ex.Message}</pre>";
            }
        }

        private static string XmlErrorToString(Error error)
        {
            var str = "";

            str += $"\nTime:             {error?.Time}";
            str += $"\nMessage:          {error?.Message ?? ""}";
            str += $"\nType:             {error?.Type ?? ""}";
            str += $"\nStatus Code:      {error?.StatusCode}";
            str += $"\nUser:             {error?.User ?? ""}";
            str += $"\nSource:           {error?.Source ?? ""}";
            str += $"\nDetail:           {error?.Detail ?? ""}";

            return str;
        }
    }
}