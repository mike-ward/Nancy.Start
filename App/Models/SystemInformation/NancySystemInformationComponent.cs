using System;
using System.Dynamic;
using System.Linq;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.ViewEngines;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace App.Models.SystemInformation
{
    public class NancySystemInformationComponent : ISystemInformationComponent
    {
        private readonly IRootPathProvider _rootPathProvider;

        public NancySystemInformationComponent(IRootPathProvider rootPathProvider)
        {
            _rootPathProvider = rootPathProvider;
        }

        public int Cardinality => 2;

        public string Title()
        {
            return "NancyFX Configuration";
        }

        public string Html()
        {
            var json = JsonConvert.SerializeObject(
                GetData(), Formatting.Indented,
                new JsonConverter[] {new StringEnumConverter()}) as string;

            return $"<pre>{json}</pre>";
        }

        public dynamic GetData()
        {
            dynamic data = new ExpandoObject();

            data.Nancy = new ExpandoObject();
            data.Nancy.Version = GetNancyVersion();
            data.Nancy.TracesDisabled = StaticConfiguration.DisableErrorTraces;
            data.Nancy.CaseSensitivity = StaticConfiguration.CaseSensitive ? "Sensitive" : "Insensitive";
            data.Nancy.RootPath = _rootPathProvider.GetRootPath();
            data.Nancy.Hosting = GetHosting();
            data.Nancy.BootstrapperContainer = GetBootstrapperContainer();
            data.Nancy.LocatedBootstrapper = NancyBootstrapperLocator.Bootstrapper.GetType().ToString();
            data.Nancy.LoadedViewEngines = GetViewEngines();

            return data;
        }

        private static string[] GetViewEngines()
        {
            var engines =
                AppDomainAssemblyTypeScanner.TypesOf<IViewEngine>();

            return engines
                .Select(engine => engine.Name.Split(new[] {"ViewEngine"}, StringSplitOptions.None)[0])
                .ToArray();
        }

        private static string GetBootstrapperContainer()
        {
            var name = AppDomain.CurrentDomain
                .GetAssemblies()
                .Select(asm => asm.GetName())
                .FirstOrDefault(asmName => asmName.Name != null && asmName.Name.StartsWith("Nancy.Bootstrappers."));

            return name == null ?
                "TinyIoC" :
                $"{name.Name.Split('.').Last()} (v{name.Version})";
        }

        private static string GetNancyVersion()
        {
            var name = AppDomain.CurrentDomain
                .GetAssemblies()
                .Select(asm => asm.GetName())
                .FirstOrDefault(asmName => asmName.Name != null && asmName.Name == "Nancy");

            return name == null ?
                "Unknown" :
                $"{name.Name.Split('.').Last()} (v{name.Version})";
        }

        private static string GetHosting()
        {
            var name = AppDomain.CurrentDomain
                .GetAssemblies()
                .Select(asm => asm.GetName())
                .FirstOrDefault(asmName => asmName.Name != null && asmName.Name.StartsWith("Nancy.Hosting."));

            return name == null ?
                "Unknown" :
                $"{name.Name.Split('.').Last()} (v{name.Version})";
        }
    }
}