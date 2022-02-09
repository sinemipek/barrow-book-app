using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BarrowBookApp.Models;
using Microsoft.Extensions.Options;
using BarrowBookApp.Services;

namespace BarrowBookApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAccountService _accountBusiness;

        public AccountController(ILogger<HomeController> logger, IAccountService accountBusiness)
        {
            _logger = logger;
            _accountBusiness = accountBusiness;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserModel user)
        {
            var loginUser = _accountBusiness.Login(user);
            if (loginUser != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Sid, loginUser.UserId),
                    new Claim(ClaimTypes.Name, loginUser.Username)
                };

                var userIdentity = new ClaimsIdentity(claims, "login");

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);

                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Invalid username or password !");
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Login");
        }
    }
}
