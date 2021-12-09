using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Framework.Utility.Helper
{
    public static class MoneyHelper
    {
        //把小写金额转成大写
        public static string ConvertToChinese(this decimal number)
        {
            var s = number.ToString("#L#E#D#C#K#E#D#C#J#E#D#C#I#E#D#C#H#E#D#C#G#E#D#C#F#E#D#C#.0B0A");
            var d = Regex.Replace(s, @"((?<=-|^)[^1-9]*)|((?'z'0)[0A-E]*((?=[1-9])|(?'-z'(?=[F-L\.]|$))))|((?'b'[F-L])(?'z'0)[0A-L]*((?=[1-9])|(?'-z'(?=[\.]|$))))", "${b}${z}");
            var r = Regex.Replace(d, ".", m => "负元空零壹贰叁肆伍陆柒捌玖空空空空空空空分角拾佰仟万亿兆京垓秭穰"[m.Value[0] - '-'].ToString());
            return r + "整";
        }
        //把小写金额转成大写
        public static string ConvertToChinese(this string numstr)
        {
            if (string.IsNullOrEmpty(numstr)) return "";
            try
            {
                decimal num = Convert.ToDecimal(numstr);
                return ConvertToChinese(num);
            }
            catch
            {
                return "非数字形式！";
            }
        }
    }
}
