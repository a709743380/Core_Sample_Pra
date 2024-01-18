using System.ComponentModel;

namespace Core_Sample_Pra.ViewModel
{
    public class V_Lend_History
    {


        [DisplayName("借閱日期")]
        public string Lend_Date { get; set; }
        //[DisplayName("書籍類別")]
        //public string Book_Class_Name { get; set; }
        [DisplayName("借閱人員ID")]
        public string Borrower_ID { get; set; }
        [DisplayName("英文名字")]
        public string USER_ENAME { get; set; }
        [DisplayName("中文名字")]
        public string USER_CNAME { get; set; }

    }
}
