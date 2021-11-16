using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ConsoleDBTest
{
    public class AdoInsertTest
    {
        const string connstring = "Data Source=.;Initial Catalog=DHDatabase;user id=sa;password=2021@ldh";
        public int GetData()
        {
            try
            {

                //using 数据库链接后会自动释放（关闭）
                using (SqlConnection conn = new SqlConnection(connstring))
                {
                    conn.Open();

                    //SqlDataAdapter
                    SqlDataAdapter da = new SqlDataAdapter("select * from Employee", conn);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Employee");
                    var dt = ds.Tables["Employee"];
                    var rest2 = ConvertDataTableToGenericList2<Employee>(dt);

                    var rest3 = dt.ConvertDataTableToList<Employee>();

                    var rest4 = rest3.ConvertToDataTable();

                    var rest5 = rest4.Rows.Count;

                    //SqlDataReader
                    SqlCommand cmd = new SqlCommand("select * from Employee", conn);
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader num = cmd.ExecuteReader();

                    return dt.Rows.Count;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }

        }



        /// <summary>
        /// DataTable 转成 IEnumerable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> ConvertDataTableToGenericList2<T>(DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>()
                   .Select(c => c.ColumnName)
                   .ToList();

            var properties = typeof(T).GetProperties();
            DataRow[] rows = dt.Select();
            return rows.Select(row =>
            {
                var objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name))
                        pro.SetValue(objT, row[pro.Name]);
                }
                return objT;
            }).ToList();
        }


        public int Add()
        {
            try
            {
                int num = 0;
                //using 数据库链接后会自动释放（关闭）
                using (SqlConnection conn = new SqlConnection(connstring))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("insert into dbo.Employee(Name,BankCard) values (@Name,@BandCard)", conn);
                    cmd.CommandType = CommandType.Text;
                    Random _Index = new Random();

                    cmd.Parameters.AddWithValue("@Name", "李_" + _Index.Next());
                    cmd.Parameters.AddWithValue("@BandCard", "6227_" + _Index.Next(1000000, 9000000));
                    num = cmd.ExecuteNonQuery();
                }
                return num;

                //using (TransactionScope ts = new TransactionScope())
                //{
                //    SqlConnection conn = new SqlConnection(connstring);
                //    conn.Open();
                //    SqlCommand cmd = new SqlCommand("insert into dbo.Table_1(Massage,CreateTime) values (@Massage,@CreateTime)", conn);
                //    int num = (int)cmd.ExecuteScalar();
                //    conn.Close();

                //    //throw new ArgumentException("事务提交失败");
                //    ts.Complete(); 
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }

        }


        //public int Add_2()
        //{
        //    try
        //    {
        //        using (TransactionScope ts = new TransactionScope())
        //        {
        //            SqlDatabase db = new SqlDatabase(_conString);
        //            int i = 0;
        //            using (SqlCommand cmd = new SqlCommand("insert into dbo.Table_1(Massage,CreateTime) values (@Massage,@CreateTime)"))
        //            {
        //                cmd.CommandType = CommandType.Text;
        //                Random _Index = new Random();

        //                cmd.Parameters.AddWithValue("@Massage", "2_" + _Index.Next());
        //                cmd.Parameters.AddWithValue("@CreateTime", DateTime.Now);
        //                i = i + db.ExecuteNonQuery(cmd);
        //            }
        //            throw new ArgumentException("事务提交失败");
        //            ts.Complete();
        //            return i;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return 0;
        //    }

        //}


    }
}
