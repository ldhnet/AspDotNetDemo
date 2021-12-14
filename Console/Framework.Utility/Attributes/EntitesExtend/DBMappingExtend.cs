using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utility.Attributes.EntitesExtend
{
    public static class DBMappingExtend
    {
        public static string GetMappingName<T>(this T t) where T : MemberInfo
        {
            if (t.IsDefined(typeof(BaseMappingAttribute), true))
            {
                var attribute = t.GetCustomAttribute<BaseMappingAttribute>();
                return attribute.GetMappingName();
            } 
            else
            {
                return t.Name;
            }
        }
    }
}
