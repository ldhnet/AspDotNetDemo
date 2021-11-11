using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text; 
using Framework.Utility.Extensions;

namespace Framework.Utility
{
    /// <summary>
    /// 业务操作结果信息类，对操作结果进行封装
    /// </summary>
    public class BaseResponse : BaseResponse<object>
    {
        #region 构造函数

        /// <summary>
        /// 初始化一个<see cref="BaseResponse"/>类型的新实例
        /// </summary>
        public BaseResponse(successCode resultType) : this(resultType, null, null)
        { }

        /// <summary>
        /// 初始化一个<see cref="OperationResult"/>类型的新实例
        /// </summary>
        public BaseResponse(successCode resultType, string message): this(resultType, message, null)
        { }

        /// <summary>
        /// 初始化一个<see cref="BaseResponse"/>类型的新实例
        /// </summary>
        public BaseResponse(successCode resultType, string message, object data) : base(resultType, message, data)
        { }

        #endregion
    }

    public class BaseResponse<T>
    {
        /// <summary>
        /// 返回数据
        /// </summary>
        public T data { get; set; }

        /// <summary>
        /// 返回结果
        /// </summary>
        public successCode success { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 初始化一个<see cref="BaseResponse{T}"/>类型的新实例
        /// </summary>
        public BaseResponse(successCode resultType, string message, T data)
        {
            this.success = resultType;
            this.msg = message;
            this.data = data;
        }

    }

    [Description("接口详细描述代码")]
    public enum successCode : int
    {
        /// <summary>
        /// 无意义的，防止某些序列化工具在序列化时报错
        /// </summary>
        [Description("无意义的，防止某些序列化工具在序列化时报错")]
        None = 0,

        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        Success = 1,

        /// <summary>
        /// 失败
        /// </summary>
        [Description("失败")]
        Fail = 2,

        /// <summary>
        /// 未知异常
        /// </summary>
        [Description("未知异常")]
        Error = 3,

        /// <summary>
        /// 参数异常
        /// </summary>
        [Description("参数异常")]
        ParamsError = 4,

        /// <summary>
        /// 重复提交数据
        /// </summary>
        [Description("重复提交数据")]
        RepeatSubmit = 5,

        /// <summary>
        /// 配置错误
        /// </summary>3
        [Description("配置错误")]
        ConfigIsError = 6,

        /// <summary>
        /// 暂无数据
        /// </summary>
        [Description("暂无数据")]
        DataIsNull = 7,

        /// <summary>
        /// 用户已存在
        /// </summary>
        [Description("用户已存在")]
        DataAlreadyExists = 8,

        /// <summary>
        /// 没有权限
        /// </summary>
        [Description("没有权限")]
        NoPerssion = 9,


        /// <summary>
        ///   输入信息验证失败
        /// </summary>
        [Description("输入信息验证失败。")]
        ValidError = 9,

        /// <summary>
        ///   指定参数的数据不存在
        /// </summary>
        [Description("指定参数的数据不存在。")]
        QueryNull = 10,

        /// <summary>
        ///   操作取消或操作没引发任何变化
        /// </summary>
        [Description("操作没有引发任何变化，提交取消。")]
        NoChanged = 11,
         
    }
}
