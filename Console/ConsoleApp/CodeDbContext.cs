using Microsoft.EntityFrameworkCore;

namespace ConsoleApp
{
    public class CodeDbContext : DbContext
    {
        public CodeDbContext(DbContextOptions<CodeDbContext> options) : base(options)
        {
        }
        //public DbSet<Employee> Employee { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(connstring);
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().ToTable("UserEntity");
            //modelBuilder.Entity<Employee>().ToTable("Employee"); 
            base.OnModelCreating(modelBuilder);
        }
    }
    public class UserEntity
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
    }
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BankCard { get; set; }
    }
}
