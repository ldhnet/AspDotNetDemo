using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DataEncryption.Providers;
using System;
using System.Diagnostics;
using System.Linq;
using System.Security;

namespace AesSample
{
    static class Program
    {
        static void Main()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>().UseInMemoryDatabase(databaseName: "MyInMemoryDatabase").Options;

            //var connstring = "Data Source=.;Initial Catalog=DHDatabase;user id=sa;password=2021@ldh";
            //var options = new DbContextOptionsBuilder<DatabaseContext>().UseSqlServer(connstring).Options;

            Debug.Assert(options is  null,"123456789");
             
            using var context = new DatabaseContext(options);

              
            var list1 = context.Users.ToList();

            var user = new UserEntity
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "John",
                LastName = "Doe",
                Email = "john@doe.com",
                Password = "123",
            };

            context.Users.Add(user);
            context.SaveChanges();

            var list = context.Users.ToList();


            Console.WriteLine($"Users count: {context.Users.Count()}");

            user = context.Users.First();

            Console.WriteLine($"User: {user.FirstName} {user.LastName} - {user.Email} ({user.Password.Length})");
        }

        static SecureString BuildPassword()
        {
            SecureString result = new();
            result.AppendChar('L');
            result.AppendChar('e');
            result.AppendChar('t');
            result.AppendChar('M');
            result.AppendChar('e');
            result.AppendChar('I');
            result.AppendChar('n');
            result.AppendChar('!');
            result.MakeReadOnly();
            return result;
        }
    }
}
