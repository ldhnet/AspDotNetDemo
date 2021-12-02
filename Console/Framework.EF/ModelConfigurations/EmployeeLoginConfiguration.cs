using DH.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.EF.ModelConfigurations
{
    internal class EmployeeLoginConfiguration : IEntityTypeConfiguration<EmployeeLogin>
    {
        public void Configure(EntityTypeBuilder<EmployeeLogin> entity)
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
            entity.ToTable("EmployeeLogin");
            entity.Property(e => e.EmployeeId).IsRequired().HasColumnType("int");
            entity.Property(e => e.CreateTime).HasColumnType("datetime");
            entity.HasOne(ul => ul.Employee).WithMany(u => u.EmployeeLogins).HasForeignKey(ul => ul.EmployeeId).IsRequired();
        }
    } 
}
