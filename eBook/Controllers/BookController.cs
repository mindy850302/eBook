using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eBook.Controllers
{
    public class BookController : Controller
    {
        Models.ClassServices classServices = new Models.ClassServices();

        // GET: Book
        public ActionResult Index()
        {
            ///下拉式選單資料
            ViewBag.BookClass = classServices.GetClassTable();
            ViewBag.KeeperClass = classServices.GetKeeperTable();
            ViewBag.StatusClass = classServices.GetStatusTable();
            return View();
        }

        [HttpGet]
        //show create book page
        public ActionResult InsertBook() {
            ViewBag.BookClass = classServices.GetClassTable();
            return View();
        }

        [HttpPost]
        //show create book page
        public ActionResult InsertBook(Models.Book book)
        {
            Models.BookServices bookServices = new Models.BookServices();
            if (ModelState.IsValid)
            {
                bookServices.InsertBook(book);
            }
            Models.Book emptybook = new Models.Book();
                return View("index");
        }
    }
}