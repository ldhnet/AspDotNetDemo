using Lee.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lee.EF.ModelConfigurations
{
    internal class EmployeeDetailConfiguration : IEntityTypeConfiguration<EmployeeDetail>
    {
        public void Configure(EntityTypeBuilder<EmployeeDetail> entity)
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("EmployeeDetail");

            entity.Property(e => e.EmployeeId).IsRequired().HasColumnType("int");
            entity.Property(e => e.EnglishName).HasColumnType("varchar(50)");
            entity.Property(e => e.CreateTime).HasColumnType("datetime");

            entity.HasOne(ud => ud.Employee).WithOne(u => u.EmployeeDetail).HasForeignKey<EmployeeDetail>(ud => ud.EmployeeId).IsRequired();
        }
    } 
}
