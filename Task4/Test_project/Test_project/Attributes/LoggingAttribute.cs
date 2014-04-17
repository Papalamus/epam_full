using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class LoggingAttribute : System.Attribute
    {
    }
}
