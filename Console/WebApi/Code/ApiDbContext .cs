using Microsoft.EntityFrameworkCore; 
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks; 

namespace WebApi.Code
{
    public class ApiDbContext : DbContext
    {  
        public ApiDbContext()  : this(GetOptions())
        { }
         
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        { 
        
        }
        private static DbContextOptions<ApiDbContext> GetOptions()
        { 
            return new DbContextOptionsBuilder<ApiDbContext>().UseInMemoryDatabase(databaseName: "MyInMemoryDatabase").Options;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            //optionsBuilder.UseSqlServer(_connectionString);
        }
        public DbSet<Employee> Employees { get; set; } 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("Employee");  
            
            base.OnModelCreating(modelBuilder);
        }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BankCard { get; set; }
    }
}
