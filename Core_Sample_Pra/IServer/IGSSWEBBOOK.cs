using Core_Sample_Pra.Models;
using Core_Sample_Pra.ViewModel;
using System.Diagnostics.Metrics;
using System.Globalization;

namespace Core_Sample_Pra.IServer
{

    public interface IGSSWEBBOOK
    {
        //取得 BOOK_CLASS_NAME
        List<DdlModel> GetBOOK_CLASS_NAME_Ddl();
        //取得USER_ENAME
        List<DdlModel> GetUSER_ENAME_Ddl();
        List<DdlModel> BOOK_CODE_Ddl();
        List<V_IndexBook> GetBookData();
        List<V_IndexBook> GetSearchBookData(Filter_V_IndexBook filter_V_IndexBook , string condition);

        public EditBookData GetEditBookData(int book_id);
        public bool SetBookEditData(EditBookData v_EditBook);
        public int InsertBookData(EditBookData EditBook);
        public bool DeleteBook(int book_id);
        
        public List<V_Lend_History> GetLend_History(int book_ID);
        public V_Book_Detail GetBookDetail(int book_id);
    }

}