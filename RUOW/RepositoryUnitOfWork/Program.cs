// See https://aka.ms/new-console-template for more information
using Framework.EFCore;
using Framework.EFCore.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

Console.WriteLine("Hello, World!");


var options = new DbContextOptionsBuilder<DefaultDbContext>().UseInMemoryDatabase(databaseName: "MyInMemoryDatabase").Options;

//Debug.Assert(options is null, "123456789");

using var context = new DefaultDbContext(options);
 
var list1 = context.UserEntitys.ToList();

var user = new UserEntity
{
    Id = 1,
    FirstName = "John",
    LastName = "Doe",
    Email = "john@doe.com",
    Password = "123",
};

context.UserEntitys.Add(user);
context.SaveChanges();

var list = context.UserEntitys.ToList();


Console.WriteLine($"Users count: {context.UserEntitys.Count()}");

user = context.UserEntitys.First();

Console.WriteLine($"User: {user.FirstName} {user.LastName} - {user.Email} ({user.Password.Length})");
