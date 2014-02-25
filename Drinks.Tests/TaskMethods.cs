using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Drinks.Tests
{
    using System.Linq;
    using Drinks.Entities;
    using Drinks.Repository;
    using Drinks.Services;

    [TestClass]
    public class TaskMethods
    {
        [TestMethod]
        public void CreateUser()
        {
            var user = new User
            {
                Username = "levi.botelho",
                Permissions = UserPermissions.IsAdmin | UserPermissions.CanMonitorPurchases,
                Name = "Levi Botelho"
            };
            
            var uow = new UnitOfWork(new DrinksContext());
            var userService = new UserService(uow, new PasswordHelper());
            userService.CreateUser(user, "test");
            uow.Dispose();
        }

        [TestMethod]
        public void GetUser()
        {
            var uow = new UnitOfWork(new DrinksContext());
            var userService = new UserService(uow, new PasswordHelper());
            var users = userService.GetAllUsers().ToArray();
            var i = 0;

        }
    }
}
