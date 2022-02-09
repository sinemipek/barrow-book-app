using BarrowBookApp.Models;
using System.Collections.Generic;

namespace BarrowBookApp.Services
{
    public class BookService : IBookService
    {
        private readonly IXmlSerializerService _xmlSerializer;
        private readonly IAccountService _accountBusiness;
        private string fileName = "Books.xml";

        public List<BookUserModel> _bookUserList;
        public BookService(IXmlSerializerService xmlSerializer, IAccountService accountBusiness)
        {
            _xmlSerializer = xmlSerializer;
            LoadAllBooks(fileName);
            _accountBusiness = accountBusiness;
       
        }

        public List<BookUserModel> GetBookUserList()
        {
            return _bookUserList;
        }
        public List<BookUserModel> LoadAllBooks(string path)
        {
            var catalog = DeserializeBooksXML(path);
            List<BookUserModel> bookUserList = new List<BookUserModel>();
            if(catalog != null)
            {
                foreach (var book in catalog.book)
                {
                    BookUserModel bookUserModel = new BookUserModel()
                    {
                        Book = book,
                        UserId = "0"
                    };
                    bookUserList.Add(bookUserModel);
                }
            }
            _bookUserList = bookUserList;
            return bookUserList;
        }

        public bool BarrowBook(string bookId,string userId)
        {
            var index = _bookUserList.FindIndex(i => i.Book.id == bookId);
            if (index != -1 && _bookUserList[index].UserId == "0")
            {
                _bookUserList[index].UserId = userId;
                _bookUserList[index].Username = _accountBusiness.GetUsernameById(userId);
                return true;
            }
            return false;
        }

        public bool ReturnBook(string bookId, string userId)
        {
            var index = _bookUserList.FindIndex(i => i.Book.id == bookId);
            if (index != -1 && _bookUserList[index].UserId == userId)
            {
                _bookUserList[index].UserId = "0";
                _bookUserList[index].Username = string.Empty;
                return true;
            }
            return false;
        }
        public catalog DeserializeBooksXML(string path)
        {
            var catalog = _xmlSerializer.Deserialize<catalog>(path);
            return catalog;
        }
    }
}
