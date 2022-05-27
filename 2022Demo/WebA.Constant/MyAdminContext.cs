using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebA.Admin.Contracts;
using WebA.Admin.Service;

namespace WebA.Constant
{
    public class MyAdminContext//: IDependency
    {
        //public MyAdminContext(int _currentID,DateTime _currentMonth)
        //{
        //    CurrentID = _currentID;
        //    CurrentMonth = _currentMonth;
        //}
        public int CurrentID { get; set; }

        public DateTime CurrentMonth { get; set; }
    }

 
}
