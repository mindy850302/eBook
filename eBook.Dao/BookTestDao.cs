using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eBook.Model;

namespace eBook.Dao
{
    public class BookTestDao : IBookDao
    {
        public void DeleteBookById(string BookId)
        {
            throw new NotImplementedException();
        }

        public List<Book> GetBookByCondtioin(BookSearchArg arg)
        {
            throw new NotImplementedException();
        }

        public Book GetUpdateBook(string id)
        {
            throw new NotImplementedException();
        }

        public int InsertBook(Book book)
        {
            throw new NotImplementedException();
        }

        public void UpdateBook(Book book)
        {
            throw new NotImplementedException();
        }
    }
}
