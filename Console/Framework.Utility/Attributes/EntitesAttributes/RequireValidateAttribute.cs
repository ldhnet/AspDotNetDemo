namespace Framework.Utility.Attributes
{
    /// <summary>
    /// 必填验证特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public  class RequireValidateAttribute : BaseValidateAttribute
    {
        public override bool Validate(object oValue)
        {
            return oValue != null && !string.IsNullOrEmpty(oValue.ToString());
        }
    }
}
