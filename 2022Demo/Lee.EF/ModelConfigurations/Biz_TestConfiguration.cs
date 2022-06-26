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
    internal class Biz_TestConfiguration : IEntityTypeConfiguration<Biz_Test>
    {
        public void Configure(EntityTypeBuilder<Biz_Test> entity)
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Biz_Test");
            entity.Property(e => e.Name).IsRequired().HasColumnType("varchar(50)"); 
            entity.Property(e => e.Phone).HasColumnType("varchar(20)").HasConversion(c=> SecurityHelper.Encrypt(c),c=> SecurityHelper.Decrypt(c)); 
            entity.Property(e => e.BirthDate).HasColumnType("date");
            entity.Property(e => e.CreateTime).HasColumnType("datetime");
            entity.Property(e => e.UpdateTime).HasColumnType("datetime");
            //.HasConversion(
            //                    d => d.ToDateTime(TimeOnly.MinValue),
            //                    d => DateOnly.FromDateTime(d))
        }
    } 
}
