using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    interface IInputHelper<T>
    {
        T makeObject();
        object requestID();
    }
}
