using BarrowBookApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarrowBookApp.Services
{
    public interface IAccountService
    {
        UserModel Login(UserModel userToLogin);
        string GetUsernameById(string userId);
    }
}
