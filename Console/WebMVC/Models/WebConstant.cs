using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models
{
    public class WebConstant
    {
        /// <summary>
        /// 会话键值
        /// </summary>
        public struct SessionKey
        {
            /// <summary>
            /// 登录用户会话键值
            /// </summary>
            public const string UserCacheModel = "UserCacheModel"; 
            /// <summary>
            /// 需要重置密码的用户ID
            /// </summary>
            public const string ResetPasswordEmployeeId = "TempResetPasswordEmployeeId"; 
        }
        /// <summary>
        /// 文件夹键值
        /// </summary>
        public struct FolderName
        {
            /// <summary>
            /// 模板生成信息
            /// </summary> 
            public const string ReportFolder = "~/Export";

            public const string TempFolder = "~/Temp";
            public const string Template = "~/Template";
        }
    }

   
}
