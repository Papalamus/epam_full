using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Containers
{
    public interface IContainer
    {
        T Resolve<T>() where T : class;

        void Register<TFrom, TTo>() where TTo : class
            where TFrom : TTo;

      //  T Resolve<T>(params object[] args) where T : class;
    }
}
