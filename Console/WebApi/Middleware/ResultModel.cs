namespace WebApi.Middleware
{
    /// <summary>
    /// 响应实体类
    /// </summary>
    public class ResultModel
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
        public static ResultModel Ok(object data)
        {
            return new ResultModel { Data = data, ErrorMessage = null, IsSuccess = true, ReturnCode = 200 };
        }
        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="str">错误信息</param>
        /// <param name="code">状态码</param>
        /// <returns></returns>
        public static ResultModel Error(string str, int code)
        {
            return new ResultModel { Data = null, ErrorMessage = str, IsSuccess = false, ReturnCode = code };
        }
    }

}
