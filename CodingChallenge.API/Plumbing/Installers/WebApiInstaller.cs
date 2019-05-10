using System.Web.Http;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CodingChallenge.API.BusinessLogic.Controllers;
using CodingChallenge.API.Common.Helpers;
using log4net;

namespace CodingChallenge.API.Plumbing.Installers
{
    public class WebApiInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes
                    .FromThisAssembly()
                    .BasedOn<BaseApiController>()
                    .LifestylePerWebRequest()
                );

            container.Register(
                Classes
                    .FromAssemblyContaining<BaseApiController>()
                    .BasedOn<ApiController>()
                    .LifestylePerWebRequest()
            );


            container.Register(
                Component.For<ILog>().UsingFactoryMethod((kernel, componentModel, creationContext) =>
                    LogManager.GetLogger(creationContext.Handler.ComponentModel.Name)).LifeStyle.Transient);

           container.Register(
                Component.For<ILog>()
                    .Named("LoggingService")
                    .UsingFactoryMethod((kernel, componentModel, creationContext) => LogManager.GetLogger("LoggingService")).LifeStyle.Transient);

            ContainerHelper.Container = container;

        }
    }
}