using Core_Sample_Pra.IServer;
using Newtonsoft.Json;
using System.ComponentModel;

namespace Core_Sample_Pra.Models
{
    public class BOOK
    {
        public BOOK_CLASS Book_Class { get; set; }
        public BOOK_CODE Book_Code { get; set; }
        public BOOK_DATA Book_Data { get; set; }
        public BOOK_LEND_RECORD Book_Lend_Rrecode { get; set; }
        public MEMBER_M Member_m { get; set; }
        public List<BOOK_DATA> Book_DataList { get; set; }
        public SPAN_TABLE Span_Table { get; set; }
        public BOOK()
        {
            Book_Class = new BOOK_CLASS();
            Book_Code = new BOOK_CODE();
            Book_Data = new BOOK_DATA();
            Book_DataList = new List<BOOK_DATA>();
            Book_Lend_Rrecode = new BOOK_LEND_RECORD();
            Member_m = new MEMBER_M();
            Span_Table = new SPAN_TABLE();
        }
    }
    public class BOOK_CLASS
    {
        public string BOOK_CLASS_ID { get; set; }
        public string BOOK_CLASS_NAME { get; set; }
        public DateTime CREATE_DATE { get; set; }
        public string CREATE_USER { get; set; }
        public DateTime MODIFY_DATE { get; set; }
        public string MODIFY_USER { get; set; }
    }
    public class BOOK_CODE
    {
        public string CODE_TYPE { get; set; }
        public string CODE_ID { get; set; }
        public string CODE_TYPE_DESC { get; set; }
        public string CODE_NAME { get; set; }
        public DateTime CREATE_DATE { get; set; }
        public string CREATE_USER { get; set; }
        public DateTime MODIFY_DATE { get; set; }
        public string MODIFY_USER { get; set; }
    }
    public class BOOK_DATA
    {
        [DisplayName("書本ID")]
        public int BOOK_ID { get; set; }
        [DisplayName("書名")]
        public string BOOK_NAME { get; set; }
        [DisplayName("圖書類別")]
        public string BOOK_CLASS_ID { get; set; }
        [DisplayName("作者")]
        public string BOOK_AUTHOR { get; set; }
        [DisplayName("購書日期")]
        public DateTime BOOK_BOUGHT_DATE { get; set; }
        [DisplayName("出版商")]
        public string BOOK_PUBLISHER { get; set; }
        [DisplayName("內容簡介")]
        public string BOOK_NOTE { get; set; }
        [DisplayName("借閱狀態")]
        public string BOOK_STATUS { get; set; }
        [DisplayName("借閱人")]
        public string BOOK_KEEPER { get; set; }
        public int BOOK_AMOUNT { get; set; }
        public DateTime CREATE_DATE { get; set; }
        public string CREATE_USER { get; set; }
        public DateTime MODIFY_DATE { get; set; }
        public string MODIFY_USER { get; set; }

        public bool IsAdd { get; set; } = false;
    }
    public class BOOK_LEND_RECORD
    {
        public int IDENTITY_FILED { get; set; }
        public int BOOK_ID { get; set; }
        public string KEEPER_ID { get; set; }
        public DateTime LEND_DATE { get; set; }
        public DateTime CRE_DATE { get; set; }
        public string CRE_USR { get; set; }
        public DateTime MOD_DATE { get; set; }
        public string MOD_USR { get; set; }
    }
    public class MEMBER_M
    {
        public string USER_ID { get; set; }
        public string USER_CNAME { get; set; }
        public string USER_ENAME { get; set; }
        public DateTime CREATE_DATE { get; set; }
        public string CREATE_USER { get; set; }
        public DateTime MODIFY_DATE { get; set; }
        public string MODIFY_USER { get; set; }
    }
    public class SPAN_TABLE
    {
        public int IDENTITY_FILED { get; set; }
        public string SPAN_YEAR { get; set; }
        public string SPAN_START { get; set; }
        public string SPAN_END { get; set; }
        public string NOTE { get; set; }
        public DateTime CRE_DATE { get; set; }
        public string CRE_USR { get; set; }
        public DateTime MOD_DATE { get; set; }
        public string MOD_USR { get; set; }
    }

}