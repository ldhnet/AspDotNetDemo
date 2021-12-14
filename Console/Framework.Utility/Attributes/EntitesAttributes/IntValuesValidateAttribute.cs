 namespace Framework.Utility.Attributes
{
    /// <summary>
    /// 长度验证特性
    /// </summary> 
    public  class IntValuesValidateAttribute : BaseValidateAttribute
    {
        private int[] _Values = null; 

        public IntValuesValidateAttribute(params int[] values)
        {
            this._Values = values; 
        }

        public override bool Validate(object oValue)
        {
            return oValue != null 
                && !string.IsNullOrEmpty(oValue.ToString())
                && int.TryParse(oValue.ToString(), out int iValue)
                && this._Values !=null
                && this._Values.Contains(iValue);

        }
    }
}
