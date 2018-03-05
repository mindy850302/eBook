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

            ViewBag.SearchResult = bookService.GetBookByCondtioin(arg);///回傳查詢結果
            ViewBag.BookClass = classServices.GetClassTable();///取得圖書類別
            ViewBag.KeeperClass = classServices.GetKeeperTable();///取得借閱人類別
            ViewBag.StatusClass = classServices.GetStatusTable();///取得借閱狀態類別

            return View("Index");
        }

        ///顯示新增書籍頁面
        [HttpGet]
        public ActionResult InsertBook() {
            ViewBag.BookClass = classServices.GetClassTable();
            return View();
        }

        ///執行新增書籍
        [HttpPost]
        public ActionResult InsertBook(Models.Book book)
        {
            Models.BookServices bookServices = new Models.BookServices();
            if (ModelState.IsValid)
            {
                bookServices.InsertBook(book);
                return RedirectToAction("Index", "Book");
            }
            else
            {
                ViewBag.BookClass = classServices.GetClassTable();
                return View();
            }

            //return RedirectToAction("Index","Book");
        }
        
        ///顯示修改書籍頁面
        [HttpGet]
        public ActionResult UpdateBook(String id)
        {
            Models.BookServices bookServices = new Models.BookServices();
            
            ///取得欲更新的書籍資訊
            ViewBag.TheUpdateBook = bookServices.GetUpdateBook(id);
            
            ///取得圖書類別
            List<SelectListItem> BookClassSelect = classServices.GetClassTable();
            foreach (SelectListItem i in BookClassSelect) {
                if (i.Value.Equals(ViewBag.TheUpdateBook.BOOK_CLASS_ID)){
                    i.Selected = true;
                }
            }
            ViewBag.BookClass = BookClassSelect;

            ///取得借閱狀態
            List<SelectListItem> StatusClassSelect = classServices.GetStatusTable();
            foreach (SelectListItem i in StatusClassSelect)
            {
                if (i.Value.Equals(ViewBag.TheUpdateBook.BOOK_STATUS))
                {
                    i.Selected = true;
                }
            }
            ViewBag.StatusClass = StatusClassSelect;

            ///取得借閱人
            List<SelectListItem> KeeperClassSelect = classServices.GetKeeperTable();
            foreach (SelectListItem i in KeeperClassSelect)
            {
                if (i.Value.Equals(ViewBag.TheUpdateBook.BOOK_KEEPER))
                {
                    i.Selected = true;
                }
            }
            ViewBag.KeeperClass = KeeperClassSelect;

            return View();
        }

        ///更新書籍資料
        [HttpPost]
        public ActionResult UpdateBook(Models.Book book)
        {
            Models.BookServices bookServices = new Models.BookServices();
            if (ModelState.IsValid)
            {
                int i = 5;
                i = bookServices.UpdateBook(book);
                Console.Write(i);
            }

            return RedirectToAction("Index", "Book");
        }

        ///顯示書籍明細頁面
        [HttpGet]
        public ActionResult DetailOfBook(String id)
        {
            Models.BookServices bookServices = new Models.BookServices();

            ///取得特定書籍資訊
            ViewBag.DetailBook = bookServices.GetUpdateBook(id);

            ///取得圖書類別
            List<SelectListItem> BookClassSelect = classServices.GetClassTable();
            foreach (SelectListItem i in BookClassSelect)
            {
                if (i.Value.Equals(ViewBag.DetailBook.BOOK_CLASS_ID))
                {
                    i.Selected = true;
                    ViewBag.Class = i.Text;
                }
            }
            ViewBag.BookClass = BookClassSelect;

            ///取得借閱狀態
            List<SelectListItem> StatusClassSelect = classServices.GetStatusTable();
            foreach (SelectListItem i in StatusClassSelect)
            {
                if (i.Value.Equals(ViewBag.DetailBook.BOOK_STATUS))
                {
                    i.Selected = true;
                    ViewBag.Status = i.Text;
                }
            }
            ViewBag.StatusClass = StatusClassSelect;

            ///取得借閱人
            List<SelectListItem> KeeperClassSelect = classServices.GetKeeperTable();
            foreach (SelectListItem i in KeeperClassSelect)
            {
                if (i.Value.Equals(ViewBag.DetailBook.BOOK_KEEPER))
                {
                    i.Selected = true;
                    ViewBag.Keeper = i.Text;
                }
            }
            ViewBag.KeeperClass = KeeperClassSelect;

            return View();
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