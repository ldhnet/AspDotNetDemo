using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Rule
{
    public class TestStatic
    {
        public static string GetStaticString()
        {
            return "序列化保持原有大小写（默认首字母小写）";
        }
        public static void TestGof()
        {



            AbstractAuditor director = new Director()
            {
                Name = "小张"
            };
            AbstractAuditor manager = new Manager()
            {
                Name = "小刘"
            };
            AbstractAuditor ceo = new CEO()
            {
                Name = "小李"
            };
            ApplyContext apply = new ApplyContext()
            {
                Hour = 18,
                AuditResult = false,
            };
            director.SetNextAuditor(manager);
            manager.SetNextAuditor(ceo);
            director.Audit(apply);
        }
    }
}
