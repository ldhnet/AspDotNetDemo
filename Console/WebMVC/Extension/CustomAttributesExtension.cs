using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebMVC.Extension
{
    public static class CustomAttributesExtension
    {
        public static bool IsContainAttribute(this IEnumerable<System.Reflection.CustomAttributeData> list, Type _type)
        { 
            return list.Any(a => a.AttributeType.Equals(_type));           
        }
    }
}
