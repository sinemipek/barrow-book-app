using BarrowBookApp.Models;
using BarrowBookApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace BarrowBookAppTest
{
    [TestFixture]
    public class AccountTests
    {
        protected IAccountService _accountBusiness;

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IAccountService, AccountService>();
            var serviceProvider = services.BuildServiceProvider();
            _accountBusiness = serviceProvider.GetService<IAccountService>();
        }

        [Test]
        public void GetUsernameById_ReturnsIsNotNull()
        {
            var inputUserId = "1";
            var username = _accountBusiness.GetUsernameById(inputUserId);
            Assert.IsNotNull(username);
        }

        [Test]
        public void GetUsernameById_ReturnsAreEqual()
        {
            var inputUserId = "2";
            var expectedUsername = "User2";
            var username = _accountBusiness.GetUsernameById(inputUserId);
            Assert.AreEqual(expectedUsername, username);
        }
    }
}