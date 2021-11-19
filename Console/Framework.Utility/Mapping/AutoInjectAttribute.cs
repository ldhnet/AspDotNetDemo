using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utility.Mapping
{
    public sealed class MapperInitAttribute : Attribute
    {
        public Type SourceType { get; }
        public Type TargetType { get; }

        public MapperInitAttribute(Type sourceType, Type targetType)
        {
            SourceType = sourceType;
            TargetType = targetType;
        }
    }
}
