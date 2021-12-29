 
namespace Framework.Utility
{
    /// <summary>
    /// 响应实体类
    /// </summary>
    public class ResultModel
    { 
        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="data">返回数据</param>
        /// <returns></returns>
        public static TResponse Ok(object data)
        {
            return new TResponse<object> { Data = data, Message = null, HttpCode = 200,  ReturnCode = 1 };
        }
        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="str">错误信息</param>
        /// <param name="code">状态码</param>
        /// <returns></returns>
        public static TResponse Error(string str, int code)
        {
            return new TResponse<object> { Data = null, Message = str, HttpCode = code, ReturnCode = 0 };
        }
    }

    /// <summary>
    /// 响应实体类
    /// </summary>
    public class ResultModel2
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int ReturnCode { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="data">返回数据</param>
        /// <returns></returns>
        public static ResultModel2 Ok(object data)
        {
            return new ResultModel2 { Data = data, ErrorMessage = null, IsSuccess = true, ReturnCode = 200 };
        }
        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="str">错误信息</param>
        /// <param name="code">状态码</param>
        /// <returns></returns>
        public static ResultModel2 Error(string str, int code)
        {
            return new ResultModel2 { Data = null, ErrorMessage = str, IsSuccess = false, ReturnCode = code };
        }
    }

}
