using System.Collections.Generic;
using System.Web.Mvc;

namespace eBook.Dao
{
    public interface IClassDao
    {
        List<SelectListItem> GetClassTable();
        List<SelectListItem> GetKeeperTable();
        List<SelectListItem> GetStatusTable();
    }
}