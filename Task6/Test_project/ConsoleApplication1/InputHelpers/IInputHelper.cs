using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.InputHelpers
{
    interface IInputHelper<out T>
    {
        T MakeObject();
        object RequestId();
    }
}
