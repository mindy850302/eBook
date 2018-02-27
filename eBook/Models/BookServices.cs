using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace eBook.Models
{
    public class BookServices
    {
        /// <summary>
        /// 取得DB連線字串
        /// </summary>
        /// <returns></returns>
        private string GetDBConnectionString()
        {
            return
                System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString.ToString();
        }

        public int InsertBook(Models.Book book)
        {
            ///Select SCOPE_IDENTITY() ->取得insert 的Book id
            string sql = @" INSERT INTO [dbo].[BOOK_DATA]
                                   ([BOOK_NAME],[BOOK_CLASS_ID]
                                   ,[BOOK_AUTHOR],[BOOK_BOUGHT_DATE]
                                   ,[BOOK_PUBLISHER],[BOOK_NOTE]
                                   ,[BOOK_STATUS],[BOOK_KEEPER]
                                   ,[CREATE_DATE],[CREATE_USER]
                                   ,[MODIFY_DATE],[MODIFY_USER])
                             VALUES
                                   (@BookName,@BookClassId
                                   ,@BookAuthor,@BookBoughtDate
                                   ,@BookPublisher,@BookNote
                                   ,@BookStatus,@BookKeeper
                                   ,@CreateDate,@CreateUser
                                   ,@ModifyDate,@ModifyUser)
						Select SCOPE_IDENTITY()";

            int BookId;

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                DateTime localDate = DateTime.Now;
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.Add(new SqlParameter("@BookName", book.BOOK_NAME));
                cmd.Parameters.Add(new SqlParameter("@BookClassId", book.BOOK_CLASS_ID));
                cmd.Parameters.Add(new SqlParameter("@BookAuthor", book.BOOK_AUTHOR));
                cmd.Parameters.Add(new SqlParameter("@BookBoughtDate", book.BOOK_BOUGHT_DATE));
                cmd.Parameters.Add(new SqlParameter("@BookPublisher", book.BOOK_PUBLISHER));
                cmd.Parameters.Add(new SqlParameter("@BookNote", book.BOOK_NOTE));
                cmd.Parameters.Add(new SqlParameter("@BookStatus", "A"));
                cmd.Parameters.Add(new SqlParameter("@BookKeeper", ""));
                cmd.Parameters.Add(new SqlParameter("@CreateDate", localDate));
                cmd.Parameters.Add(new SqlParameter("@CreateUser", (Object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@ModifyDate", (Object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@ModifyUser", (Object)DBNull.Value));


                BookId = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
            }

            return BookId;
        }
    }
}