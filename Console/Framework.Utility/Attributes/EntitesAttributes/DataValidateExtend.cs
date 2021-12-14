using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utility.Attributes
{
    public static class DataValidateExtend
    {
        public static bool ValidateModel<T>(this T t)
        { 
            Type type= typeof(T);
            foreach (var prop in type.GetProperties())
            {
                if (prop.IsDefined(typeof(BaseValidateAttribute),true))
                { 
                    object oValue = prop.GetValue(t);
                    var attributeList = prop.GetCustomAttributes<BaseValidateAttribute>();

                    foreach (var attribute in attributeList)
                    {
                        if (!attribute.Validate(oValue))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }
}
