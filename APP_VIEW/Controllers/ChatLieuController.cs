using APP_DATA.Models;
using APP_VIEW.IServices;
using Microsoft.AspNetCore.Mvc;

namespace APP_VIEW.Controllers
{
    public class ChatLieuController : Controller
    {
        private readonly IChatLieuService _iChatLieuService;

        public ChatLieuController(IChatLieuService ichatLieuService)
        {
            _iChatLieuService = ichatLieuService;
        }

        public IActionResult Index()
        {
            List<ChatLieu> chatLieus = _iChatLieuService.GetAllChatLieu();
            return View(chatLieus);
        }

        public IActionResult Details(Guid id)
        {
            ChatLieu? matchingChatLieu = _iChatLieuService.GetChatLieuById(id);
            return View(matchingChatLieu);
        }

        public IActionResult Create(ChatLieu chatLieu)
        {
            return View(chatLieu);
        }

        [HttpPost]
        public IActionResult CreateChatLieu(ChatLieu chatLieu)
        {
            _iChatLieuService.CreateChatLieu(chatLieu);
            return View(chatLieu);
        }

        public IActionResult Edit(Guid id)
        {
            ChatLieu matchingChatLieu = _iChatLieuService.GetChatLieuById(id);
            return View(matchingChatLieu);
        }

        [HttpPost]
        public ActionResult EditChatLieu(ChatLieu chatLieu)
        {
            _iChatLieuService.UpdateChatLieu(chatLieu);
            return RedirectToAction("Index");
        }
    }
}
