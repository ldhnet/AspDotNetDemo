using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utility.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum obj)
        {
            object[]? array = obj.GetType().GetField(obj.ToString())?.GetCustomAttributes(typeof(DescriptionAttribute), inherit: true);
            if (array != null)
            {
                var attr = array.FirstOrDefault(x => x is DescriptionAttribute);
                if (attr != null)
                {
                    return ((DescriptionAttribute)attr).Description;
                }

            }

            return string.Empty;
        }
    }
}
