using Core_Sample_Pra.IServer;
using Core_Sample_Pra.Models;
using Core_Sample_Pra.Repository;
using Core_Sample_Pra.Validator;
using Core_Sample_Pra.ViewModel;
using Dapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Reflection;

namespace Core_Sample_Pra.ServiceModel
{
    public class HomeService
    {
        private readonly IGSSWEBBOOK _gsswebbook;
        public HomeService(IGSSWEBBOOK _Gsswebbook)
        {
            _gsswebbook = _Gsswebbook;
        }
        public List<SelectListItem> GetBOOK_CLASS_NAME_Ddl()
        {
            var data = _gsswebbook.GetBOOK_CLASS_NAME_Ddl();
            var ddldata = GetDdl(data);
            return ddldata;
        }

        public List<SelectListItem> GetUSER_ENAME_Ddl()
        {
            var data = _gsswebbook.GetUSER_ENAME_Ddl();
            var ddldata = GetDdl(data);
            return ddldata;
        }
        public List<SelectListItem> BOOK_CODE_Ddl()
        {
            var data = _gsswebbook.BOOK_CODE_Ddl();
            var ddldata = GetDdl(data);
            return ddldata;
        }
        public List<V_IndexBook> GetBookData()
        {
            var data = _gsswebbook.GetBookData();

            return data;

        }
        public EditBookData GetEditBookData(int book_id)
        {
            var data = _gsswebbook.GetEditBookData(book_id);

            return data;

        }
        public bool SetBookEditData(EditBookData v_EditBook)
        {
            if (v_EditBook.BOOK_STATUS == "A")
            {
                v_EditBook.BOOK_KEEPER = "";
            }
            return _gsswebbook.SetBookEditData(v_EditBook);

        }
        public bool InsertBookData(EditBookData EditBook)
        {
            return  _gsswebbook.InsertBookData(EditBook) == 1;

        }
        public List<V_Lend_History> GetLend_History(int book_ID)
        {

            var data = _gsswebbook.GetLend_History(book_ID);
            return data;
        }

        public bool DeleteBook(int book_id)
        {
            return _gsswebbook.DeleteBook(book_id);
        }
        public Dictionary<string, string> GetBookDetail(int book_id)
        {
            var data = _gsswebbook.GetBookDetail(book_id);
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            PropertyInfo[] properties = typeof(V_Book_Detail).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            for (int i = 0; i < properties.Count(); i++)
            {
                string displayName = properties[i].GetCustomAttribute<DisplayNameAttribute>().DisplayName;
                string propertyValue = properties[i].GetValue(data) == null ? "" : properties[i].GetValue(data).ToString();
                dictionary.Add(displayName, propertyValue);
            }
            return dictionary;
        }
        public List<V_IndexBook> GetSearchBookData(Filter_V_IndexBook filter_V_IndexBook, string sortOrder, string sortDirection, out int count)
        {

            string condition = GetResultcondition(filter_V_IndexBook, sortOrder, sortDirection);
            var data = _gsswebbook.GetSearchBookData(filter_V_IndexBook, condition);
            count = data.Count();
            return data;
        }
        private List<SelectListItem> GetDdl(List<DdlModel> ddlModel)
        {

            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var ddl in ddlModel)
            {
                SelectListItem item = new SelectListItem

                { Value = ddl.DdlValue, Text = ddl.DdlText };
                ;
                items.Add(item);
            }
            return items;
        }
        private string GetResultcondition(Filter_V_IndexBook filter, string sortOrder, string sortDirection)
        {
            /*
             SELECT  
		 BD.BOOK_ID --AS '書本ID'
		,BCL.BOOK_CLASS_NAME --AS '書籍類別'
		,BCL.BOOK_CLASS_ID --查詢
		,ISNULL(BD.BOOK_NAME ,'-')AS BOOK_NAME
		,CONVERT(varchar(12), BD.BOOK_BOUGHT_DATE, 111)AS BOOK_BOUGHT_DATE  --AS '購買日期'
		,CONVERT(varchar(12), BLR.LEND_DATE, 111) AS  LEND_DATE--AS '借閱日期'
		,M.[USER_ID]
		,M.USER_CNAME
		,M.USER_ENAME
		,BCO.CODE_NAME
		,BCO.CODE_ID --查詢
		--,BLR.KEEPER_ID +'-'+M.USER_CNAME+'('+M.USER_ENAME+')' AS '借閱人'
		--,STUFF('-'+BCO.CODE_NAME,1,0, BCO.CODE_ID ) AS '狀態'
		--, FORMAT(BD.BOOK_AMOUNT, 'N0')+'元' AS '購書金額'
FROM [dbo].BOOK_DATA BD WITH(NOLOCK)
INNER JOIN BOOK_CLASS BCL  WITH(NOLOCK) ON BCL.BOOK_CLASS_ID = BD.BOOK_CLASS_ID
INNER JOIN BOOK_CODE BCO  WITH(NOLOCK) ON BCO.CODE_ID=BD.BOOK_STATUS  AND   BCO.CODE_TYPE='BOOK_STATUS'
LEFT JOIN [BOOK_LEND_RECORD] BLR  WITH(NOLOCK) ON BD.BOOK_ID=BLR.BOOK_ID
LEFT JOIN MEMBER_M M  WITH(NOLOCK) ON M.[USER_ID]=BD.BOOK_KEEPER 
             */
            List<string> conditions = new List<string>();
            conditions.Add(" WHERE 1=1 ");
            string Sort = "";
            if (string.IsNullOrWhiteSpace(sortOrder))
            {
                Sort = " BD.BOOK_BOUGHT_DATE  DESC";
            }
            else
            {
                Sort = sortOrder + " " + sortDirection;

            }

            if (!string.IsNullOrWhiteSpace(filter.BOOK_NAME))
            {
                filter.BOOK_NAME = "%" + filter.BOOK_NAME.Trim() + "%";
                conditions.Add(" BD.BOOK_NAME LIKE @BOOK_NAME ");
            }
            if (!string.IsNullOrWhiteSpace(filter.BOOK_CLASS_ID))
            {
                conditions.Add(" BCL.BOOK_CLASS_ID = @BOOK_CLASS_ID ");
            }
            if (!string.IsNullOrWhiteSpace(filter.USER_ID))
            {
                conditions.Add(" M.USER_ID LIKE @USER_ID ");
            }

            if (!string.IsNullOrWhiteSpace(filter.CODE_ID))
            {
                conditions.Add(" BCO.CODE_ID = @CODE_ID ");
            }

            string conditionstring = string.Join("AND", conditions) + " ORDER BY " + Sort;
            return conditionstring;
        }

        public MessageResult ValidatorEditBookData(EditBookData data)
        {
            MessageResult result = new MessageResult();
            result.status = true;
            var validator = new EditBookDataValidator();
            var validationResult = validator.Validate(data);
            // 如果沒有通過檢查，就把訊息串一串丟回去
            if (validationResult.IsValid is false)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage);
                var resultMessage = string.Join("\n", errorMessages);
                result.message = resultMessage;
                result.status = false;
            }
            return result;
        }
    }

}
