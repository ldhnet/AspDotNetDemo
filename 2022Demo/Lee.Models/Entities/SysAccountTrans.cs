namespace Lee.Models.Entities
{
    public partial class SysAccountTrans
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
