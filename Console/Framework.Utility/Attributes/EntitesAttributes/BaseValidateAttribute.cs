namespace Framework.Utility.Attributes
{
    /// <summary>
    /// 字段验证特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property,AllowMultiple = true,Inherited = true)]
    public abstract class BaseValidateAttribute : Attribute
    {
        public abstract bool Validate(object value);
    }
}
