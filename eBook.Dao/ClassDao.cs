using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace eBook.Dao
{
    public class ClassDao : IClassDao
    {
        private string GetDBConnectionString()
        {
            return eBook.Common.ConfigTool.GetDBConnectionString("DBConn");
        }

        /// <summary>
        /// 取得圖書類別代號及名稱
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetClassTable()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT BOOK_CLASS_ID as CODE_ID,BOOK_CLASS_NAME as CODE_NAME
                          FROM BOOK_CLASS";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))///using可以確保程式碼結束該區塊時，自動關閉連線
            {
                ///開啟connection
                conn.Open();

                ///宣告一個SqlCommand物件
                SqlCommand cmd = new SqlCommand(sql, conn);

                ///宣告一個SqlDataAdapter 並傳入SqlCommand物件
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);

                ///填入資料
                sqlAdapter.Fill(dt);
                conn.Close();///關閉connection
            }

            return this.MapCodeData(dt);///MapCodeData 拆解字串
        }

        /// <summary>
        /// 取得借閱人中文及英文名稱 
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetKeeperTable()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT USER_ID as CODE_ID, ( USER_ENAME + '(' + USER_CNAME + ')' ) as CODE_NAME
                           FROM MEMBER_M";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))///using可以確保程式碼結束該區塊時，自動關閉連線
            {
                ///開啟connection
                conn.Open();

                ///宣告一個SqlCommand物件
                SqlCommand cmd = new SqlCommand(sql, conn);

                ///宣告一個SqlDataAdapter 並傳入SqlCommand物件
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);

                ///填入資料
                sqlAdapter.Fill(dt);
                conn.Close();///關閉connection
            }

            return this.MapCodeData(dt);///MapCodeData 拆解字串
        }

        /// <summary>
        /// 取得 書籍借閱狀態Status 
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetStatusTable()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT CODE_TYPE
                              ,CODE_ID
                              ,CODE_NAME
                          FROM BOOK_CODE
                          WHERE ( CODE_TYPE = 'BOOK_STATUS' )";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))///using可以確保程式碼結束該區塊時，自動關閉連線
            {
                ///開啟connection
                conn.Open();

                ///宣告一個SqlCommand物件
                SqlCommand cmd = new SqlCommand(sql, conn);

                ///宣告一個SqlDataAdapter 並傳入SqlCommand物件
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);

                ///填入資料
                sqlAdapter.Fill(dt);
                conn.Close();///關閉connection
            }

            return this.MapCodeData(dt);///MapCodeData 拆解字串
        }

        /// <summary>
        /// Map 借閱狀態指定形式 
        /// </summary>
        /// <returns></returns>
        private List<SelectListItem> MapCodeData(DataTable dt)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            result.Add(new SelectListItem()
            {
                Text = "請選擇",
                Value = ""
            });
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new SelectListItem()
                {
                    Text = row["CODE_NAME"].ToString(),
                    Value = row["CODE_ID"].ToString()
                });
            }

            return result;
        }
    }
}
