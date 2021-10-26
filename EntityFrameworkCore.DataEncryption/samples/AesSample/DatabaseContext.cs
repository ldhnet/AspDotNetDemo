using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DataEncryption;
using Microsoft.EntityFrameworkCore.DataEncryption.Providers;

namespace AesSample
{
    public class DatabaseContext : DbContext
    {
        // AES key randomly generated at each run. 每次运行生成一个随机kay
        //byte[] encryptionKey = AesProvider.GenerateKey(AesKeySize.AES256Bits).Key;

        private readonly byte[] _encryptionKey = Encoding.Default.GetBytes("4A7D1ED414474E4033AC29CCB8653D9B");
        private readonly byte[] _encryptionIV = Encoding.Default.GetBytes("0019DA6F1F30D07C51EBC5FCA1AC7DA6".Substring(0, 16));
        private readonly IEncryptionProvider _encryptionProvider;

        public DbSet<UserEntity> Users { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {  
            _encryptionProvider = new AesProvider(_encryptionKey, _encryptionIV);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().ToTable("UserEntity");
            modelBuilder.UseEncryption(_encryptionProvider);           
            base.OnModelCreating(modelBuilder);
        }
    }
}
