//using Core_Sample_Pra.Models;
//using Microsoft.AspNetCore.Mvc;
//using System.Diagnostics;
//using Core_Sample_Pra.IServer;
//using Core_Sample_Pra.ServiceModel;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using System;

//namespace Core_Sample_Pra.Controllers
//{
//    public class MemberController : Controller
//    {
//        private readonly ILogger<MemberController> _logger;
//        private readonly IMemberService _meberService;
//        public MemberController(ILogger<MemberController> _Logger, IMemberService _MeberService)
//        {
//            _meberService = _MeberService;
//            _logger = _Logger;
//        }
//        private void InitIndex()
//        {
//            List<SelectListItem> items = new List<SelectListItem>
//            {
//            new SelectListItem { Value = "M", Text = "男" },
//            new SelectListItem { Value = "F", Text = "女" },

//            };
//            ViewBag.SelectListItem = items;
//        }
//        /// <summary>
//        /// 首頁
//        /// </summary>
//        /// <returns></returns>
//        public IActionResult Index()
//        {
//            InitIndex();
//            Members memberdata = new Members();
//            memberdata.memberList = _meberService.GetAllMember();
//            return View(memberdata);
//        }
//        /// <summary>
//        /// 新增員工
//        /// </summary>
//        /// <param name="member"></param>
//        /// <returns></returns>
//        public IActionResult InsertMember(Member member)
//        {
//            MessageModel messageModel = new MessageModel();
//            _meberService.InsertMember(member, out messageModel);
//            return  RedirectToAction("Index");
//        }
//        /// <summary>
//        /// 查詢畫面
//        /// </summary>
//        /// <param name="filter"></param>
//        /// <returns></returns>
//        [HttpPost]
//        public IActionResult Index(FilterMember filterMember)
//        {
//            InitIndex();
//            Members memberdata = new Members();
//            memberdata.filterMember = filterMember;
//            memberdata.memberList = _meberService.SearchMembers(filterMember);
//            return View(memberdata);
//        }
//        /// <summary>
//        /// 刪除員工
//        /// </summary>
//        /// <param name="guid"></param>
//        /// <returns></returns>
//        public IActionResult MemberDelete(Guid guid)
//        {
//            MessageModel messageModel = new MessageModel();
//            _meberService.DeleteMember(guid,out messageModel);
//            return Json(messageModel);
//        }
//        public IActionResult GetMemberData(Guid guid)
//        {
//            MessageModel messageModel = new MessageModel();
//             var data =_meberService.GetMemberData(guid);
//            return Json(data);
//        }
        
//        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//        public IActionResult Error()
//        {
//            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//        }
//    }
//}