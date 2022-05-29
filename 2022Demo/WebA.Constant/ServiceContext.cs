
using Lee.Utility.Dependency;
using WebA.Admin.Contracts;

namespace WebA.Constant
{
    public class ServiceContext: IDependency
    {
        private readonly ISystemContract _systemContract;
        public ServiceContext() {  }
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
