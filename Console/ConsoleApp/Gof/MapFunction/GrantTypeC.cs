using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Gof.MapFunction
{

    public class GrantTypeC
    {

        private QueryGrantTypeService queryGrantTypeService=new QueryGrantTypeService(); 
        public string test(string resourceName)
        {
            return queryGrantTypeService.getResult(resourceName);
        }
    }
}
