using Microsoft.EntityFrameworkCore;

namespace WebApi.Code
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext() : this(GetOptions())
        { }

        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }
        private static DbContextOptions<ApiDbContext> GetOptions()
        {
            return new DbContextOptionsBuilder<ApiDbContext>().UseInMemoryDatabase(databaseName: "MyInMemoryDatabase").Options;
        }


        //private static string connstring = "Data Source=.;Initial Catalog=DHDatabase;user id=sa;password=2021@ldh";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(connstring);
        }
        public DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("Employee");

            base.OnModelCreating(modelBuilder);
        }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BankCard { get; set; }
    }
}
