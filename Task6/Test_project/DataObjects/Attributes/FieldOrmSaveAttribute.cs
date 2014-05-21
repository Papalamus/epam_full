using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class FieldOrmSaveAttribute : System.Attribute
    {
        public string Name { get; set; }
        public Type type { get; set; }
    }
}
