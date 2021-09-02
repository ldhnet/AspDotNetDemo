using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace TestDBConsole
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            //Console.WriteLine("测试World!");
            //Console.WriteLine("Hello World!");

            const string _constr1 = "Data Source=.;Initial Catalog=LdhTest;Integrated Security=True";
            const string _constr2 = "Data Source=.;Initial Catalog=LdhTest;user id=sa;password=root";
            const string _constr = "Server=.;Database=LdhTest;user=sa;password=root";
            //SqlDatabase db = new SqlDatabase(this.ConsultantsConnectString);

            //using (SqlConnection conn = new SqlConnection(_constr1))
            //{
            //    SqlCommand cmd = new SqlCommand("select * from Table_1", conn);
            //    cmd.CommandType = CommandType.Text;
            //    conn.Open();
            //    var list = new List<string>();
            //    using (SqlDataReader reader = cmd.ExecuteReader())
            //    {
            //        if (reader.HasRows)
            //        {
            //            while (reader.Read())
            //            {
            //                list.Add(reader[reader.GetOrdinal("Massage")].ToString());
            //                Console.WriteLine(reader[reader.GetOrdinal("Massage")].ToString());
            //            }
            //        }
            //    }
            //}
            InsertTest _test = new InsertTest(); 
            var _ret= _test.Add();




        #if !DEBUG
                    using (SqlConnection conn = new SqlConnection(_constr))
                    {
                        SqlCommand cmd = new SqlCommand("select * from Table_1", conn);
                        cmd.CommandType = CommandType.Text; 
                        conn.Open();
                        var list =new List<string>();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    list.Add(reader[reader.GetOrdinal("Massage")].ToString());
                                    Console.WriteLine(reader[reader.GetOrdinal("Massage")].ToString());   
                                }                      
                            } 
                        } 
                    }
        #endif

        }
    }
}
