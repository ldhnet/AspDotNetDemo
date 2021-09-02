using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DataEncryption;
using Microsoft.EntityFrameworkCore.DataEncryption.Providers;
using System;
using System.Configuration;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.IO;
using System.Text;

namespace ConsoleDBTest
{
    public class DefaultDbContext : DbContext
    {

        //private static string ConnectionString = ConfigurationManager.AppSettings.Get("ConnectionStringName") ?? "default";
        private static string connstring = "Data Source=.;Initial Catalog=DHDatabase;user id=sa;password=2021@ldh";
 
        private readonly byte[] _encryptionKey = AesProvider.GenerateKey(AesKeySize.AES256Bits).Key; 
        private readonly IEncryptionProvider _provider;
         
        public DefaultDbContext()
        { 
            this._provider = new AesProvider(this._encryptionKey);
        }
        public DbSet<Employee> Employees { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connstring);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseEncryption(this._provider);
            modelBuilder.Entity<Employee>().ToTable("Employee");
             
            base.OnModelCreating(modelBuilder);
        }
    }
}
