using Core_Sample_Pra.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Core_Sample_Pra.ViewModel
{
    public class IndexBook
    {
        public V_IndexBook v_IndexBook { get; set; }
        public List<V_IndexBook> v_IndexBook_List { get; set; }
        public EditBookData EditBook { get; set; }
        public Filter_V_IndexBook filter_V_IndexBook { get; set; }

        public IndexBook()
        {
            v_IndexBook = new V_IndexBook();
            v_IndexBook_List = new List<V_IndexBook>();
            EditBook = new EditBookData();
            filter_V_IndexBook = new Filter_V_IndexBook();
        }
    }
    public class V_IndexBook
    {
        public int BOOK_ID { get; set; }

        [Display(Name = "圖書類別")]
        public string BOOK_CLASS_NAME { get; set; }
        public string BOOK_CLASS_ID { get; set; }
        [Display(Name = "書名")]
        public string BOOK_NAME { get; set; }
        [Display(Name = "購書日期")]
        public string BOOK_BOUGHT_DATE { get; set; }
        [Display(Name = "借閱狀態")]
        public string CODE_NAME { get; set; }
        public string CODE_ID { get; set; }
        public string? LEND_DATE { get; set; }
        public string USER_ID { get; set; }
        [Display(Name ="中文名字")]
        public string USER_CNAME { get; set; }
        [Display(Name = "借閱人")]
        public string USER_ENAME { get; set; }


    }
    public class Filter_V_IndexBook : V_IndexBook
    {
        public int TakePageNumber { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }


    }
    public class EditBookData : BOOK_DATA
    {
        //public int BOOK_ID { get; set; }
        //public string BOOK_NAME { get; set; }
        //public string BOOK_AUTHOR { get; set; }
        //public string BOOK_PUBLISHER { get; set; }
        //public string BOOK_NOTE { get; set; }
        //public string BOOK_BOUGHT_DATE { get; set; }
        //public string BOOK_CLASS_ID { get; set; }
        //public string BOOK_CLASS_NAME { get; set; }
        //public string CODE_ID { get; set; }
        //public string CODE_NAME { get; set; }
        //public string USER_ID { get; set; }
        //public string USER_ENAME { get; set; }

    }
}
