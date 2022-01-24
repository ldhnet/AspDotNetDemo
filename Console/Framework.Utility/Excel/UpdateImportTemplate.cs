using Framework.Utility.Attributes;
using Framework.Utility.Extensions;
using Framework.Utility.Helper;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utility.Excel
{
    public static class UpdateImportTemplate
    { 
        //设置单元格格式
        private static void SetCellStyle(ICellStyle style)
        {
            style.VerticalAlignment = VerticalAlignment.Center;//垂直对齐
            style.Alignment = HorizontalAlignment.Left;//水平对齐            
            style.WrapText = false;//换行 
        }
       
        public static IWorkbook UpdateBatchImportTemplate(string templateName)
        {
            var templateFile = @"C:\\demo\\导入模板.xls"; 
            if (File.Exists(templateFile))
                File.Delete(templateFile); 
            HSSFWorkbook hssfWorkbook = new HSSFWorkbook();//创建工作簿
            ISheet mainSheet = hssfWorkbook.CreateSheet("Sheet1");
            var patr = mainSheet.CreateDrawingPatriarch();
            Type type = typeof(ImportClassInfo);
            List<PropertyInfo> propList = type.GetProperties().Where(c => c.IsDefined(typeof(ImportedColumnAttribute), true)).ToList();
            #region Cell Format
            var commonStyle = hssfWorkbook.CreateCellStyle();
            SetCellStyle(commonStyle); 
            #endregion
            IRow titleRow = mainSheet.CreateRow(0);
            titleRow.Height = 100 * 4;

            #region 获取下拉项
            var dictOptions = new Dictionary<string, string>();
            dictOptions.Add("Name", "张1111");
            dictOptions.Add("CreateTime", DateTime.Now.ToString());
            #endregion

            //表头
            for (int i = 0; i < propList.Count(); i++)
            {
                mainSheet.SetColumnWidth(i, 18 * 256);//设置列宽
 
                ImportedColumnAttribute propertyAttribute = propList[i].GetCustomAttribute<ImportedColumnAttribute>();
                ICell cell = titleRow.CreateCell(i);
                #region 设置描述          
                if (!string.IsNullOrEmpty(propertyAttribute.Comments))
                {
                    var comment1 = patr.CreateCellComment(new HSSFClientAnchor(0, 0, 0, 0, 1, 0, i + 2, 5));
                    comment1.String = new HSSFRichTextString(propertyAttribute.Comments);
                    comment1.Author = "Admin";
                    cell.CellComment = comment1;
                }
                #endregion                 
                cell.SetCellValue(propertyAttribute.Caption);
                cell.CellStyle = commonStyle;
                if (dictOptions.ContainsKey(propList[i].Name)) //下拉项
                {       
                    var options = new string[] { "张三", "李四", "王五" };//创建一个数组作为数据源
                    CreateDropdownlist(hssfWorkbook, 1, i, options);
                }
            }             
            hssfWorkbook.SetSheetHidden(1, SheetState.Hidden);
            return hssfWorkbook;
        }

        /// <summary>
        /// 创建表头下拉选项
        /// </summary>
        /// <param name="WorkBook"></param>
        /// <param name="firstRow"></param>
        /// <param name="col"></param>
        /// <param name="strarray"></param>
        private static void CreateDropdownlist(IWorkbook WorkBook, int firstRow, int col, string[] strarray)
        {
            string optSheetName = "可选项";
            ISheet sheet = WorkBook.CreateSheet(optSheetName);//先创建一个Sheet专门用于存储下拉项的值
            WorkBook.SetSheetHidden(WorkBook.GetSheetIndex(optSheetName), SheetState.Hidden);//隐藏Sheet
            int index = 0;
            foreach (var str in strarray)//将下拉列表中的数据循环赋给sheet页
            {
                sheet.CreateRow(index++).CreateCell(0).SetCellValue(str);
            }
            //定义一个名称，指向刚才创建的下拉项的区域：
　　         var rangeName = optSheetName + "Range";
            IName range = WorkBook.CreateName();
            range.RefersToFormula = optSheetName + "!$A$1:$A$" + (index == 0 ? 1 : index);
            range.NameName = rangeName;
            //-----------------分割线以上是用来创建一个sheet页赋值，然后将sheet页中的内容定义成一个名称，后面用来当作数组传入方法的过程-----------------------

            CellRangeAddressList cellRegions = new CellRangeAddressList(firstRow, 65535, col, col);//划一块地，这块地我要留着种黄瓜

            DVConstraint constraint = DVConstraint.CreateFormulaListConstraint(rangeName);//弄来了二斤瓜苗（补充一下：这里如果传进来的是一堆瓜苗就直接用了，如果是传回来的是一个字符串，比如说“翠花家”，我就去翠花找黄瓜去了）

            HSSFDataValidation dataValidate = new HSSFDataValidation(cellRegions, constraint);//把瓜苗种地里

            dataValidate.CreateErrorBox("输入不合法", "请输入或选择下拉列表中的值。");//提示你挖错了，我这是黄瓜地，茄子在隔壁翠花的地里
            dataValidate.ShowPromptBox = true;

            WorkBook.GetSheetAt(0).AddValidationData(dataValidate);//雷猴，终于种完地了，回家睡觉
        }
    }
}
