using Ninject;
using RadioStore.BusinessAccessLayer.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RadioStore.WebApplication.Infrastructure
{
    public class RadioShopDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public RadioShopDependencyResolver()
        {
            kernel = new StandardKernel(new RadioShopDependencyInjectionModule());
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
    }
}