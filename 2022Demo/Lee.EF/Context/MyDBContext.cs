using System;
using System.Collections.Generic; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lee.Utility.Config;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Lee.EF.Context
{
    public partial class MyDBContext : DbContext
    {
        private  string _connectionString = GlobalConfig.SystemConfig.DBConnectionString;
        public MyDBContext() { }
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)//options: GeContexttOptions(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseLazyLoadingProxies();//启用懒加载   
            if (!optionsBuilder.IsConfigured)
            {
                //if (string.IsNullOrEmpty(_connectionString))
                //    _connectionString = "Server=localhost;Database=DH;User Id=sa;Password=2021@ldh;";
                //optionsBuilder.UseSqlServer(_connectionString);

                _connectionString = "server=rm-2zeetsz84h2ex0760ho.mysql.rds.aliyuncs.com;userid=root;pwd=***;port=3306;database=ldhdb;sslmode=none;Convert Zero Datetime=True";
                optionsBuilder.UseMySql(_connectionString, ServerVersion.Create(8, 0, 18, ServerType.MySql)); 
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyDBContext).Assembly);
             
            base.OnModelCreating(modelBuilder);
        }
    }
}
