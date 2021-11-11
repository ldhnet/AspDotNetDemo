using System;
using Framework.Utility.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata; 

namespace DH.Models.DbModels
{
    public partial class MyDBContext : DbContext
    {
        private readonly string _connectionString = GlobalConfig.SystemConfig.DBConnectionString;
        public MyDBContext()
        {

        }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<EmployeeLogin> EmployeeLogin { get; set; } 
        public DbSet<SysAccount> SysAccount{ get; set; }
        public DbSet<SysAccountTrans> SysAccountTrans { get; set; }

   
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
               
            }
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<EmployeeLogin>().ToTable("EmployeeLogin");


            modelBuilder.Entity<SysAccount>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("SysAccount");

                entity.HasIndex(e => e.UserId).HasDatabaseName("UserId").IsUnique();  

                entity.Property(e => e.UserId).IsRequired().HasColumnType("varchar(32)");

                entity.Property(e => e.AccountName).IsRequired().HasColumnType("varchar(100)");

                entity.Property(e => e.AccountNo).IsRequired().HasColumnType("varchar(32)"); 

                entity.Property(e => e.CreateBy).HasColumnType("varchar(50)");

                entity.Property(e => e.CreateTime).IsRequired().HasColumnType("datetime");  

            });

            modelBuilder.Entity<SysAccountTrans>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");
                entity.ToTable("SysAccountTrans");

                entity.Property(e => e.Id).HasColumnType("int");

                entity.Property(e => e.UserId).HasColumnType("varchar(32)");

                entity.Property(e => e.CreateBy).HasColumnType("varchar(50)");

                entity.Property(e => e.CreateTime).IsRequired().HasColumnType("datetime"); 

            });

            base.OnModelCreating(modelBuilder); 
        }
    }
}
