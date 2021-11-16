namespace Framework.Utility.Models
{
    public class FrameConstant
    {
        public struct StringFormat
        {
            /// <summary>
            /// 日期和时间格式
            /// </summary>
            public const string DateTime = "yyyy-MM-dd HH:mm:ss";
            /// <summary>
            /// 日期格式
            /// </summary>
            public const string Date = "yyyy-MM-dd";
        }
        /// <summary>
        /// 查询日期中结束日期值处理(+1天)
        /// 一天换算为毫秒值
        /// </summary>
        public const int OneDayToMillisecond = 24 * 60 * 60 * 1000;

    }
    /// <summary>
    /// 数据排序
    /// </summary>
    public enum ListSortDirection
    {
        /// <summary>
        /// 升序
        /// </summary>
        Ascending,
        /// <summary>
        /// 降序
        /// </summary>
        Descending
    }
    /// <summary>
    /// 查询数据行状态
    /// </summary>
    public enum FilterDataStatus
    {
        /// <summary>
        /// 显示所有
        /// </summary>
        all,
        /// <summary>
        /// 活跃的数据
        /// </summary>
        active,
        /// <summary>
        /// 禁用的数据
        /// </summary>
        inactive
    }

    /// <summary>
    /// 用户类型
    /// </summary>
    public enum UserType
    {
        /// <summary>
        /// 系统
        /// </summary>
        System = 0,
        /// <summary>
        /// 管理者
        /// </summary>
        Management = 1,
        /// <summary>
        /// 学员
        /// </summary>
        Trainee = 2
    }

    /// <summary>
    /// 会话键值
    /// </summary>
    public struct SessionKey
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public const string UserId = "UserId";
        /// <summary>
        /// 登录用户信息键值
        /// </summary>
        public const string UserLogInfo = "UserLogInfo";
        /// <summary>
        /// 登录用户会话键值
        /// </summary>
        public const string UserPermissions = "UserPermissions";
    }
}
