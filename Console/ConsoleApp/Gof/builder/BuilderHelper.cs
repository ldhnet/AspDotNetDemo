using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Gof.builder
{
    public class  BuilderHelper
    {
        public static IPersonBuilder CreatePersonBuilder
        {
            get { return new PersonBuilder(); }
        }
    }

    public class SystemTime
    {
        public static DateTime CurrentTime
        {
            get { return DateTime.UtcNow.AddHours(8); }
        }
    }
}
