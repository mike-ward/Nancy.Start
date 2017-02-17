using System.Linq;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Bootstrapper;
using Nancy.Elmah;
using Nancy.Json;
using Nancy.TinyIoc;
using TLA.Models;
using TLA.Models.Authentication;
using TLA.Models.Authentication.ActiveDirectory;
using TLA.Models.Authentication.Forms;
using IUserMapper = TLA.Models.Authentication.IUserMapper;

namespace TLA.Controllers
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override byte[] FavIcon => null;

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            Elmahlogging.Enable(pipelines, "elmah");
            JsonSettings.RetainCasing = true;
            JsonSettings.MaxJsonLength = int.MaxValue;
            StaticConfiguration.DisableErrorTraces = false;

            FormsAuthentication.Enable(pipelines,
                new FormsAuthenticationConfiguration
                {
                    RedirectUrl = container.Resolve<IAuthenticationRedirectUrl>().GetUrl,
                    UserMapper = container.Resolve<IUserMapper>()
                });
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            if (Configuration.ActiveDirectoryUserGroups().Any())
            {
                container.Register<IUserMapper, ActiveDirectoryUserMapper>();
                container.Register<IUserRepository, ActiveDirectoryUserRepository>();
            }
            else
            {
                container.Register<IUserMapper, FormsUserMapper>();
                container.Register<IUserRepository, FormsUserRepository>();
            }
        }
    }
}