using Microsoft.VisualStudio.TestTools.UnitTesting;
using Drinks.Entities;
using Drinks.Repository;
using Drinks.Services;

namespace Drinks.Tests
{
    using System;
    using Drinks.Api.Controllers;
    using Drinks.Entities.Extensions;

    [TestClass]
    public class TaskMethods
    {
        [TestMethod]
        public void CreateUser()
        {
            var user = new User
            {
                Username = "levi.botelho",
                IsAdmin = true,
                Name = "Levi Botelho"
            };

            var uow = new UnitOfWork(new DrinksContext());
            var userService = new UserService(uow, new PasswordHelper());
            userService.CreateUser(user, "test");
            uow.Dispose();
        }

        [TestMethod]
        public void Test()
        {
            var uow = new UnitOfWork(new DrinksContext());
            var userService = new UserService(uow, new PasswordHelper());
            var productsService = new ProductsService(uow);
            var transactionService = new TransactionService(uow, userService, productsService);

            var controller = new BuyController(transactionService, userService, productsService);
            controller.Post(new BuyRequest { Badge = "0D0014720E", Product = 3, Time = DateTime.Now.ToUnixTimestamp() });

        }
    }
}