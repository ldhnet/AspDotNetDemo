using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utility.Mapping
{
    public sealed class AutoInjectAttribute : Attribute
    {
        public Type SourceType { get; }
        public Type TargetType { get; }

        public AutoInjectAttribute(Type sourceType, Type targetType)
        {
            SourceType = sourceType;
            TargetType = targetType;
        }
    }
}
