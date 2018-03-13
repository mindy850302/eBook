using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
///For Display
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace eBook.Models
{
    public class Book
    {
       
        /// <summary>
        /// 書籍編號
        /// </summary>
        [DisplayName("書籍編號")]
        public int BOOK_ID { get; set; }

        /// <summary>
        /// 書籍名稱
        /// </summary>
        [DisplayName("書籍名稱")]
        [Required(ErrorMessage = "此欄位必填")]
        public string BOOK_NAME { get; set; }

        /// <summary>
        /// 書籍分類
        /// </summary>
        [DisplayName("圖書類別")]
        [Required(ErrorMessage = "此欄位必填")]
        public string BOOK_CLASS_ID { get; set; }

        /// <summary>
        /// 書籍作者
        /// </summary>
        [DisplayName("書籍作者")]
        [Required(ErrorMessage = "此欄位必填")]
        public string BOOK_AUTHOR { get; set; }

        /// <summary>
        /// 購入日期
        /// </summary>
        [DisplayName("購書日期")]
        [Required(ErrorMessage = "此欄位必填")]
        public string BOOK_BOUGHT_DATE { get; set; }

        /// <summary>
        /// 出版商
        /// </summary>
        [DisplayName("出版商")]
        [Required(ErrorMessage = "此欄位必填")]
        public string BOOK_PUBLISHER { get; set; }

        /// <summary>
        /// 內容簡介
        /// </summary>
        [DisplayName("內容簡介")]
        [Required(ErrorMessage = "此欄位必填")]
        public string BOOK_NOTE { get; set; }

        /// <summary>
        /// 借閱狀態(是否可借出)
        /// </summary>
        [DisplayName("借閱狀態")]
        public string BOOK_STATUS { get; set; }

        /// <summary>
        /// 借閱狀態(是否可借出)
        /// </summary>
        [DisplayName("借閱代碼")]
        public string BOOK_STATUS_CODE { get; set; }

        /// <summary>
        /// 借閱人
        /// </summary>
        [DisplayName("借閱人")]
        public string BOOK_KEEPER { get; set; }

        /// <summary>
        /// 建立時間
        /// </summary>
        [DisplayName("建立時間")]
        public string CREATE_DATE { get; set; }

        /// <summary>
        /// 建立使用者
        /// </summary>
        [DisplayName("建立使用者")]
        public string CREATE_USER { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>
        [DisplayName("修改日期")]
        public string MODIFY_DATE { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>
        [DisplayName("修改使用者")]
        public string MODIFY_USER { get; set; }

    }
}