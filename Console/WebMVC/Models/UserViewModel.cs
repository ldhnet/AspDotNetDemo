﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WebMVC.Models
{
    public class UserViewModel
    {
        [Required]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "身份证")]
        [RegularExpression(@"^(\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$", ErrorMessage = "身份证号不合法")]
        public string IdNum { get; set; }

        public string IdCardImgName { get; set; }

        [Required]
        [Display(Name = "身份证附件")]
        [FileExtensions(Extensions = ".jpg,.png,.xlsx,.xls", ErrorMessage = "文件格式错误")]
        public IFormFile IdCardImg { get; set; }
    }
}
