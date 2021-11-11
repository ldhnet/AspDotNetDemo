using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Framework.Utility.Attributes
{ 
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ColumnExportFormatAttribute : Attribute
    {
        private string displayName;

        private ExcelCellFormat format;

        private string comments;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="displayName"></param>
        /// <param name="format"></param>
        ///  <param name="_comments"></param>
        public ColumnExportFormatAttribute(string displayName, ExcelCellFormat format, string _comments = "")
        {
            this.displayName = displayName;
            this.format = format;
            this.comments = _comments;
        }
    }

    /// <summary>
    /// Excel 单元格 格式
    /// </summary>
    public enum ExcelCellFormat
    {
        /// <summary>
        /// 普通字符串
        /// </summary>
        StringFormat,

        /// <summary>
        /// 日期
        /// </summary>
        DateFormat,

        /// <summary>
        /// 短时间，精确到分 
        /// </summary>
        ShortTimeFormat,

        /// <summary>
        /// 整型数字
        /// </summary>
        IntegerFormat,

        /// <summary>
        /// 非整型数，保留1位小数
        /// </summary>
        FloatFormat,

        /// <summary>
        /// 保留2位小数
        /// </summary>
        DecimalFormat,

        /// <summary>
        /// 长数字字符串
        /// </summary>
        LongNumStringFormat,

        /// <summary>
        /// 百分比
        /// </summary>
        PercentageFormat,
    }

    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class OrderAttribute : Attribute
    {
        private readonly int _order;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        public OrderAttribute([System.Runtime.CompilerServices.CallerLineNumber] int order = 0)
        {
            _order = order;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Order { get { return _order; } }
    }

    /// <summary>
    /// Excel导入列属性 
    /// </summary>
    public class ImportedColumnAttribute : Attribute
    {
        /// <summary>
        /// 列，如A,B...AJ
        /// </summary>
        public string Column { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Caption { get; set; }


        /// <summary>
        /// 批注
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// 不可为空值
        /// </summary>
        public bool NotNullValueRequired { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 格式
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool FormulaCell { get; set; }

        /// <summary>
        /// 公式
        /// </summary>
        public string Formula { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FormulaR1C1 { get; set; }

        /// <summary>
        /// 是否进行列汇总
        /// </summary>
        public bool Aggregate { get; set; }

        /// <summary>
        /// 汇总
        /// </summary>
        public AggregateInfo AggregateInfo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public PropertyInfo Property { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ImportedColumnAttribute()
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public struct AggregateInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public AggregateType AggregateType { get; set; }

        /// <summary>
        /// 列聚合公式
        /// sum({0}{1}:{0}{2}) 表示聚合是该列所有行数据的汇总，
        /// Aggregate = true 但聚合公式为空，表示该字段默认和Formula列公式一致
        /// </summary>
        public string AggregateFormula { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum AggregateType
    {
        /// <summary>
        /// 
        /// </summary>
        None,
        /// <summary>
        /// 当前列的汇总
        /// </summary>
        SumOfCurrentColumns,

        /// <summary>
        /// 当前行多个Cell的汇总
        /// </summary>
        SumOfCellsInCurrentRow
    }

    /// <summary>
    /// 
    /// </summary>
    public class SumAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public string Base { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Proportion { get; set; }
    }
}
