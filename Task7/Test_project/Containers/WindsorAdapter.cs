using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace Containers
{
    class WindsorAdapter:IContainer
    {
        WindsorContainer _container = new WindsorContainer();

        public T Resolve<T>() where T : class
        {
            return _container.Resolve<T>();
        }

        public void Register<TFrom, TTo>()
            where TFrom : TTo
            where TTo : class
        {
            _container.Register(Component.For<TTo>().ImplementedBy<TFrom>());
        }

        
    }
}
