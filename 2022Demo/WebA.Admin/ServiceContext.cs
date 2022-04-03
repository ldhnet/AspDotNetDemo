using Lee.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebA.Admin.Contracts;
using WebA.Admin.Service;

namespace WebA.Admin
{
    public class ServiceContext
    {
        private ISystemContract _systemContract;
        public ServiceContext(ISystemContract systemContract)
        {
            _systemContract = systemContract;
        }
        public int CurrentID
        {
            get
            {
                return _systemContract.GetCurrentID();
            }
            set
            {
                CurrentID = value;
            }
        }

        public DateTime CurrentMonth
        {
            get
            { 
                return _systemContract.GetCurrentMonth();
            }
            set
            {
                CurrentMonth = value;
            }
        }
    }

 
}
