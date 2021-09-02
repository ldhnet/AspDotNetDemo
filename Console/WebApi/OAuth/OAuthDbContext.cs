using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic; 
using System.Linq;
using System.Web; 

namespace WebApi.OAuth
{
    public class OAuthDbContext : DbContext
    {
        /// <summary>
        /// 初始化一个<see cref="OAuthDbContext"/>类型的新实例
        /// </summary>
        public OAuthDbContext()
            : this(GetConnectionStringName())
        { }

        /// <summary>
        /// 使用连接名称或连接字符串 初始化一个<see cref="CodeFirstDbContext"/>类型的新实例
        /// </summary>
        public OAuthDbContext(string nameOrConnectionString) 
        { }

        /// <summary>
        /// 获取 数据库连接串名称
        /// </summary>
        /// <returns></returns>
        private static string GetConnectionStringName()
        {
            string name = "default";
            return name;
        }

        //public DbSet<OAuth_Nonce> Nonces { get; set; }

        //public DbSet<OAuth_SymmetricCryptoKey> SymmetricCryptoKeys { get; set; }

        //public DbSet<OAuth_Client> Clients { get; set; }

        //public DbSet<OAuth_ClientAuthorization> ClientAuthorizations { get; set; }

        //public DbSet<OAuth_ClientOpenApi> ClientOpenApis { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<OAuth_Client>().HasMany(p => p.ClientAuthorizations).WithRequired(p => p.Client).HasForeignKey(p => p.ClientId);
        //}

    }
}