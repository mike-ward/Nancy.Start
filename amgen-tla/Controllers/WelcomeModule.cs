namespace TLA.Controllers
{
    public class WelcomeModule : BaseModule
    {
        public WelcomeModule()
        {
            Get["/"] = parameters => View["welcome"];
        }
    }
}