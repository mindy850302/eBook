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
            return View();
        }

        [HttpGet]
        public ActionResult GetUpdateBook(String id)
        {
            Models.BookServices bookServices = new Models.BookServices();

            Models.Book updateBook = new Models.Book();
            ///取得欲更新的書籍資訊
            updateBook = bookServices.GetUpdateBook(id);
            return Json(updateBook, JsonRequestBehavior.AllowGet);
        }

        ///更新書籍資料
        [HttpPost]
        public JsonResult UpdateBook(Models.Book book)
        {
            Models.BookServices bookServices = new Models.BookServices();        
                ///更新書籍
                bookServices.UpdateBook(book); 

            return Json("success");///RedirectToAction("Function name","Controller name")
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
                return Json(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Json(false);
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