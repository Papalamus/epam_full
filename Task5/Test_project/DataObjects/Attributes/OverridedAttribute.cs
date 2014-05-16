using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class OverridedAttribute : System.Attribute
    {

    }
}
