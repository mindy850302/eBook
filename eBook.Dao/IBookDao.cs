using System.Collections.Generic;
using eBook.Model;

namespace eBook.Dao
{
    public interface IBookDao
    {
        void DeleteBookById(string BookId);
        List<Book> GetBookByCondtioin(BookSearchArg arg);
        Book GetUpdateBook(string id);
        int InsertBook(Book book);
        void UpdateBook(Book book);
    }
}