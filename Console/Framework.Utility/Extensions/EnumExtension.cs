using System.ComponentModel;
using System.Reflection;

namespace Framework.Utility.Extensions
{
    public static class EnumExtension
    {
        /// <summary>
        /// 获取枚举的描述信息
        /// </summary>
        public static string ToDescription(this Enum em)
        {
            Type type = em.GetType();
            FieldInfo fd = type.GetField(em.ToString());
            if (fd == null)
                return string.Empty;
            object[] attrs = fd.GetCustomAttributes(typeof(DescriptionAttribute), false);
            string name = string.Empty;
            foreach (DescriptionAttribute attr in attrs)
            {
                name = attr.Description;
            }
            return name;
        }
    }
}
