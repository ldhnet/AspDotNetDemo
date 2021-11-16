namespace DH.Models.DbModels
{
    public partial class SysAccount
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
