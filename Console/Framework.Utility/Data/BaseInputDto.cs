using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Utility.Data
{
    /// <summary>
    /// 输入数据DTO基类
    /// </summary>
    /// <typeparam name="TKey">标识主键类型</typeparam>
    public class BaseInputDto<TKey>
    {
        public TKey id { get; set; }
    }
}
