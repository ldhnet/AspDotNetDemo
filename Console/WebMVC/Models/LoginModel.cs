using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using System.Xml.Linq;
namespace WebMVC.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "View_Login_LoginName")]
        public string LoginName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "View_Login_Password")]
        public string Password { get; set; }

        [Display(Name = "记住我?")]
        public bool RememberMe { get; set; }

        public string RequestIP { get; set; }
    }
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "View_Login_LoginName")]
        public string account { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "View_Login_Password")]
        public string password { get; set; }
    }

    public class OptionResult
    {
        public int resultType { get; set; }
        public string resultMsg { get; set; } 
        public object data { get; set; }
    }
}
