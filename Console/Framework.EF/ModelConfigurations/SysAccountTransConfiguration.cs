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
    internal class SysAccountTransConfiguration : IEntityTypeConfiguration<SysAccountTrans>
    {
        public void Configure(EntityTypeBuilder<SysAccountTrans> entity)
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
            entity.ToTable("SysAccountTrans");

            entity.Property(e => e.Id).HasColumnType("int");

            entity.Property(e => e.UserId).HasColumnType("varchar(32)");

            entity.Property(e => e.CreateBy).HasColumnType("varchar(50)");

            entity.Property(e => e.CreateTime).IsRequired().HasColumnType("datetime");
        }
    } 
}
