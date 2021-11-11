using System;
using System.Collections.Generic;

namespace DH.Models.DbModels
{
    public partial class SysAccountTrans
    {
        public string Id { get; set; } 
        public string UserId { get; set; }  
        public string CreateBy { get; set; }
        public long? CreateTime { get; set; } 
    }
}
