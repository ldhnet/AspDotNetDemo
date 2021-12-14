namespace Framework.Utility.Attributes
{
    /// <summary>
    /// 数据库表映射特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class,AllowMultiple = false,Inherited = false)]
    public abstract class BaseMappingAttribute : Attribute
    { 
        private string _MappingName = null;
        public BaseMappingAttribute(string mappingName)
        { 
            this._MappingName = mappingName;
        }
        public string GetMappingName()
        { 
            return _MappingName;
        }
    }

    /// <summary>
    /// 数据库字段映射特性
    /// </summary> 
    public abstract class ColumnAttribute : BaseMappingAttribute
    {
        public ColumnAttribute(string columnName) : base(columnName)
        {
        }
    }
    /// <summary>
    /// 数据库表名映射特性
    /// </summary> 
    public abstract class TableAttribute : BaseMappingAttribute
    {
        public TableAttribute(string tableName) : base(tableName)
        {
        }
    }
}
