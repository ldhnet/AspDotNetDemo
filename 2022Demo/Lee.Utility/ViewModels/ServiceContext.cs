using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebA.Admin.Service;

namespace Lee.Utility.ViewModels
{
    public class ServiceContext
    {
        public int SubsidiaryID { get; set; } = 2022;

        public DateTime CurrentProcessingMonth { get; set; } = GetSystemDate();

        private static DateTime GetSystemDate()
        {
            SystemService service = new SystemService();
            return service.GetSystemDate();
        }
    }

 
}
