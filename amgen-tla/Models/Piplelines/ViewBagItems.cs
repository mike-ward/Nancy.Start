using Nancy;
using Nancy.Bootstrapper;
using TLA.Models.Authentication;

namespace TLA.Models.Piplelines
{
    public class ViewBagItems
    {
        public static void Enable(IPipelines pipelines)
        {
            pipelines.BeforeRequest.AddItemToStartOfPipeline(AddViewBagItems);
        }

        private static Response AddViewBagItems(NancyContext ctx)
        {
            return null;
        }
    }
}