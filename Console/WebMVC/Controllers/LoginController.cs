using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Model;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class LoginController : BaseController
    { 
        public object Index()
        { 
            return View();
        } 
    }
 
}
