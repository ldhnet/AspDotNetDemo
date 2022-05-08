using Lee.Cache;
using Lee.Utility.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebA.Admin.Contracts;
using WebA.Admin.Service;

namespace WebA.Admin
{
    public class ServiceContext: IDependency
    {
        private readonly ISystemContract _systemContract;
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
