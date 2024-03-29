﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utility
{
    /// <summary>
    /// 数据传输对象
    /// </summary>
    public class TResponse
    { 
        /// <summary>
        /// 操作结果，Code，0代表失败，1代表成功
        /// </summary>
        public int Success { get; set; }
        /// <summary>
        /// 提示信息或异常信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 其他的验证返回结果，可根据需要设置
        /// </summary>
        public int Code { get; set; }
    }
    public class TResponse<T> : TResponse
    {
        /// <summary>
        /// 列表的记录数
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }
    }
}
