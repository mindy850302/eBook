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
        [HttpGet]
        public ActionResult Index()
        {
            ///下拉式選單資料
            ViewBag.BookClass = classServices.GetClassTable();
            ViewBag.KeeperClass = classServices.GetKeeperTable();
            ViewBag.StatusClass = classServices.GetStatusTable();
            return View();
        }

        ///依查詢條件回傳結果
        [HttpPost()]
        public ActionResult Index(Models.BookSearchArg arg )
        {
            Models.BookServices bookService = new Models.BookServices();

            ViewBag.SearchResult = bookService.GetBookByCondtioin(arg);
            ViewBag.BookClass = classServices.GetClassTable();
            ViewBag.KeeperClass = classServices.GetKeeperTable();
            ViewBag.StatusClass = classServices.GetStatusTable();
            return View("Index");
        }

        [HttpGet]
        ///show create book page
        public ActionResult InsertBook() {
            ViewBag.BookClass = classServices.GetClassTable();
            return View();
        }

        [HttpPost]
        ///insert new book
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

        /// <summary>
        /// 刪除書籍
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpPost()]
        public JsonResult DeleteBook(string bookId)
        {
            try
            {
                Models.BookServices BookService = new Models.BookServices();
                BookService.DeleteBookById(bookId);
                return this.Json(true);
            }

            catch (Exception ex)
            {
                return this.Json(false);
            }
        }
    }
}