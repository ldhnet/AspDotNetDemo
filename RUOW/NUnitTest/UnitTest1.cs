using Framework.EFCore;
using Framework.EFCore.Models;
using Framework.Repository;
using Framework.Service;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;

namespace NUnitTest
{
    public class Tests
    {

        private IUserService _userService;
        private UserRepository _userRepository;
        private IUnitOfWork _unitOfWork;
        [SetUp]
        public void Setup()
        { 
            _unitOfWork = new UnitOfWork();
            _userRepository = new UserRepository(_unitOfWork);
            _userService = new UserService(_userRepository);
        }

        [Test]
        public void Test_GetAll()
        {
            var aaa= _userService.GetAll();
            Assert.Pass();
        }
        [Test]
        public void Test_GetByID()
        {
            var aaa = _userService.GetUserEntity(1);
            Assert.True(aaa != null);
        }

        [Test]
        public void Test_Add()
        {
            var user = new UserEntity
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john@doe.com",
                Password = "123",
            };
            var aaa = _userService.Create(user);


            var aaab = _userService.GetAll();

            Assert.True(aaa != null);
        }

        [Test]
        public void Test_Update()
        {
           
            var model = _userService.GetUserEntity(1);

            model.Email = "123@qq.com";

            var aaa=  _userService.Edit(model);
             
            Assert.True(aaa);
        }

        [Test]
        public void Test_Delete()
        {
            var user = new UserEntity
            {
                FirstName = "John" + new Random().Next(100,999),
                LastName = "Doe" + new Random().Next(100, 999),
                Email = "john@doe.com",
                Password = "123",
            };
            var aaa = _userService.Create(user);

            var model = _userService.Delete(1); 

            Assert.True(aaa);
        }
    }
}