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
    internal class SysAccountConfiguration : IEntityTypeConfiguration<SysAccount>
    {
        public void Configure(EntityTypeBuilder<SysAccount> entity)
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("SysAccount");

            entity.HasIndex(e => e.UserId).HasDatabaseName("UserId").IsUnique();

            entity.Property(e => e.UserId).IsRequired().HasColumnType("varchar(32)");

            entity.Property(e => e.AccountName).IsRequired().HasColumnType("varchar(100)");

            entity.Property(e => e.AccountNo).IsRequired().HasColumnType("varchar(32)");

            entity.Property(e => e.CreateBy).HasColumnType("varchar(50)");

            entity.Property(e => e.CreateTime).IsRequired().HasColumnType("datetime");
        }
    } 
}
