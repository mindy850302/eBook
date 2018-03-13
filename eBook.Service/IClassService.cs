using System.Collections.Generic;
using System.Web.Mvc;

namespace eBook.Service
{
    public interface IClassService
    {
        List<SelectListItem> GetClassTable();
        List<SelectListItem> GetKeeperTable();
        List<SelectListItem> GetStatusTable();
    }
}