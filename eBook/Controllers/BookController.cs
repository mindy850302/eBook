using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using System.Web.Script.Serialization;
using eBook.Service;

namespace eBook.Controllers
{
    public class BookController : Controller
    {
        private IClassService classService { get; set; }
        private IBookService bookService { get; set; }

        /// 首頁
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                eBook.Common.Logger.Write(eBook.Common.Logger.LogCategoryEnum.Error, ex.ToString());
                return View("Error");
            }
            
        }

        ///依查詢條件回傳結果
        [HttpPost()]
        public JsonResult Index(eBook.Model.BookSearchArg arg )
        {
            ///ViewBag.SearchResult = bookService.GetBookByCondtioin(arg);回傳查詢結果

            ///回傳JSON格式的查詢結果
            var json = new JavaScriptSerializer();
            var jsonArray = json.Serialize(bookService.GetBookByCondtioin(arg));

            return Json(jsonArray);
        }

        ///顯示新增書籍頁面
        [HttpGet]
        public ActionResult InsertBook() {
            return View();
        }

        ///執行新增書籍
        [HttpPost]
        public JsonResult InsertBook(eBook.Model.Book book)
        {
                int bookId = bookService.InsertBook(book);

                return Json(bookId);
        }
        
        ///顯示修改書籍頁面
        [HttpGet]
        public ActionResult UpdateBook(String id)
        {
            ViewBag.BookId = id;
            return View();
        }

        /// <summary>
        /// 取得更新頁面書籍資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetUpdateBook(String id)
        {

            eBook.Model.Book updateBook = new eBook.Model.Book();
            ///取得欲更新的書籍資訊
            updateBook = bookService.GetUpdateBook(id);
            return Json(updateBook, JsonRequestBehavior.AllowGet);
        }

        ///更新書籍資料
        [HttpPost]
        public JsonResult UpdateBook(eBook.Model.Book book)
        {    
                ///更新書籍
                bookService.UpdateBook(book); 

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
                bookService.DeleteBookById(bookId);
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
                ///回傳圖書類別Data
                case "BookClassId":
                    jsonForClassArray = json.Serialize(classService.GetClassTable());
                    break;

                ///回傳借閱人Data
                case "BookKeeper":
                    jsonForClassArray = json.Serialize(classService.GetKeeperTable());
                    break;

                ///回傳借閱狀態Data
                case "BookStatus":
                    jsonForClassArray = json.Serialize(classService.GetStatusTable());
                    break;
                default:
                    break;
            }

            return Json(jsonForClassArray, JsonRequestBehavior.AllowGet);
        }
    }
}