using Core_Sample_Pra.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Xml.Linq;
using Newtonsoft.Json;
using Core_Sample_Pra.ServiceModel;
using Core_Sample_Pra.ViewModel;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.NetworkInformation;
using PagedList;
using PagedList.Mvc;
using X.PagedList.Mvc.Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;
using System.Reflection.Metadata;
using Core_Sample_Pra.Validator;

namespace Core_Sample_Pra.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private HomeService homeService;

        public HomeController(ILogger<HomeController> _Logger, HomeService _HomeService)
        {
            homeService = _HomeService;
            _logger = _Logger;
        }
        private void init_Index()
        {

            ViewBag.USER_ENAME = homeService.GetUSER_ENAME_Ddl();
            ViewBag.BOOK_CODE = homeService.BOOK_CODE_Ddl();
            ViewBag.BOOK_CLASS_NAME = homeService.GetBOOK_CLASS_NAME_Ddl();
            List<SelectListItem> items = new List<SelectListItem>() {
                new SelectListItem { Value = "10",  Text = "10" },
                new SelectListItem { Value = "50",  Text = "50" },
                new SelectListItem { Value = "100", Text = "100" }
            };
            ViewBag.TakePageNumber = items;
        }
        public IActionResult Index(Filter_V_IndexBook filter_V_IndexBook, int page, string sortOrder, string sortDirection)
        {
            init_Index();

            ViewBag.CurrentSort = sortOrder == "" ? "BOOK_BOUGHT_DATE" : sortOrder;
            ViewBag.CurrentDirection = sortDirection == "" ? "DESC" : sortDirection;

            IndexBook indexBook = new IndexBook();
            //分頁處理
            filter_V_IndexBook.TakePageNumber = filter_V_IndexBook.TakePageNumber == 0 ? 10 : filter_V_IndexBook.TakePageNumber;
            int TakePageNumber = filter_V_IndexBook.TakePageNumber;
            page = page < 1 ? 1 : page;
            filter_V_IndexBook.CurrentPage = page;
            int takenum = page - 1;
            ViewData["PageNumber"] = TakePageNumber;
            //取得資料
            indexBook.v_IndexBook_List = homeService.GetSearchBookData(filter_V_IndexBook, sortOrder, sortDirection, out int count)
                                                    .Skip(takenum * TakePageNumber)
                                                    .Take(filter_V_IndexBook.TakePageNumber)
                                                    .ToList();
            int totalpage = count / TakePageNumber;
            filter_V_IndexBook.TotalPage = totalpage < 1 ? 1 : totalpage;
            //賦值參數
            indexBook.filter_V_IndexBook = filter_V_IndexBook;


            return View(indexBook);
        }

        [HttpGet]
        public IActionResult GetEditBookData(int book_id)
        {
            init_Index();
            IndexBook indexBook = new IndexBook();
            //取得查詢結果 分頁佔時沒做
            indexBook.EditBook = homeService.GetEditBookData(book_id);
            return Json(indexBook.EditBook);
        }
        [HttpPut]
        public IActionResult SetEditData(EditBookData EditBook)
        {
            var validResult = homeService.ValidatorEditBookData(EditBook);
            if(!validResult.status)
                return Json(validResult); 

            bool  isUpdate= homeService.SetBookEditData(EditBook);
            MessageResult updateResult = new MessageResult()
            {
                status = isUpdate,
                message = isUpdate ? "修改成功" : "修改失敗"
            };
            return Json(updateResult);
        }
        [HttpPost]
        public IActionResult Book(EditBookData EditBook)
        {
            // 這邊需要對參數做檢查
            EditBook.IsAdd = true;
            var validResult = homeService.ValidatorEditBookData(EditBook);
            if (!validResult.status)
                return Json(validResult);


            bool isInsert = homeService.InsertBookData(EditBook);
            MessageResult updateResult = new MessageResult()
            {
                status = isInsert,
                message = isInsert ? "新增成功" : "新增失敗"
            };
            return Json(updateResult);
        }
        [HttpGet]
        public IActionResult BookDetail(int book_id)
        {
            var data = homeService.GetBookDetail(book_id);

            return Json(data);
        }
        [HttpPatch]
        public IActionResult BookHistory(int book_id)
        {
            var data = homeService.GetLend_History(book_id);

            return Json(data);
        }
        [HttpDelete]
        public IActionResult Book(int book_id)
        {
            var data = homeService.DeleteBook(book_id);

            return Json(data);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}