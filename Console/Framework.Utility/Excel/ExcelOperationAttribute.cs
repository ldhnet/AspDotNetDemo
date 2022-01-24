using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utility.Excel
{
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

    [AttributeUsage(AttributeTargets.Property)]
    public class ExcelOperationAttribute: Attribute
    {
        public string Title { get; set; }
        //public HSSFellStyle CellType { get; set; }
    }
    /// <summary>
    ///  Excel导出列属性 设置
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ExportColumnAttribute : Attribute
    {
        public string Title { get; set; }

        public ExcelCellFormat Format { get; set; }

        public string Comments { get; set; } 
    }

    /// <summary>
    /// Excel导入列属性 设置
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ImportedColumnAttribute : Attribute
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string Caption { get; set; }
         
        /// <summary>
        /// 批注
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        public bool Required { get; set; } =false;

        /// <summary>
        /// 格式
        /// </summary>
        public ExcelCellFormat Format { get; set; } = ExcelCellFormat.StringFormat;
    }
}
