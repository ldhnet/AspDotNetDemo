using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace TestDBConsole
{
    public class InsertTest
    {
        const string _conString = "Data Source=.;Initial Catalog=LdhTest;user id=sa;password=root";
        public int Add()
        {
            try
            {
                TransactionOptions tOpt = new TransactionOptions();
                //设置TransactionOptions模式
                tOpt.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                // 设置超时间隔为2分钟，默认为60秒
                tOpt.Timeout = new TimeSpan(0, 2, 0);
                //------
                using (TransactionScope tsCope = new TransactionScope(TransactionScopeOption.Required, tOpt))
                {
                    Add_2();
                    Add_3(); 
                }

                using (TransactionScope tsCope = new TransactionScope(TransactionScopeOption.Required, tOpt))
                {
                    Add_2();
                    Add_3();
                    tsCope.Complete();
                }
                //------
                using (TransactionScope tsCope = new TransactionScope(TransactionScopeOption.RequiresNew, tOpt))
                {
                    Add_2();
                    Add_3(); 
                }
                using (TransactionScope tsCope = new TransactionScope(TransactionScopeOption.RequiresNew, tOpt))
                {
                    Add_2();
                    Add_3();
                    tsCope.Complete();
                }
                //------
                using (TransactionScope tsCope = new TransactionScope(TransactionScopeOption.Suppress, tOpt))
                {
                    Add_2();
                    Add_3(); 
                }
                using (TransactionScope tsCope = new TransactionScope(TransactionScopeOption.Suppress, tOpt))
                {
                    Add_2();
                    Add_3();
                    tsCope.Complete();
                }
                using (TransactionScope ts = new TransactionScope())
                {
                    SqlDatabase db = new SqlDatabase(_conString);
                    int i = 0;
                    using (SqlCommand cmd = new SqlCommand("insert into dbo.Table_1(Massage,CreateTime) values (@Massage,@CreateTime)"))
                    {
                        cmd.CommandType = CommandType.Text;
                        Random _Index = new Random();

                        cmd.Parameters.AddWithValue("@Massage", "1_" + _Index.Next());
                        cmd.Parameters.AddWithValue("@CreateTime", DateTime.Now);
                        i = i + db.ExecuteNonQuery(cmd);

                        i = i + Add_2();
                    }
                    throw new ArgumentException("事务提交失败");
                    ts.Complete();
                    return i;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }

        }


        public int Add_2()
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    SqlDatabase db = new SqlDatabase(_conString);
                    int i = 0;
                    using (SqlCommand cmd = new SqlCommand("insert into dbo.Table_1(Massage,CreateTime) values (@Massage,@CreateTime)"))
                    {
                        cmd.CommandType = CommandType.Text;
                        Random _Index = new Random();

                        cmd.Parameters.AddWithValue("@Massage", "2_" + _Index.Next());
                        cmd.Parameters.AddWithValue("@CreateTime", DateTime.Now);
                        i = i + db.ExecuteNonQuery(cmd);
                    }
                    throw new ArgumentException("事务提交失败");
                    ts.Complete();
                    return i;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }

        }

        public int Add_3()
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    SqlDatabase db = new SqlDatabase(_conString);
                    int i = 0;
                    using (SqlCommand cmd = new SqlCommand("insert into dbo.Table_1(Massage,CreateTime) values (@Massage,@CreateTime)"))
                    {
                        cmd.CommandType = CommandType.Text;
                        Random _Index = new Random();

                        cmd.Parameters.AddWithValue("@Massage", "3_" + _Index.Next());
                        cmd.Parameters.AddWithValue("@CreateTime", DateTime.Now);
                        i = i + db.ExecuteNonQuery(cmd);
                    }
                    //throw new ArgumentException("事务提交失败");
                    ts.Complete();
                    return i;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }

        }

    }
}
