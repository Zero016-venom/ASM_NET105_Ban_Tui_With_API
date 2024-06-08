using APP_DATA.DTO;
using APP_VIEW.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace APP_VIEW.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly ISanPhamService _sanPhamService;
        private readonly IChatLieuService _chatLieuService;

        public SanPhamController(ISanPhamService sanPhamService, IChatLieuService chatLieuService)
        {
            _sanPhamService = sanPhamService;
            _chatLieuService = chatLieuService;
        }

        public async Task<IActionResult> Index()
        {
            List<SanPhamResponse> sanPhamResponses = await _sanPhamService.GetAllSanPhams();
            return View(sanPhamResponses);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            SanPhamResponse? sanPhamResponse = await _sanPhamService.GetSanPhamById(id);
            return View(sanPhamResponse);
        }

        public async Task<IActionResult> Create()
        {
            List<ChatLieuResponse> chatLieus = await _chatLieuService.GetAllChatLieu();
            ViewBag.ChatLieus = chatLieus.Select(temp => new SelectListItem()
            {
                Text = temp.TenChatLieu,
                Value = temp.ID_ChatLieu.ToString()
            });

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SanPhamAddRequest sanPhamAddRequest)
        {
            List<ChatLieuResponse> chatLieus = await _chatLieuService.GetAllChatLieu();
            ViewBag.ChatLieus = chatLieus.Select(temp => new SelectListItem()
            {
                Text = temp.TenChatLieu,
                Value = temp.ID_ChatLieu.ToString()
            });

            SanPhamResponse sanPhamResponse = await _sanPhamService.AddSanPham(sanPhamAddRequest);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            SanPhamResponse? sanPhamResponse = await _sanPhamService.GetSanPhamById(id);
            return View(sanPhamResponse);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SanPhamUpdateRequest sanPhamUpdateRequest)
        {
            SanPhamResponse sanPhamResponse = await _sanPhamService.UpdateSanPham(sanPhamUpdateRequest);
            return RedirectToAction("Index");
        }
    }
}
