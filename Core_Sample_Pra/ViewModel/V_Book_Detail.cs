using System.ComponentModel;

namespace Core_Sample_Pra.ViewModel
{


    public class V_Book_Detail
    {
        [DisplayName("書本ID")]
        public int BOOK_ID { get; set; }
        [DisplayName("書名")]
        public string BOOK_NAME { get; set; }
        [DisplayName("圖書類別")]
        public string BOOK_CLASS_NAME { get; set; }
        [DisplayName("作者")]
        public string BOOK_AUTHOR { get; set; }
        [DisplayName("購書日期")]
        public String BOOK_BOUGHT_DATE { get; set; }
        [DisplayName("出版商")]
        public string BOOK_PUBLISHER { get; set; }
        [DisplayName("內容簡介")]
        public string BOOK_NOTE { get; set; }
        [DisplayName("書本狀態")]
        public string CODE_NAME { get; set; }
        [DisplayName("借閱人ID")]
        public string Borrower_ID { get; set; }
        [DisplayName("借閱人")]
        public string Name { get; set; }
        [DisplayName("購書金額")]
        public string BOOK_AMOUNT { get; set; }

        //public DateTime CREATE_DATE { get; set; }

        //public string CREATE_USER { get; set; }

        //public DateTime MODIFY_DATE { get; set; }

        //public string MODIFY_USER { get; set; }
    }
}
