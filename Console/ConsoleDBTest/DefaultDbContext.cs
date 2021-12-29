using ConsoleDBTest.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DataEncryption;
using Microsoft.EntityFrameworkCore.DataEncryption.Providers;
using System.Text;

namespace ConsoleDBTest
{
    public class DefaultDbContext : DbContext
    {

        //private static string ConnectionString = ConfigurationManager.AppSettings.Get("ConnectionStringName") ?? "default";
        private static string connstring = "Data Source=.;Initial Catalog=DHDatabase;user id=sa;password=2021@ldh";


        // AES key randomly generated at each run. 每次运行生成随机kay
        //byte[] encryptionKey = AesProvider.GenerateKey(AesKeySize.AES256Bits).Key;

        private readonly byte[] _encryptionKey = Encoding.Default.GetBytes("4A7D1ED414474E4033AC29CCB8653D9B");
        private readonly byte[] _encryptionIV = Encoding.Default.GetBytes("0019DA6F1F30D07C51EBC5FCA1AC7DA6".Substring(0, 16));

        private readonly IEncryptionProvider _provider;

        public DefaultDbContext()
        {
            //this._provider = new AesProvider(this._encryptionKey);
            this._provider = new AesProvider(this._encryptionKey, this._encryptionIV);
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeExtend> EmployeeExtends { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connstring);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseEncryption(this._provider);
            modelBuilder.Entity<Employee>().ToTable("Employee").HasKey(c => c.Id);

            modelBuilder.Entity<EmployeeExtend>(entity =>
            {
                entity.ToTable("EmployeeExtend");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.HasOne(p => p.Employee).WithOne(c => c.EmployeeExtend).HasForeignKey<EmployeeExtend>(p => p.EmployeeId).IsRequired().OnDelete(deleteBehavior: DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<EmployeeLogin>(entity =>
            {
                entity.ToTable("EmployeeLogin");
                entity.HasKey(e => e.Id);
                entity.HasOne(c => c.Employee).WithMany(p => p.EmployeeLogins).HasForeignKey(p => p.EmployeeId).IsRequired().OnDelete(deleteBehavior: DeleteBehavior.Cascade);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
