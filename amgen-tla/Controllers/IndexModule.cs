
namespace TLA.Controllers
{
    public class IndexModule : BaseModule
    {
        public IndexModule()
        {
            Get["/"] = parameters => View["index"];
        }
    }
}