using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace eBook.Models
{
    public class ClassServices
    {
        private string GetDBConnectionString()
        {
            return
                System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString.ToString();
        }

        /// <summary>
        /// 取得圖書類別代號及名稱
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetClassTable()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT BOOK_CLASS_ID
                              ,BOOK_CLASS_NAME
                          FROM GSSWEB.dbo.BOOK_CLASS";
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
        /// Map 圖書類別指定形式
        /// </summary>
        /// <returns></returns>
        private List<SelectListItem> MapCodeData(DataTable dt)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new SelectListItem()
                {
                    Text = row["BOOK_CLASS_ID"].ToString() + '-' + row["BOOK_CLASS_NAME"].ToString(),
                    Value = row["BOOK_CLASS_ID"].ToString()
                });
            }
            return result;
        }

        /// <summary>
        /// 取得借閱人中文及英文名稱 
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetKeeperTable()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT USER_ID
                              ,USER_CNAME
                              ,USER_ENAME
                          FROM GSSWEB.dbo.MEMBER_M";
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

            return this.MapKeeperData(dt);///MapCodeData 拆解字串
        }
        
        /// <summary>
        /// Map 借閱人指定形式 
        /// </summary>
        /// <returns></returns>
        private List<SelectListItem> MapKeeperData(DataTable dt)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new SelectListItem()
                {
                    Text = row["USER_ENAME"].ToString() + '(' + row["USER_CNAME"].ToString() + ')',
                    Value = row["USER_ID"].ToString()
                });
            }
            return result;
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
                          FROM GSSWEB.dbo.BOOK_CODE";
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

            return this.MapStatusData(dt);///MapCodeData 拆解字串
        }

        /// <summary>
        /// Map 借閱狀態指定形式 
        /// </summary>
        /// <returns></returns>
        private List<SelectListItem> MapStatusData(DataTable dt)
        {
            List<SelectListItem> result = new List<SelectListItem>();
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