using Framework.Utility.Attributes;

namespace DH.Models.Entities
{
    public  class SysAccount
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
        [LengthValidate(6,6)]
        public string AccountNo { get; set; }
        /// <summary>
        /// 账号名称
        /// </summary>
        [RequireValidate]
        public string AccountName { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [RequireValidate]
        public string CreateBy { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
