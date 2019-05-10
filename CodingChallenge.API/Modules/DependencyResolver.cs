using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Castle.MicroKernel;
using IDependencyResolver = System.Web.Http.Dependencies.IDependencyResolver;

namespace CodingChallenge.API.Modules
{
    internal class DependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public DependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
        }

        public IDependencyScope BeginScope()
        {
            return new DependencyScope(_kernel);
        }

        public object GetService(Type type)
        {
            return _kernel.HasComponent(type) ? _kernel.Resolve(type) : null;
        }

        public IEnumerable<object> GetServices(Type type)
        {
            return _kernel.ResolveAll(type).Cast<object>();
        }

        public void Dispose()
        {
        }
    }
}