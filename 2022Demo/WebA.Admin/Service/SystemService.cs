using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebA.Admin.Contracts;

namespace WebA.Admin.Service
{
    public class SystemService:ISystemContract
    {
        public DateTime GetSystemDate()
        { 
            return new DateTime(2021,12,31);
        }
    }
}
