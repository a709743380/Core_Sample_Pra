using Core_Sample_Pra.IServer;
using Core_Sample_Pra.Models;
using Core_Sample_Pra.ViewModel;
using Dapper;
using System.Data;


namespace Core_Sample_Pra.Repository
{

    public class HomeRepository : IGSSWEBBOOK
    {
        private readonly IDbConnection _dbConnection;
        public HomeRepository(IDbConnection _DbConnection)
        {
            _dbConnection = _DbConnection;
        }
        public List<DdlModel> GetBOOK_CLASS_NAME_Ddl()
        {


            string sqlquery = @"
                    SELECT [BOOK_CLASS_ID] AS 'DdlValue',
                        [BOOK_CLASS_NAME] AS 'DdlText' 
                    FROM BOOK_CLASS  WITH(NOLOCK)";

            var data = _dbConnection.Query<DdlModel>(sqlquery);

            return data.ToList();

        }

        public List<DdlModel> GetUSER_ENAME_Ddl()
        {

            string sqlquery = @"
                    SELECT M.[USER_ID] AS 'DdlValue',
                        M.USER_CNAME+'('+M.USER_ENAME+')' AS 'DdlText'
                    FROM MEMBER_M M WITH(NOLOCK)";

            var data = _dbConnection.Query<DdlModel>(sqlquery);

            return data.ToList();

        }
        public List<DdlModel> BOOK_CODE_Ddl()
        {


            string sqlquery = "SELECT [CODE_ID] AS 'DdlValue',[CODE_NAME] AS 'DdlText' FROM [BOOK_CODE] WITH(NOLOCK) WHERE BOOK_CODE. CODE_TYPE= 'BOOK_STATUS'";

            var data = _dbConnection.Query<DdlModel>(sqlquery);

            return data.ToList();

        }

        public List<V_IndexBook> GetBookData()
        {


            string sqlquery = @"
                        SELECT  
                             BD.BOOK_ID --AS '書本ID',
                             BCL.BOOK_CLASS_NAME --AS '書籍類別',
                             BCL.BOOK_CLASS_ID --查詢,
                             ISNULL(BD.BOOK_NAME ,'-')AS BOOK_NAME,
                             CONVERT(varchar(12), BD.BOOK_BOUGHT_DATE, 111)AS BOOK_BOUGHT_DATE  --AS '購買日期',
                             CONVERT(varchar(12), BLR.LEND_DATE, 111) AS  LEND_DATE--AS '借閱日期',
                             M.[USER_ID],
                             M.USER_CNAME,
                             M.USER_ENAME,
                             BCO.CODE_NAME,
                             BCO.CODE_ID   --查詢
                            --,BLR.KEEPER_ID +'-'+M.USER_CNAME+'('+M.USER_ENAME+')' AS '借閱人'
                            --,STUFF('-'+BCO.CODE_NAME,1,0, BCO.CODE_ID ) AS '狀態'
                            --, FORMAT(BD.BOOK_AMOUNT, 'N0')+'元' AS '購書金額'
                        FROM [dbo].BOOK_DATA BD WITH(NOLOCK)
                        INNER JOIN BOOK_CLASS BCL  WITH(NOLOCK) ON BCL.BOOK_CLASS_ID = BD.BOOK_CLASS_ID
                        INNER JOIN BOOK_CODE BCO  WITH(NOLOCK) ON BCO.CODE_ID=BD.BOOK_STATUS  AND   BCO.CODE_TYPE='BOOK_STATUS'
                        LEFT JOIN [BOOK_LEND_RECORD] BLR  WITH(NOLOCK) ON BD.BOOK_ID=BLR.BOOK_ID
                        LEFT JOIN MEMBER_M M  WITH(NOLOCK) ON M.[USER_ID]=BD.BOOK_KEEPER 
";

            var data = _dbConnection.Query<V_IndexBook>(sqlquery);

            return data.ToList();

        }
        public List<V_IndexBook> GetSearchBookData(Filter_V_IndexBook filter_V_IndexBook, string condition)
        {

            string sqlquery = @"
                        SELECT  
                             BD.BOOK_ID ,
                             BCL.BOOK_CLASS_NAME ,
                             BCL.BOOK_CLASS_ID ,
                             ISNULL(BD.BOOK_NAME ,'-')AS BOOK_NAME,
                             CONVERT(varchar(12), BD.BOOK_BOUGHT_DATE, 111)AS BOOK_BOUGHT_DATE  ,
                             CONVERT(varchar(12), BLR.LEND_DATE, 111) AS  LEND_DATE,
                             M.[USER_ID],
                             M.USER_CNAME,
                             M.USER_ENAME,
                             BCO.CODE_NAME,
                             BCO.CODE_ID  
                            
                        FROM [dbo].BOOK_DATA BD WITH(NOLOCK)
                        INNER JOIN BOOK_CLASS BCL  WITH(NOLOCK) ON BCL.BOOK_CLASS_ID = BD.BOOK_CLASS_ID
                        INNER JOIN BOOK_CODE BCO  WITH(NOLOCK) ON BCO.CODE_ID=BD.BOOK_STATUS  AND   BCO.CODE_TYPE='BOOK_STATUS'
                        LEFT JOIN [BOOK_LEND_RECORD] BLR  WITH(NOLOCK) ON BD.BOOK_ID=BLR.BOOK_ID
                        LEFT JOIN MEMBER_M M  WITH(NOLOCK) ON M.[USER_ID]=BD.BOOK_KEEPER " + condition;

            var data = _dbConnection.Query<V_IndexBook>(sqlquery, filter_V_IndexBook);

            return data.ToList();

        }
        public EditBookData GetEditBookData(int book_id)
        {
            string sqlquery = @"
                        SELECT 
                               [BOOK_ID],
                               [BOOK_NAME],
                               [BOOK_CLASS_ID],
                               [BOOK_AUTHOR],
                               [BOOK_BOUGHT_DATE],
                               [BOOK_PUBLISHER],
                               [BOOK_NOTE],
                               [BOOK_STATUS],
                               [BOOK_KEEPER],
                               [BOOK_AMOUNT],
                               [CREATE_DATE],
                               [CREATE_USER],
                               [MODIFY_DATE],
                               [MODIFY_USER]      
                        FROM BOOK_DATA  WHERE BOOK_ID=@BOOK_ID";

            var data = _dbConnection.Query<EditBookData>(sqlquery, new { @BOOK_ID = book_id }).FirstOrDefault();

            return data;
        }
        public V_Book_Detail GetBookDetail(int book_id)
        {
            string sqlquery = @"
                        SELECT 
                        	   [BOOK_ID],
                               [BOOK_NAME],
                               BCL.BOOK_CLASS_NAME,
                               [BOOK_AUTHOR],
                               CONVERT(varchar(10), [BOOK_BOUGHT_DATE], 111) AS [BOOK_BOUGHT_DATE],
                               [BOOK_PUBLISHER],
                               [BOOK_NOTE],
                               BCO.[CODE_NAME],
                        	   M.[USER_ID] AS 'Borrower_ID',
                               M.USER_CNAME+'('+USER_ENAME+')'AS [Name],
                               FORMAT(BOOK_AMOUNT, 'N0') as BOOK_AMOUNT,
                               BD.[CREATE_DATE],
                               BD.[CREATE_USER],
                               BD.[MODIFY_DATE],
                               BD.[MODIFY_USER]
                        FROM [GSSWEB].[dbo].[BOOK_DATA] BD 
                        INNER JOIN BOOK_CLASS BCL ON BD.[BOOK_CLASS_ID]=BCL.BOOK_CLASS_ID
                        INNER JOIN BOOK_CODE BCO ON BD.[BOOK_STATUS]=BCO.CODE_ID
                        LEFT JOIN MEMBER_M M ON BD.BOOK_KEEPER=M.[USER_ID]  WHERE BOOK_ID=@BOOK_ID";

            var data = _dbConnection.Query<V_Book_Detail>(sqlquery, new { @BOOK_ID = book_id }).FirstOrDefault();

            return data;
        }
        public List<V_Lend_History> GetLend_History(int book_ID)
        {
            string sqlquery = @"
                        SELECT 
                            [Lend_Date],
                            [Borrower_ID],
                            [USER_ENAME],
                            [USER_CNAME]
                        FROM V_Lend_History  WHERE Book_ID=@Book_ID ORDER BY Lend_Date DESC";

            var data = _dbConnection.Query<V_Lend_History>(sqlquery, new { Book_ID = book_ID }).ToList();

            return data;

        }
        public bool SetBookEditData(EditBookData v_EditBook)
        {
            string sqlquery = @"
                    UPDATE [BOOK_DATA]
                    SET  
                      [BOOK_NAME]=@BOOK_NAME,
                      [BOOK_CLASS_ID]=@BOOK_CLASS_ID,
                      [BOOK_AUTHOR]=@BOOK_AUTHOR,
                      [BOOK_BOUGHT_DATE]=@BOOK_BOUGHT_DATE,
                      [BOOK_PUBLISHER]=@BOOK_PUBLISHER,
                      [BOOK_NOTE]=@BOOK_NOTE,
                      [BOOK_STATUS]=@BOOK_STATUS,
                      [BOOK_KEEPER]=@BOOK_KEEPER
                    WHERE [BOOK_ID]=@BOOK_ID

                                ";

            var data = _dbConnection.Execute(sqlquery, v_EditBook);
            if (data > 0)
            {
                return true;
            }
            else { return false; }

        }
        public int InsertBookData(EditBookData EditBook)
        {
            string sqlquery = @"INSERT INTO BOOK_DATA
                        (
                                [BOOK_NAME]       ,
                                [BOOK_CLASS_ID]   ,
                                [BOOK_AUTHOR]     ,
                                [BOOK_BOUGHT_DATE],
                                [BOOK_PUBLISHER]  ,
                                [BOOK_NOTE]       ,
                                [BOOK_STATUS]     ,
                                [BOOK_KEEPER]     ,
                                [BOOK_AMOUNT]     ,
                                [CREATE_DATE]     ,
                                [CREATE_USER]     ,
                                [MODIFY_DATE]     ,
                                [MODIFY_USER]     
                        )VALUES(
                                @BOOK_NAME        ,
                                @BOOK_CLASS_ID   ,
                                @BOOK_AUTHOR     ,
                                @BOOK_BOUGHT_DATE,
                                @BOOK_PUBLISHER  ,
                                @BOOK_NOTE       ,
                                'A'              ,
                                ' '              ,
                                '1'              ,
                                GETDATE()        ,
                                '1'              ,
                                GETDATE()        ,
                                '1'

                                ) ";

            var data = _dbConnection.Execute(sqlquery, EditBook);
            return data;
        }

        public bool DeleteBook(int book_id)
        {
            string sqlquery = "DELETE FROM  BOOK_DATA  WHERE BOOK_ID=@BOOK_ID";

            var data = _dbConnection.Execute(sqlquery, new { @BOOK_ID = book_id });
            if (data > 0)
            {
                return true;
            }
            else { return false; }
        }
    }
}
