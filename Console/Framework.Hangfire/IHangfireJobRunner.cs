namespace Framework.Hangfire
{
    /// <summary>
    /// Hangfire作业运行器
    /// </summary>
    public interface IHangfireJobRunner
    {
        /// <summary>
        /// 开始运行
        /// </summary>
        void Start();
    }
}