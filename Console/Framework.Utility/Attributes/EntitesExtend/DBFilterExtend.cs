using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utility.Attributes.EntitesExtend
{
    public static class DBFilterExtend
    {
        public static IEnumerable<PropertyInfo> GetPropertiesWithNoKey(this Type type)
        {
            return type.GetProperties().Where(p => !p.IsDefined(typeof(KeyAttribute), true));
        }
    }
}
