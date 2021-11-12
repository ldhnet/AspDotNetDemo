using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Utility.Helper
{
    /// <summary>
    /// 字母表
    /// </summary>
    public static class AlphabetHelper
    {
        private static List<string> alphabet = new List<string>();

        static AlphabetHelper()
        {
            for (char c = 'A'; c <= 'Z'; c++)
            {
                alphabet.Add(c.ToString());
            }
        }

        /// <summary>
        /// 获取Excel列的字母列号
        /// </summary>
        /// <param name="columnNum"></param>
        /// <returns></returns>
        public static string GetExcelAlphabeticTag(int columnNum)
        {
            return columnNum < 26 ? alphabet[columnNum] : alphabet[(columnNum / 26) - 1] + alphabet[columnNum % 26];
        }
    }
}
