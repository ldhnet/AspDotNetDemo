using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class SqlConnectionTest
    {
        private static void TestMain()
        {
            TestSQLServerConnectionCount();
            Console.Read();
        }

        public static void TestSQLServerConnectionCount()
        {
            try
            {
                int maxCount = 4000;
                string connectionString = "Server=localhost;Database=Test;User Id=sa;Password=2021@ldh;";
                for (int i = 1; i < maxCount; i++)
                {
                    var db = new SqlConnection(connectionString);
                    db.Open();
                    Console.WriteLine("已创建连接对象== " + i);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
