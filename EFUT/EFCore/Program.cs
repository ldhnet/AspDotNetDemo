// See https://aka.ms/new-console-template for more information



using EFCore.Data;
using EFCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.WriteLine("Hello, World!");

var conn = "Data Source=.;Initial Catalog=DHDatabase;user id=sa;password=2021@ldh";
//var conn = "server=.;database=question;integrated security=true";
var host = new HostBuilder()
    .ConfigureServices(services =>
    {
        services.AddDbContext<MyDbContext>(options => options.UseSqlServer(conn));
    }).Build();


using (var db = host.Services.GetRequiredService<MyDbContext>())
{
   
    var list = db.Set<Employee>().Take(100).ToList();//.Include(c => new { c.EmployeeExtend, c.EmployeeLogins })
    var list2 = db.Set<EmployeeExtend>().Include(c=>c.Employee).Take(100).ToList();
    var list3 = db.Set<EmployeeLogin>().Take(100).ToList();
     

    var zhangsan = new Employee
    {
        Name = Guid.NewGuid().ToString(),
        BankCard = "123456789101112131415",
        BankCardDisplay = "123456789101112131415",
        Monery = "11.1",
        MoneryDisplay = "11.1"
    };
    zhangsan.EmployeeExtend = new EmployeeExtend
    {
        Phone = "15225074031",
    };
    var emplogin = new EmployeeLogin
    {
        LoginTime = DateTime.Now,
    };
    zhangsan.EmployeeLogins = new List<EmployeeLogin>() { emplogin };


    db.Set<Employee>().Add(zhangsan);
    await db.SaveChangesAsync();
}

using (var db = host.Services.GetRequiredService<MyDbContext>())
{
    var newQuestion = new Question
    {
        Title = "test " + DateTime.Now.ToLongDateString(),
        Owner = await db.Set<User>().FirstAsync(u => u.Id == 1)
    };

    var latestQuestion = await db.Set<Question>()
        .Where(q => q.UserId == 1).OrderByDescending(q => q.Id).FirstOrDefaultAsync();

    db.Set<Question>().Add(newQuestion);
    await db.SaveChangesAsync();

    var list=   db.Set<Question>().Include(c=>c.Owner).Take(100).ToList();
    var list2 = db.Set<User>().Take(100).ToList();
}