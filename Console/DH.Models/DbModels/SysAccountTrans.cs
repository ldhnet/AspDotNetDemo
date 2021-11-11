using System;
using System.Collections.Generic;

namespace DH.Models.DbModels
{
    public partial class SysAccountTrans
    {
        public int Id { get; set; } 
        public string UserId { get; set; }  
        public string CreateBy { get; set; }
        public DateTime? CreateTime { get; set; } 
    }
}
