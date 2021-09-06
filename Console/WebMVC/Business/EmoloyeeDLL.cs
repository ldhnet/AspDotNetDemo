using NPOI.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Model;
using WebMVC.Service;

namespace WebMVC.Business
{
    public class EmoloyeeDLL
    {
        private UserService userService = new UserService();

        public Employee Find(string employeeSerialNumber)
        { 
            return userService.Find(employeeSerialNumber);
        }
    }
}
