using BarrowBookApp.Models;
using System.Collections.Generic;

namespace BarrowBookApp.Services
{
    public interface IBookService
    {
        List<BookUserModel> GetBookUserList();
        bool BarrowBook(string bookId, string userId);
        bool ReturnBook(string bookId, string userId);
        List<BookUserModel> LoadAllBooks(string path);
        catalog DeserializeBooksXML(string path);

    }
}
