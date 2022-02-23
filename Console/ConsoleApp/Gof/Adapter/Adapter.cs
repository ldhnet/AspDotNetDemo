using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Gof.Adapter
{
    public class Adapter
    {
        public void testAdapter()
        {
            IHelper helper = new RedisinhritHelper();
       
        }
     
    }
}
