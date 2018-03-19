using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using eBook.Controllers;
using System.Web.Mvc;
using eBook.Service;
using eBook.Model;
using System.Collections.Generic;
using eBook.Dao;

namespace eBookTest
{
    [TestClass]
    public class UnitTest1
    {

        /// <summary>
        /// 測試查詢所有書籍資料
        /// </summary>
        [TestMethod]
        public void TestGetAllBooks()
        {
            ///Arrange
            BookDao bookDao = new BookDao();
            BookSearchArg arg = new BookSearchArg();

            ///Act
            List<Book> books = bookDao.GetBookByCondtioin(arg);

            ///Assert
            Assert.AreEqual(2305, books.Count);

        }
        
        /// <summary>
        /// 測試查詢所有class是BK的書籍資料
        /// </summary>
        [TestMethod]
        public void TestGetClassBKBooks()
        {
            ///Arrange
            BookDao bookDao = new BookDao();
            BookSearchArg arg = new BookSearchArg() {
                BookClassId = "BK"
            };

            ///Act
            List<Book> books = bookDao.GetBookByCondtioin(arg);

            ///Assert
            Assert.AreEqual(145, books.Count);

        }
        /// <summary>
        /// 測試刪除"已借出"資料 從Dao判斷
        /// </summary>
        [TestMethod]
        public void TestDeleteBooks()
        {
            //Arrange
            BookDao bookDao = new BookDao();
            String bookId = "1";

            //Act
            Boolean result = bookDao.DeleteBookById(bookId);

            //Assert
            Assert.AreEqual(false, result);

        }

        /// <summary>
        /// 測試刪除"已借出"資料 從Controller判斷
        /// </summary>
        [TestMethod]
        public void TestDeleteBooksByController()
        {
            //Arrange
            BookController controller = new BookController();
            String bookId = "1";

            //Act
            JsonResult result = controller.DeleteBook(bookId);

            //Assert
            Assert.AreEqual("False", result.Data.ToString());

        }

        /// <summary>
        /// 測試新增資料 
        /// </summary>
        [TestMethod]
        public void TestInsertBook()
        {
            //Arrange
            BookDao bookDao = new BookDao();
            eBook.Model.Book book = new Book() {
                BOOK_NAME = "test1",
                BOOK_AUTHOR = "test1",
                BOOK_NOTE = "test1",
                BOOK_BOUGHT_DATE = "2018/03/17",
                BOOK_CLASS_ID = "DB",
                BOOK_PUBLISHER = "test1"
            };


            //Act
            int bookAutoId = bookDao.InsertBook(book);

            //Assert
            Assert.AreEqual(2334, bookAutoId);

        }

        /// <summary>
        /// 測試更新資料 
        /// </summary>
        [TestMethod]
        public void TestUpdateBook()
        {
            //Arrange
            BookDao bookDao = new BookDao();
            eBook.Model.Book book = new Book()
            {
                BOOK_ID = 2326 ,
                BOOK_NAME = "666666",
                BOOK_AUTHOR = "66666",
                BOOK_NOTE = "66666",
                BOOK_BOUGHT_DATE = "2018/03/17",
                BOOK_CLASS_ID = "DB",
                BOOK_STATUS = "A" ,
                BOOK_KEEPER = "",
                BOOK_PUBLISHER = "test1"
            };


            //Act
            bookDao.UpdateBook(book);
            eBook.Model.Book check = bookDao.GetUpdateBook(book.BOOK_ID.ToString());
            
            //Assert
            Assert.AreEqual(book.BOOK_AUTHOR, check.BOOK_AUTHOR);

        }
    }
}
