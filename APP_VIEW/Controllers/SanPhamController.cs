using APP_DATA.DTO;
using APP_VIEW.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;

namespace APP_VIEW.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly ISanPhamService _sanPhamService;
        private readonly IChatLieuService _chatLieuService;
        private readonly IHangService _hangService;
        private readonly IMauSacService _mauSacService;
        private readonly ILoaiSanPhamService _loaiSanPhamService;

        public SanPhamController(ISanPhamService sanPhamService, IChatLieuService chatLieuService, IHangService hangService, IMauSacService mauSacService, ILoaiSanPhamService loaiSanPhamService)
        {
            _sanPhamService = sanPhamService;
            _chatLieuService = chatLieuService;
            _hangService = hangService;
            _mauSacService = mauSacService;
            _loaiSanPhamService = loaiSanPhamService;
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

            List<HangResponse> hangs = await _hangService.GetAllHang();
            ViewBag.Hangs = hangs.Select(temp => new SelectListItem()
            {
                Text = temp.TenHang,
                Value = temp.ID_Hang.ToString()
            });

            List<MauSacResponse> mauSacs = await _mauSacService.GetAllMauSac();
            ViewBag.MauSacs = mauSacs.Select(temp => new SelectListItem()
            {
                Text = temp.TenMauSac,
                Value = temp.ID_MauSac.ToString()
            });

            List<LoaiSanPhamResponse> loaiSanPhams = await _loaiSanPhamService.GetAllLoaiSanPham();
            ViewBag.LoaiSanPhams = loaiSanPhams.Select(temp => new SelectListItem()
            {
                Text = temp.TenLoaiSP,
                Value = temp.ID_LoaiSP.ToString()
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

            List<HangResponse> hangs = await _hangService.GetAllHang();
            ViewBag.Hangs = hangs.Select(temp => new SelectListItem()
            {
                Text = temp.TenHang,
                Value = temp.ID_Hang.ToString()
            });

            List<MauSacResponse> mauSacs = await _mauSacService.GetAllMauSac();
            ViewBag.MauSacs = mauSacs.Select(temp => new SelectListItem()
            {
                Text = temp.TenMauSac,
                Value = temp.ID_MauSac.ToString()
            });

            List<LoaiSanPhamResponse> loaiSanPhams = await _loaiSanPhamService.GetAllLoaiSanPham();
            ViewBag.LoaiSanPhams = loaiSanPhams.Select(temp => new SelectListItem()
            {
                Text = temp.TenLoaiSP,
                Value = temp.ID_LoaiSP.ToString()
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
