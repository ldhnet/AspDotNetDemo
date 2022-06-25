using Lee.Models.Entities;
using Lee.Utility.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lee.EF.ModelConfigurations
{
    internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> entity)
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Employee");

            entity.Property(e => e.Name).IsRequired().HasColumnType("varchar(50)");
            entity.Property(e => e.BankCard).IsRequired().HasColumnType("varchar(50)");
            entity.Property(e => e.EmployeeName).HasColumnType("varchar(50)");
            entity.Property(e => e.EmployeeSerialNumber).HasColumnType("varchar(50)").HasConversion(c=> SecurityHelper.Encrypt(c),c=> SecurityHelper.Decrypt(c));
            entity.Property(e => e.Department).HasColumnType("int");
            entity.Property(e => e.EmployeeStatus).HasColumnType("int"); 
            entity.Property(e => e.Phone).HasColumnType("varchar(50)");
            entity.Property(e => e.WebToken).HasColumnType("varchar(50)");
            entity.Property(e => e.ApiToken).HasColumnType("varchar(50)");
            entity.Property(e => e.ExpirationDateUtc).HasColumnType("datetime"); 

            //.HasConversion(
            //                    d => d.ToDateTime(TimeOnly.MinValue),
            //                    d => DateOnly.FromDateTime(d))
        }
    } 
}
