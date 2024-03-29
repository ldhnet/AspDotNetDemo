﻿using Framework.Utility.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utility.Excel
{
    public class ExcelClassInfo
    {
        [ExportColumn(Title = "用户ID", Format = ExcelCellFormat.IntegerFormat, Comments = "用户的唯一ID")]
        public int UserId { get; set; }
        [ExportColumn(Title = "用户名称", Format = ExcelCellFormat.StringFormat)]
        public string UserName { get; set; }
        [ExportColumn(Title = "用户年龄")]
        public int Age { get; set; }
        [ExportColumn(Title = "用户类型", Format = ExcelCellFormat.IntegerFormat)]
        public int UserType { get; set; }
        [ExportColumn(Title = "创建时间", Format = ExcelCellFormat.ShortTimeFormat)]
        public DateTime? CreateTime { get; set; }
        //[ExportColumn(Title = "账户余额", Format = ExcelCellFormat.DecimalFormat)]
        public decimal MoneyCount { get; set; }

        //[ExportColumn(Title = "账户积分", Format = ExcelCellFormat.FloatFormat)]
        public float PointCount { get; set; }

        [ExportColumn(Title = "用户描述")]
        public string Description { get; set; }
    }
    public class ImportClassInfo
    {
        [ImportedColumn(Caption = "用户ID",  Comments = "用户的唯一ID")]
        public int UserId { get; set; }
        [ImportedColumn(Caption = "用户名称", Comments = "用户名称")]
        public string UserName { get; set; }
        [ImportedColumn(Caption = "用户年龄",Format = ExcelCellFormat.IntegerFormat)]
        public int Age { get; set; }
        [ImportedColumn(Caption = "用户类型")]
        public int UserType { get; set; }
        [ImportedColumn(Caption = "创建时间", Format = ExcelCellFormat.ShortTimeFormat)]
        public DateTime? CreateTime { get; set; }
        [ImportedColumn(Caption = "账户余额", Format = ExcelCellFormat.DecimalFormat)]
        public decimal MoneyCount { get; set; }

        //[ExportColumn(Title = "账户积分", Format = ExcelCellFormat.FloatFormat)]
        public float PointCount { get; set; }

        [ImportedColumn(Caption = "用户描述")]
        public string Description { get; set; }
    }
}
