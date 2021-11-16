namespace DH.Models.DbModels
{
    public partial class SysAccount
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 账号编号
        /// </summary>
        public string AccountNo { get; set; }
        /// <summary>
        /// 账号名称
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
