using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DataEncryption;
using Microsoft.EntityFrameworkCore.DataEncryption.Providers;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks; 
using WebMVC.Model;

namespace WebMVC.Context
{
    public class ApplicationDbContext : DbContext
    {
        private readonly string _connectionString = GlobalContext.SystemConfig.DBConnectionString;

        //private readonly byte[] _encryptionKey = AesProvider.GenerateKey(AesKeySize.AES128Bits).Key; 
        //private readonly IEncryptionProvider _provider;

        public ApplicationDbContext()
        {
            // this._provider = new AesProvider(this._encryptionKey); 
        } 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            optionsBuilder.UseSqlServer(_connectionString);
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeLogin> EmployeeLogins { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            //modelBuilder.UseEncryption(this._provider);
            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<EmployeeLogin>().ToTable("EmployeeLogin");
            base.OnModelCreating(modelBuilder);
        }
    }
}
