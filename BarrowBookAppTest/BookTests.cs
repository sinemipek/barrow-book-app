using BarrowBookApp.Services;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace BarrowBookAppTest
{
    [TestFixture]
    public class BookTests
    {
        protected IBookService _bookBusiness;

        public BookTests()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IXmlSerializerService, XmlSerializerService>();
            services.AddSingleton<IAccountService, AccountService>();
            services.AddSingleton<IBookService, BookService>();
            var serviceProvider = services.BuildServiceProvider();
            _bookBusiness = serviceProvider.GetService<IBookService>();
            var path = "..\\..\\..\\..\\WebApplication1\\Books.xml";
            _bookBusiness.LoadAllBooks(path);
        }

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void GetBookUserList_ReturnsIsNotNull()
        {
            var list = _bookBusiness.GetBookUserList();
            Assert.IsNotNull(list);
        }

        [Test]
        public void GetBookUserList_ReturnsTrueCountMoreThan0()
        {
            var list = _bookBusiness.GetBookUserList();
            Assert.IsTrue(list.Count>0);
        }

        [Test]
        public void BarrowBook_AvaliableBook_ReturnsTrue()
        {
            var bookId = "bk101";
            var userId = "1";
            var result = _bookBusiness.BarrowBook(bookId, userId);
            Assert.IsTrue(result);
        }

        [Test]
        public void BarrowBook_UnavaliableBook_ReturnsFalse()
        {
            var bookId = "bk101";
            var userId = "1";
            _bookBusiness.BarrowBook(bookId, userId);
            var result = _bookBusiness.BarrowBook(bookId, userId);
            Assert.IsFalse(result);
        }

        [Test]
        public void ReturnBook_ReturnBarrowedBook_ReturnsTrue()
        {
            var bookId = "bk101";
            var userId = "1";
            _bookBusiness.BarrowBook(bookId, userId);
            var result = _bookBusiness.ReturnBook(bookId, userId);
            Assert.IsTrue(result);
        }

        [Test]
        public void ReturnBook_ReturnUnbarrowedBook_ReturnsFalse()
        {
            var bookId = "bk101";
            var userId = "1";
            _bookBusiness.BarrowBook(bookId, userId);
             bookId = "bk101";
             userId = "2";
            var result = _bookBusiness.ReturnBook(bookId, userId);
            Assert.IsFalse(result);
        }
    }
}