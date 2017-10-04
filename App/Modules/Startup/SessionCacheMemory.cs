using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web.Configuration;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Cookies;
using Nancy.Session;

namespace App.Controllers.Startup
{
    public class SessionCacheMemory : IApplicationStartup
    {
        private const string CookieName = "_scm";
        private readonly MemoryCache _cache = MemoryCache.Default;

        public void Initialize(IPipelines pipelines)
        {
            var store = new SessionCacheMemory();
            pipelines.BeforeRequest.AddItemToStartOfPipeline(ctx => LoadSession(ctx, store));
            pipelines.AfterRequest.AddItemToEndOfPipeline(ctx => SaveSession(ctx, store));
        }

        private static int GetSessionTimeout()
        {
            var sessionSection = (SessionStateSection) WebConfigurationManager.GetSection("system.web/sessionState");
            return (int) sessionSection.Timeout.TotalMinutes;
        }

        private void Save(string sessionId, ISession session, Response response)
        {
            var sess = session as Session;
            if (sess == null) return;
            var dict = session.ToDictionary(x => x.Key, x => x.Value);
            var timeout = GetSessionTimeout();
            var cookie = new NancyCookie(CookieName, sessionId) {Expires = DateTime.UtcNow.AddMinutes(timeout)};
            _cache.Set(sessionId, dict, DateTime.Now + TimeSpan.FromMinutes(timeout + 1));
            response.WithCookie(cookie);
        }

        private ISession Load(NancyContext context)
        {
            var request = context.Request;
            return request.Cookies.ContainsKey(CookieName) && _cache.Any(kvp => kvp.Key == request.Cookies[CookieName])
                ? new Session(_cache[request.Cookies[CookieName]] as Dictionary<string, object>)
                : new Session(new Dictionary<string, object>());
        }

        private static void SaveSession(NancyContext context, SessionCacheMemory sessionStore)
        {
            var sessionId = context.Request.Cookies.ContainsKey(CookieName)
                ? context.Request.Cookies[CookieName]
                : Guid.NewGuid().ToString();
            sessionStore.Save(sessionId, context.Request.Session, context.Response);
        }

        private static Response LoadSession(NancyContext context, SessionCacheMemory sessionStore)
        {
            context.Request.Session = sessionStore.Load(context);
            return null;
        }
    }
}