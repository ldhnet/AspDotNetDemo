using Framework.EFCore.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Framework.EFCore
{
    public class DefaultDbContext : DbContext
    {  
        public DefaultDbContext()
        {
        }
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options)
        { 
        }
        public DbSet<UserEntity> UserEntitys { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            { 
                var connection = "server=rm-2zeetsz84h2ex0760ho.mysql.rds.aliyuncs.com;userid=root;pwd=***;port=3306;database=ldhdb;sslmode=none;Convert Zero Datetime=True";

                optionsBuilder.UseMySql(connection, ServerVersion.Create(8, 0, 18, ServerType.MySql));
            }

          
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.ToTable("UserEntity");
                entity.HasKey(e => e.Id); 
            }); 
            base.OnModelCreating(modelBuilder);
        }
    }
}