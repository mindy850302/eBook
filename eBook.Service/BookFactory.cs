using eBook.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBook.Service
{
    public class BookFactory
    {
        public IBookDao GetBookDao()
        {
            IBookDao result;

            switch (Common.ConfigTool.GetAppsetting("DaoInTest"))
            {
                case "Y":
                    result = new eBook.Dao.BookTestDao();
                    break;
                case "N":
                    result = new eBook.Dao.BookDao();
                    break;
                default:
                    result = new eBook.Dao.BookTestDao();
                    break;
            }
            return result;
        }
    }
}
