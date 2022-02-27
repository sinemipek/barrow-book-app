using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BarrowBookApp.Models;
using BarrowBookApp.Services;
using System.Security.Claims;
using System.Threading;

namespace BarrowBookApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookService _bookBusiness;

        public HomeController(ILogger<HomeController> logger, IBookService bookBusiness)
        {
            _logger = logger;
            _bookBusiness = bookBusiness;
        }

        [Authorize]
        public IActionResult Index()
        {
            var bookUserList = GetBookUserList();
            return View(bookUserList);
        }

        public IActionResult _List()
        {
            var bookUserList = GetBookUserList();
            return PartialView("_List",bookUserList);
        }

        private List<BookUserModel> GetBookUserList()
        {
            return _bookBusiness.GetBookUserList();
        }

        public IActionResult BarrowBook(string bookId)
        {
            var identity = (ClaimsPrincipal)HttpContext.User;
            var userId = identity.Claims.Where(c => c.Type == ClaimTypes.Sid)
                               .Select(c => c.Value).SingleOrDefault();

           var result = _bookBusiness.BarrowBook(bookId,userId);
            return Json(result);
        }

        public IActionResult ReturnBook(string bookId)
        {
            var identity = (ClaimsPrincipal)HttpContext.User;
            var userId = identity.Claims.Where(c => c.Type == ClaimTypes.Sid)
                               .Select(c => c.Value).SingleOrDefault();

            _bookBusiness.ReturnBook(bookId, userId);
            return Json(true);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
