using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Utility.Data
{
    /// <summary>
    /// 业务操作结果信息类，对操作结果进行封装
    /// </summary>
    public class BaseOutputDto : BaseOutputDto<object>
    {
        public BaseOutputDto(bool paramSuccess)
    : base(paramSuccess, null, null)
        {
        }
        public BaseOutputDto(bool paramSuccess, string paramMsg)
            : base(paramSuccess, paramMsg, null)
        {
        }
        /// <summary>
        /// 成功操作
        /// </summary>
        public static BaseOutputDto Sucess { get; } = new BaseOutputDto(true);
    }
    /// <summary>
    /// 泛型业务操作结果信息类，一般用于查询结果
    /// </summary>
    public class BaseOutputDto<T>
    {
        public BaseOutputDto(T paramData)
            : this(paramData!=null, null, new CommonItemOutputDto<T> { item = paramData })
        {
        }
        public BaseOutputDto(T paramData,string message)
    : this(paramData != null, message, new CommonItemOutputDto<T> { item = paramData })
        {
        }
        public BaseOutputDto(T paramData, int total)
            : this(true, null, new CommonListOutputDto<T>
            {
                list = paramData,
                total = total
            })
        {
        }
        public BaseOutputDto(bool paramSucess, string paramMsg, object paraData)
        {
            data = paraData;
            success = paramSucess;
            msg = paramMsg;
        }
        public object data { get; set; }
        /// <summary>
        /// 是否执行成功
        /// </summary>
        public bool success { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string msg { get; set; }
    }
    public class CommonItemOutputDto<T>
    {
        public T item { get; set; }
    }
    public class CommonListOutputDto<T>
    {
        public T list { get; set; }
        public int total { get; set; }
    }
}
