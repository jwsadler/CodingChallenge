using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Dispatcher;
using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using CodingChallenge.API.BusinessLogic.Interfaces;
using CodingChallenge.API.BusinessLogic.Profiles;
using CodingChallenge.API.BusinessLogic.Services;
using CodingChallenge.API.Common.Interfaces;
using CodingChallenge.API.Plumbing;

namespace CodingChallenge.API
{
    public static class WebApiConfig
    {
        public static string UrlPrefix => "api";

        public static string UrlPrefixRelative => "~/api";

        public static void Register(HttpConfiguration config, IWindsorContainer container)
        {
            var corsAttr = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(corsAttr);

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/plain"));
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/jsonp"));
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            config.MessageHandlers.Add(new LogRequestAndResponseHandler());

            MapRoutes(config);
            RegisterControllerActivator(container);
            // Web API configuration and services
        }

        private static void MapRoutes(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{action}/{id}",
                new { id = RouteParameter.Optional, format = RouteParameter.Optional }
                );
        }

        private static void RegisterControllerActivator(IWindsorContainer container)
        {
            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator),
                new WindsorCompositionRoot(container));

            container.Register(
               Classes.FromAssemblyContaining<ILoggingService>()
                    .BasedOn<IService>()
                    .WithService.FirstInterface().LifestyleTransient());

            container.Register(
                Classes.FromAssemblyContaining<ILoggingService>()
                    .BasedOn<IHelper>()
                    .WithService.FirstInterface().LifestyleSingleton());

            container.Register(
                Classes.FromAssemblyContaining<IAPIConfigurationHelper>()
                    .BasedOn<IService>()
                    .WithService.FirstInterface().LifestyleTransient());

            container.Register(
                Classes.FromAssemblyContaining<IAPIConfigurationHelper>()
                    .BasedOn<IHelper>()
                    .WithService.FirstInterface().LifestyleSingleton());

            var filter = new AssemblyFilter(HttpRuntime.BinDirectory, "CodingChallenge.*.dll");

            container.Register(
                Classes.FromAssemblyInDirectory(filter)
                    .BasedOn<IValidationService>()
                    .WithService.FirstInterface().LifestyleTransient());

            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel, true));

            Mapper.Initialize(m =>
            {
                m.ConstructServicesUsing(container.Resolve);
                m.AddProfile<APIMappingProfile>();
            });
            Mapper.AssertConfigurationIsValid();
        }
    }
}