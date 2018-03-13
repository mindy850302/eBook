using System.Collections.Generic;
using eBook.Model;

namespace eBook.Service
{
    public interface IBookService
    {
        void DeleteBookById(string BookId);
        List<Book> GetBookByCondtioin(BookSearchArg arg);
        Book GetUpdateBook(string id);
        int InsertBook(Book book);
        void UpdateBook(Book book);
    }
}