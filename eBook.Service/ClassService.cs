using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace eBook.Service
{
    public class ClassService : IClassService
    {
        private eBook.Dao.IClassDao classDao { get; set; }

        public List<SelectListItem> GetClassTable()
        {
            return classDao.GetClassTable();
        }
        public List<SelectListItem> GetKeeperTable()
        {
            return classDao.GetKeeperTable();
        }
        public List<SelectListItem> GetStatusTable()
        {
            return classDao.GetStatusTable();
        }

        }
}
