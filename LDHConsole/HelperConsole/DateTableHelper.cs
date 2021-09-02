using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperConsole
{
    public class DateTableHelper
    {
        public void DataTableCompute()
        {
            DataTable dtable = new DataTable();

            var veal_1 = dtable.Compute("1+1", null);
            Console.WriteLine(veal_1);

            var veal_2 = dtable.Compute("1+1", "false");
            Console.WriteLine(veal_2);

            var veal_3 = dtable.Compute("1+1", "true");
            Console.WriteLine(veal_3);
        }

    }
}
