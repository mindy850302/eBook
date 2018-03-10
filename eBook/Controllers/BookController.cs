using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using System.Web.Script.Serialization;

namespace eBook.Controllers
{
    public class BookController : Controller
    {
        Models.ClassServices classServices = new Models.ClassServices();

        // GET: Book
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        ///依查詢條件回傳結果
        [HttpPost()]
        public JsonResult Index(Models.BookSearchArg arg )
        {
            Models.BookServices bookService = new Models.BookServices();

            ///ViewBag.SearchResult = bookService.GetBookByCondtioin(arg);回傳查詢結果

            //回傳JSON格式的查詢結果
            var json = new JavaScriptSerializer();
            var jsonArray = json.Serialize(bookService.GetBookByCondtioin(arg));

            return Json(jsonArray);
        }

        ///顯示新增書籍頁面
        [HttpGet]
        public ActionResult InsertBook() {
            ///下拉式選單資料
            ViewBag.BookClass = classServices.GetClassTable();///圖書類別
            return View();
        }

        ///執行新增書籍
        [HttpPost]
        public JsonResult InsertBook(Models.Book book)
        {
            Models.BookServices bookServices = new Models.BookServices();

                int bookId = bookServices.InsertBook(book);

                return Json(bookId);
        }
        
        ///顯示修改書籍頁面
        [HttpGet]
        public ActionResult UpdateBook(String id)
        {
            Models.BookServices bookServices = new Models.BookServices();
            
            ///取得欲更新的書籍資訊
            ViewBag.TheUpdateBook = bookServices.GetUpdateBook(id);

            ///取得預設圖書類別
            List<SelectListItem> BookClassSelect = classServices.GetClassTable();
            foreach (SelectListItem i in BookClassSelect) {
                if (i.Value.Equals(ViewBag.TheUpdateBook.BOOK_CLASS_ID)){
                    i.Selected = true;
                }
            }
            ViewBag.BookClass = BookClassSelect;

            ///取得預設借閱狀態
            List<SelectListItem> StatusClassSelect = classServices.GetStatusTable();
            foreach (SelectListItem i in StatusClassSelect)
            {
                if (i.Value.Equals(ViewBag.TheUpdateBook.BOOK_STATUS))
                {
                    i.Selected = true;
                }
            }
            ViewBag.StatusClass = StatusClassSelect;

            ///取得預設借閱人
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
                ///更新書籍
                bookServices.UpdateBook(book); 
            }

            return RedirectToAction("Index", "Book");///RedirectToAction("Function name","Controller name")
        }

        ///顯示書籍明細頁面
        [HttpGet]
        public ActionResult DetailOfBook(String id)
        {
            Models.BookServices bookServices = new Models.BookServices();

            ///取得特定書籍資訊
            ViewBag.DetailBook = bookServices.GetUpdateBook(id);

            ///取得預設圖書類別
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

            ///取得預設借閱狀態
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

            ///取得預設借閱人
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

        ///下拉式選單資料
        [HttpGet]
        public JsonResult GetDropdownList(string type)
        {
            var json = new JavaScriptSerializer();
            var jsonForClassArray = "";
            switch (type)
            {
                case "BookClass":
                    jsonForClassArray = json.Serialize(classServices.GetClassTable());
                    break;

                case "KeeperClass":
                    jsonForClassArray = json.Serialize(classServices.GetKeeperTable());
                    break;

                case "StatusClass":
                    jsonForClassArray = json.Serialize(classServices.GetStatusTable());
                    break;
                default:
                    break;
            }

            return Json(jsonForClassArray, JsonRequestBehavior.AllowGet);
        }
    }
}