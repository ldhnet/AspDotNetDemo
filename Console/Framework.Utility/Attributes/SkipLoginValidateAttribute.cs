namespace Framework.Utility.Attributes
{
    /// <summary>
    /// 跳过登录验证特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class SkipLoginValidateAttribute : Attribute
    {

    }
}
