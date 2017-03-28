using System;
using System.Linq;
using App.Models;
using App.Models.Account.Admin.SystemInformation;
using App.Models.Authentication;
using App.Models.Authentication.ActiveDirectory;
using App.Models.Authentication.Forms;
using App.Models.Utilities;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Bootstrapper;
using Nancy.Elmah;
using Nancy.Json;
using Nancy.TinyIoc;
using IUserMapper = App.Models.Authentication.IUserMapper;

namespace App.Controllers.Startup
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
            var configuration = container.Resolve<IConfiguration>();

            if (configuration.ActiveDirectoryUserGroups().Any())
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

        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);

            if (context.Request.Url.Path.EndsWith("system-information", StringComparison.OrdinalIgnoreCase))
            {
                context.ViewBag.SystemInformationComponents = container
                    .ResolveAll<ISystemInformationComponent>()
                    .Distinct(new SystemInformationComponentEqualityComparer())
                    .OrderBy(c => c.Cardinality)
                    .ToArray();
            }
        }
    }
}