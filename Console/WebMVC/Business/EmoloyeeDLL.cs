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
        public TData<Employee> Find(string employeeSerialNumber)
        {
            TData<Employee> obj = new TData<Employee>();
            obj.Data = userService.Find(employeeSerialNumber);
            obj.Tag = 1;
            return obj;
        }
    }
}
