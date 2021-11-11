using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebMVC.Model;

namespace DH.Models.DbModels
{
    public partial class MyDBContext : DbContext
    {  
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options: GetOptions(options))
        {

        } 
        /// <summary>
        /// 初始化数据库连接
        /// </summary>
        /// <param name="options"></param>
        /// <param name="connectionStringProvider"></param>
        /// <returns></returns>
        private static DbContextOptions<MyDBContext> GetOptions(DbContextOptions<MyDBContext> options)
        {
            var builder = new DbContextOptionsBuilder<MyDBContext>(options);
            return builder.Options;
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeLogin> EmployeeLogins { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //optionsBuilder.UseMySql("server=172.28.15.110;userid=root;pwd=Esbu@2016;port=3306;database=pointapi;sslmode=none;TreatTinyAsBoolean=false;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SysAccount>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("SysAccount");

                entity.HasIndex(e => e.UserId).HasDatabaseName("UserId").IsUnique();  

                entity.Property(e => e.UserId).IsRequired().HasColumnType("varchar(32)");

                entity.Property(e => e.AccountName).IsRequired().HasColumnType("varchar(100)");

                entity.Property(e => e.AccountNo).IsRequired().HasColumnType("varchar(32)"); 

                entity.Property(e => e.CreateBy).HasColumnType("varchar(50)");

                entity.Property(e => e.CreateTime).HasColumnType("bigint(20)");  

            });

            modelBuilder.Entity<SysAccountTrans>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");
                entity.ToTable("SysAccountTrans");

                entity.Property(e => e.Id).HasColumnType("int");

                entity.Property(e => e.UserId).HasColumnType("varchar(32)");

                entity.Property(e => e.CreateBy).HasColumnType("varchar(50)");

                entity.Property(e => e.CreateTime).HasColumnType("bigint(20)"); 

            });
             
        }
    }
}
