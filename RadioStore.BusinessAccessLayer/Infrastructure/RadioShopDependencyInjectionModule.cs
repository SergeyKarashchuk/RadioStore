using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using RadioStore.BusinessAccessLayer.AbstractsDTO;
using RadioStore.BusinessAccessLayer.CocreteDTO;
using RadioStore.DataAccessLayer.Abstracts;
using RadioStore.DataAccessLayer.Concrete;

namespace RadioStore.BusinessAccessLayer.Infrastructure
{
    public class RadioShopDependencyInjectionModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWorkDTO>().To<UnitOfWorkDTO>();
            Bind<IUnitOfWork>().To<UnitOfWork>();
        }
    }
}
