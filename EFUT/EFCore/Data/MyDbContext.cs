using EFCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Question>().HasOne(q => q.Owner).WithOne();

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
