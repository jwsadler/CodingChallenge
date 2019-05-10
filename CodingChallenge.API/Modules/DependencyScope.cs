using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Castle.MicroKernel;
using Castle.MicroKernel.Lifestyle;

namespace CodingChallenge.API.Modules
{
    public class DependencyScope : IDependencyScope
    {
        private readonly IDisposable _disposable;
        private readonly IKernel _kernel;

        public DependencyScope(IKernel kernel)
        {
            _kernel = kernel;
            _disposable = kernel.BeginScope();
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
            _disposable.Dispose();
        }
    }
}