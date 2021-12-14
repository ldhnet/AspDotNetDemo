namespace Framework.Utility.Attributes
{
    /// <summary>
    /// 长度验证特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public  class LengthValidateAttribute : BaseValidateAttribute
    {
        public int _iMin = 0;
        public int _iMax = 0;

        public LengthValidateAttribute(int min, int max)
        {
            this._iMin = min;
            this._iMax = max;
        }

        public override bool Validate(object oValue)
        {
            return oValue != null 
                && !string.IsNullOrEmpty(oValue.ToString())
                && oValue.ToString().Length >= this._iMin
                && oValue.ToString().Length < this._iMax;

        }
    }
}
