using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBook.Service
{
    public class BookService : IBookService
    {
        private eBook.Dao.IBookDao bookDao { get; set; }

        public int InsertBook(eBook.Model.Book book)
        {
            return bookDao.InsertBook(book);
        }

        public List<eBook.Model.Book> GetBookByCondtioin(eBook.Model.BookSearchArg arg)
        {
            return bookDao.GetBookByCondtioin(arg);
        }

        public bool DeleteBookById(string BookId)
        {
            return bookDao.DeleteBookById(BookId);
        }



        public eBook.Model.Book GetUpdateBook(String id)
        {
            return bookDao.GetUpdateBook(id);
        }
        public void UpdateBook(eBook.Model.Book book)
        {
            bookDao.UpdateBook(book);
        }
        }
}
