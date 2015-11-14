using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using Ninject.Syntax;
using System.Web.Mvc;
using Avanhandava.Domain.Abstract;
using Avanhandava.Domain.Models.Admin;
using Avanhandava.Domain.Service.Admin;

namespace Avanhandava.App_Start
{
    public class IocConfig
    {
        public static void ConfigurarDependencias()
        {
            // container
            IKernel kernel = new StandardKernel();

            // mapeamento - interfaces x classes
            kernel.Bind<IBaseService<SistemaParametro>>().To<SistemaParametroService>();
            kernel.Bind<IBaseService<Usuario>>().To<UsuarioService>();

            // registro do container
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }

    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IResolutionRoot _resolutionRoot;

        public NinjectDependencyResolver(IResolutionRoot kernel)
        {
            _resolutionRoot = kernel;
        }

        public object GetService(Type serviceType)
        {
            return _resolutionRoot.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _resolutionRoot.GetAll(serviceType);
        }
    }
}