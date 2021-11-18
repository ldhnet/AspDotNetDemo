using Framework.Utility.Config;
using Microsoft.EntityFrameworkCore;

namespace DH.Models.DbModels
{
    public partial class MyDBContext : DbContext
    {
        private readonly string _connectionString = GlobalConfig.SystemConfig.DBConnectionString;
        public MyDBContext()
        {

        }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<EmployeeDetail> EmployeeDetail { get; set; }
        public DbSet<EmployeeLogin> EmployeeLogin { get; set; }
        public DbSet<SysAccount> SysAccount { get; set; }
        public DbSet<SysAccountTrans> SysAccountTrans { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
            optionsBuilder.UseLazyLoadingProxies();//启用懒加载
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("Employee");
                  
                entity.Property(e => e.Name).IsRequired().HasColumnType("varchar(50)"); 
                entity.Property(e => e.BankCard).IsRequired().HasColumnType("varchar(50)"); 
                entity.Property(e => e.EmployeeName).HasColumnType("varchar(50)"); 
                entity.Property(e => e.EmployeeSerialNumber).HasColumnType("varchar(50)"); 
                entity.Property(e => e.Department).HasColumnType("int"); 
                entity.Property(e => e.Phone).HasColumnType("varchar(50)");
                entity.Property(e => e.WebToken).HasColumnType("varchar(50)");
                entity.Property(e => e.ApiToken).HasColumnType("varchar(50)"); 
                entity.Property(e => e.ExpirationDateUtc).HasColumnType("datetime"); 
            });
            modelBuilder.Entity<EmployeeDetail>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("EmployeeDetail");

                entity.Property(e => e.EmployeeId).IsRequired().HasColumnType("int");
                entity.Property(e => e.EnglishName).HasColumnType("varchar(50)");
                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.HasOne(ud => ud.Employee).WithOne(u => u.EmployeeDetail).HasForeignKey<EmployeeDetail>(ud => ud.EmployeeId).IsRequired();
                 
            });

            modelBuilder.Entity<EmployeeLogin>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY"); 
                entity.ToTable("EmployeeLogin"); 
                entity.Property(e => e.EmployeeId).IsRequired().HasColumnType("int"); 
                entity.Property(e => e.CreateTime).HasColumnType("datetime");                 
                entity.HasOne(ul => ul.Employee).WithMany(u => u.EmployeeLogins).HasForeignKey(ul => ul.EmployeeId).IsRequired();
            }); 

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
