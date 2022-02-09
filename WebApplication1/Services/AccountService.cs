using BarrowBookApp.Models;
using BarrowBookApp.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarrowBookApp.Services
{
    public class AccountService : IAccountService
    {
        private readonly List<UserModel> _users;
        public AccountService()
        {
            _users = GetUsers();
        }
        public UserModel Login(UserModel userToLogin)
        {
            var user = _users.Find(c => c.Username == userToLogin.Username && c.Password == userToLogin.Password);
            return user;

        }

        public string GetUsernameById(string userId)
        {
            var user = _users.Find(c => c.UserId == userId);
            return user.Username;
        }

        private List<UserModel> GetUsers()
        {
            return new List<UserModel>
            { new UserModel()
                {
                    UserId="1" ,
                    Username="User1",
                    Password = "123"
                },
                new UserModel()
                {
                    UserId="2" ,
                    Username="User2",
                    Password = "123"
                },
                new UserModel()
                {
                    UserId="3" ,
                    Username="User3",
                    Password = "123"
                }
            };
        }
    }
}
