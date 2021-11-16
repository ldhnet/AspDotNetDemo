namespace Framework.Utility.Extensions
{
    public static class CustomAttributesExtension
    {
        public static bool IsContainAttribute(this IEnumerable<System.Reflection.CustomAttributeData> list, Type _type)
        {
            return list.Any(a => a.AttributeType.Equals(_type));
        }
    }
}
