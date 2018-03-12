using System;
using System.Collections.Generic;
using System.Data;
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

        /// <summary>
        /// 新增書籍
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public int InsertBook(Models.Book book)
        {
            ///Select SCOPE_IDENTITY() ->取得insert 的Book id
            string sql = @" 
                       BEGIN TRY   
                             INSERT INTO BOOK_DATA
                                   ( BOOK_NAME , BOOK_CLASS_ID
                                   , BOOK_AUTHOR , BOOK_BOUGHT_DATE
                                   , BOOK_PUBLISHER , BOOK_NOTE
                                   , BOOK_STATUS , BOOK_KEEPER
                                   , CREATE_DATE , CREATE_USER
                                   , MODIFY_DATE , MODIFY_USER )
                             VALUES
                                   ( @BookName , @BookClassId
                                   , @BookAuthor , @BookBoughtDate
                                   , @BookPublisher , @BookNote
                                   , @BookStatus , @BookKeeper
                                   , @CreateDate , @CreateUser
                                   , @ModifyDate , @ModifyUser )
						    Select SCOPE_IDENTITY()
                        
                        END TRY
                        BEGIN CATCH 
	                        SELECT ERROR_NUMBER()
                            ROLLBACK TRAN
                        END CATCH;
                        ";

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

        /// <summary>
        /// 依照條件取得書籍資料
        /// </summary>
        /// <returns></returns>
        public List<Models.Book> GetBookByCondtioin(Models.BookSearchArg arg)
        {

            DataTable dt = new DataTable();
            string sql = @"SELECT a.BOOK_ID , a.BOOK_NAME
	                          ,a.BOOK_CLASS_ID , b.BOOK_CLASS_NAME as CLASS_NAME
                              ,a.BOOK_AUTHOR , CONVERT( varchar(12), a.BOOK_BOUGHT_DATE, 111) as BOOK_BOUGHT_DATE
                              ,a.BOOK_PUBLISHER , a.BOOK_NOTE
	                          ,a.BOOK_STATUS , c.CODE_TYPE , c.CODE_NAME as STATUS_NAME
                              ,a.BOOK_KEEPER , case(a.BOOK_STATUS) 
							  WHEN 'A' THEN ' '
							  WHEN 'U' THEN ' '
							  ELSE d.USER_ENAME + '(' + d.USER_CNAME + ')'
							  END as USER_NAME
                              ,a.CREATE_DATE , a.CREATE_USER
                              ,a.MODIFY_DATE , a.MODIFY_USER
                          FROM BOOK_DATA as a
                          Left JOIN BOOK_CLASS as b
	                        ON a.BOOK_CLASS_ID = b.BOOK_CLASS_ID
                          Left JOIN BOOK_CODE as c
	                        ON a.BOOK_STATUS = c.CODE_ID
                          Left JOIN MEMBER_M as d
	                        ON a.BOOK_KEEPER = d.USER_ID
                        WHERE (a.BOOK_NAME LIKE '%' + @BookName + '%' ) AND 
	                          (a.BOOK_CLASS_ID = @BookClassId OR @BookClassId = '' ) AND
	                          (a.BOOK_KEEPER = @BookKeeper OR @BookKeeper = '' ) AND
	                          (a.BOOK_STATUS = @BookStatus OR @BookStatus = '' ) AND
                              (c.CODE_TYPE = 'BOOK_STATUS')
                        ORDER BY BOOK_BOUGHT_DATE DESC
                        ";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@BookName", arg.BookName == null ?string.Empty : arg.BookName));
                cmd.Parameters.Add(new SqlParameter("@BookClassId", arg.BookClassId == null ? string.Empty : arg.BookClassId));
                cmd.Parameters.Add(new SqlParameter("@BookKeeper", arg.BookKeeper == null ?string.Empty : arg.BookKeeper));
                cmd.Parameters.Add(new SqlParameter("@BookStatus", arg.BookStatus == null ? string.Empty : arg.BookStatus));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapBookDataToList(dt);
        }

        /// <summary>
        /// Map 書籍資訊格式
        /// </summary>
        /// <param name="bookdata"></param>
        /// <returns></returns>
        private List<Models.Book> MapBookDataToList(DataTable bookdata)
        {
            List<Models.Book> result = new List<Book>();
            foreach (DataRow row in bookdata.Rows)
            {
                result.Add(new Book()
                {
                    BOOK_ID = (int)row["BOOK_ID"],
                    BOOK_NAME = row["BOOK_NAME"].ToString(),///將object轉成字串
                    BOOK_CLASS_ID = row["CLASS_NAME"].ToString(),
                    BOOK_AUTHOR = row["BOOK_AUTHOR"].ToString(),
                    BOOK_BOUGHT_DATE = row["BOOK_BOUGHT_DATE"].ToString(),
                    BOOK_KEEPER = row["USER_NAME"].ToString() ,
                    BOOK_NOTE = row["BOOK_NOTE"].ToString(),
                    BOOK_PUBLISHER = row["BOOK_PUBLISHER"].ToString(),
                    BOOK_STATUS = row["STATUS_NAME"].ToString(),
                    CREATE_DATE = row["CREATE_DATE"].ToString(),
                    CREATE_USER = row["CREATE_USER"].ToString(),
                    MODIFY_DATE = row["MODIFY_DATE"].ToString(),
                    MODIFY_USER = row["MODIFY_USER"].ToString()
                   
                });
            }
            return result;
        }

        /// <summary>
        /// 刪除書籍
        /// </summary>
        public void DeleteBookById(string BookId)
        {
            try
            {
                string sql = @"
                                BEGIN TRY
                                        Delete FROM BOOK_DATA Where BOOK_ID=@BookId
                                END TRY
                                BEGIN CATCH 
	                                SELECT ERROR_NUMBER()
                                    ROLLBACK TRAN
                                END CATCH;";
                using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@BookId", BookId));
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 取得欲更新書籍的資料
        /// </summary>
        /// <returns></returns>
        public Models.Book GetUpdateBook(String id)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT a.BOOK_ID , a.BOOK_NAME
	                          ,a.BOOK_CLASS_ID , b.BOOK_CLASS_NAME as CLASS_NAME
                              ,a.BOOK_AUTHOR , FORMAT(a.BOOK_BOUGHT_DATE,'yyyy/MM/dd') as BOOK_BOUGHT_DATE
                              ,a.BOOK_PUBLISHER , a.BOOK_NOTE
	                          ,a.BOOK_STATUS , c.CODE_NAME as STATUS_NAME
                              ,a.BOOK_KEEPER , d.USER_ENAME ,d.USER_CNAME
                              ,a.CREATE_DATE , a.CREATE_USER
                              ,a.MODIFY_DATE , a.MODIFY_USER
                          FROM GSSWEB.dbo.BOOK_DATA as a
                          Left JOIN BOOK_CLASS as b
	                        ON a.BOOK_CLASS_ID = b.BOOK_CLASS_ID
                          Left JOIN BOOK_CODE as c
	                        ON a.BOOK_STATUS = c.CODE_ID
                          Left JOIN MEMBER_M as d
	                        ON a.BOOK_KEEPER = d.USER_ID
                        WHERE (a.BOOK_ID = @BookID) ";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@BookID", id == null ? string.Empty : id));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }

            Models.Book book = new Book();

            foreach (DataRow row in dt.Rows)
            {
                book = new Book() {
                    BOOK_ID = (int)row["BOOK_ID"],
                    BOOK_NAME = row["BOOK_NAME"].ToString(),///將object轉成字串
                    BOOK_CLASS_ID = row["BOOK_CLASS_ID"].ToString(),
                    BOOK_AUTHOR = row["BOOK_AUTHOR"].ToString(),
                    BOOK_BOUGHT_DATE = row["BOOK_BOUGHT_DATE"].ToString(),
                    BOOK_KEEPER = row["BOOK_KEEPER"].ToString(),
                    BOOK_NOTE = row["BOOK_NOTE"].ToString(),
                    BOOK_PUBLISHER = row["BOOK_PUBLISHER"].ToString(),
                    BOOK_STATUS = row["BOOK_STATUS"].ToString(),
                    CREATE_DATE = row["CREATE_DATE"].ToString(),
                    CREATE_USER = row["CREATE_USER"].ToString(),
                    MODIFY_DATE = row["MODIFY_DATE"].ToString(),
                    MODIFY_USER = row["MODIFY_USER"].ToString()
                };
            }
            return book;
        }

        /// <summary>
        /// 更新書籍的資料
        /// </summary>
        /// <returns></returns>
        public int UpdateBook(Models.Book book)
        {
            string sql = @" 
                         BEGIN TRY   
                            UPDATE BOOK_DATA
                                SET
                                        BOOK_NAME =  @BookName 
                                       ,BOOK_CLASS_ID = @BookClassId
                                       ,BOOK_AUTHOR = @BookAuthor
                                       ,BOOK_BOUGHT_DATE =  @BookBoughtDate 
                                       ,BOOK_PUBLISHER = @BookPublisher 
                                       ,BOOK_NOTE = @BookNote
                                       ,BOOK_STATUS =  @BookStatus 
                                       ,BOOK_KEEPER = @BookKeeper
                                       ,MODIFY_DATE = @ModifyDate
                                WHERE BOOK_DATA.BOOK_ID = @BookId
                        END TRY
                        BEGIN CATCH 
	                            SELECT ERROR_NUMBER()
                                ROLLBACK TRAN
                        END CATCH;";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                
                DateTime localDate = DateTime.Now;
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.Add(new SqlParameter("@BookName", book.BOOK_NAME));
                cmd.Parameters.Add(new SqlParameter("@BookClassId", book.BOOK_CLASS_ID));
                cmd.Parameters.Add(new SqlParameter("@BookAuthor", book.BOOK_AUTHOR));
                cmd.Parameters.Add(new SqlParameter("@BookBoughtDate", book.BOOK_BOUGHT_DATE));
                cmd.Parameters.Add(new SqlParameter("@BookPublisher", book.BOOK_PUBLISHER));
                cmd.Parameters.Add(new SqlParameter("@BookNote", book.BOOK_NOTE));
                cmd.Parameters.Add(new SqlParameter("@BookStatus", book.BOOK_STATUS));
                cmd.Parameters.Add(new SqlParameter("@BookKeeper", book.BOOK_STATUS.Equals("A")|| book.BOOK_STATUS.Equals("U") ? "" : book.BOOK_KEEPER));
                cmd.Parameters.Add(new SqlParameter("@ModifyDate", localDate));
                cmd.Parameters.Add(new SqlParameter("@BookId", (int)book.BOOK_ID));

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            return 1;
        }

    }
}