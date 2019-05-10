using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using CodingChallenge.API.Common.Json;
using CodingChallenge.API.Plumbing.MediaFormatters;
using Newtonsoft.Json;
using DependencyResolver = CodingChallenge.API.Modules.DependencyResolver;

namespace CodingChallenge.API
{
    public class WebApiApplication : HttpApplication
    {
        private static IWindsorContainer _container;

        protected void Application_Start()
        {
            ConfigureWindsor(GlobalConfiguration.Configuration);
            GlobalConfiguration.Configure(c => WebApiConfig.Register(c, _container));

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            //Converts null string to empty string and null enums to the default value
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings =
                new JsonSerializerSettings { ContractResolver = new NullToDefaultValueResolver() };

            // Add a reference here to the new MediaTypeFormatter that adds text/plain support
            GlobalConfiguration.Configuration.Formatters.Insert(0, new TextMediaTypeFormatter());
        }

        public static void ConfigureWindsor(HttpConfiguration configuration)
        {
            _container = new WindsorContainer();
            _container.Install(FromAssembly.This());
            _container.Kernel.Resolver.AddSubResolver(new CollectionResolver(_container.Kernel, true));
            var dependencyResolver = new DependencyResolver(_container.Kernel);
            configuration.DependencyResolver = dependencyResolver;
        }

        protected void Application_PostAuthorizeRequest()
        {
            if (IsWebApiRequest())
                HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
        }

        private bool IsWebApiRequest()
        {
            return HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.StartsWith(WebApiConfig
                .UrlPrefixRelative);
        }

        protected void Application_End()
        {
            _container.Dispose();
            Dispose();
        }
    }
}