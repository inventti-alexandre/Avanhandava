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
using Avanhandava.Domain.Abstract.Admin;

namespace Avanhandava.App_Start
{
    public class IocConfig
    {
        public static void ConfigurarDependencias()
        {
            // container
            IKernel kernel = new StandardKernel();

            // mapeamento - interfaces x classes
            kernel.Bind<IBaseService<Conta>>().To<ContaService>();
            kernel.Bind<IBaseService<Empresa>>().To<EmpresaService>();
            kernel.Bind<IBaseService<Estado>>().To<EstadoService>();
            kernel.Bind<IBaseService<Fornecedor>>().To<FornecedorService>();
            kernel.Bind<IBaseService<FPgto>>().To<FPgtoService>();
            kernel.Bind<IBaseService<GrupoCusto>>().To<GrupoCustoService>();
            kernel.Bind<IBaseService<ItemCusto>>().To<ItemCustoService>();
            kernel.Bind<IBaseService<Pgto>>().To<PgtoService>();
            kernel.Bind<IBaseService<SistemaParametro>>().To<SistemaParametroService>();
            kernel.Bind<ITrocaSenha>().To<UsuarioService>();
            kernel.Bind<IBaseService<Usuario>>().To<UsuarioService>();
            kernel.Bind<ILogin>().To<UsuarioService>();

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