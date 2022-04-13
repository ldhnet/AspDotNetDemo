using System;
using System.Collections.Generic; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lee.Utility.Config;
using Microsoft.EntityFrameworkCore;

namespace Lee.EF.Context
{
    public partial class MyDBContext : DbContext
    {
        private readonly string _connectionString = GlobalConfig.SystemConfig.DBConnectionString;
        public MyDBContext() { }
        //public DbSet<Employee> Employee { get; set; }
        //public DbSet<EmployeeDetail> EmployeeDetail { get; set; }
        //public DbSet<EmployeeLogin> EmployeeLogin { get; set; }
        //public DbSet<SysAccount> SysAccount { get; set; }
        //public DbSet<SysAccountTrans> SysAccountTrans { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
            //optionsBuilder.UseLazyLoadingProxies();//启用懒加载
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyDBContext).Assembly);
             
            base.OnModelCreating(modelBuilder);
        }
    }
}
