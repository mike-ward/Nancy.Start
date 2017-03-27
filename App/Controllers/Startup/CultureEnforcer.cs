using System.Globalization;
using System.Threading;
using Nancy;
using Nancy.Bootstrapper;

namespace App.Controllers.Startup
{
    public class CultureEnforcer : IRequestStartup
    {
        public void Initialize(IPipelines pipelines, NancyContext context)
        {
            pipelines.BeforeRequest.AddItemToStartOfPipeline(ctx => EnforceCulture());
        }

        private static Response EnforceCulture()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            return null;
        }
    }
}