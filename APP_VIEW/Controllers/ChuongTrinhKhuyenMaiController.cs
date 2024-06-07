using APP_DATA.DTO;
using APP_VIEW.IServices;
using Microsoft.AspNetCore.Mvc;

namespace APP_VIEW.Controllers
{
    public class ChuongTrinhKhuyenMaiController : Controller
    {
        private readonly IChuongTrinhKhuyenMaiService _ichuongTrinhKhuyenMaiService;

        public ChuongTrinhKhuyenMaiController(IChuongTrinhKhuyenMaiService chuongTrinhKhuyenMaiService)
        {
            _ichuongTrinhKhuyenMaiService = chuongTrinhKhuyenMaiService;
        }

        public async Task<IActionResult> Index()
        {
            List<ChuongTrinhKhuyenMaiResponse> chuongTrinhKhuyenMaiResponses = await _ichuongTrinhKhuyenMaiService.GetAllChuongTrinhKhuyenMai();
            return View(chuongTrinhKhuyenMaiResponses);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            ChuongTrinhKhuyenMaiResponse? chuongTrinhKhuyenMaiResponse = await _ichuongTrinhKhuyenMaiService.GetChuongTrinhKhuyenMaiById(id);
            if (chuongTrinhKhuyenMaiResponse == null) return RedirectToAction("Index");
            return View(chuongTrinhKhuyenMaiResponse);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ChuongTrinhKhuyenMaiAddRequest chuongTrinhKhuyenMaiAddRequest)
        {
            ChuongTrinhKhuyenMaiResponse chuongTrinhKhuyenMaiResponse = await _ichuongTrinhKhuyenMaiService.CreateChuongTrinhKhuyenMai(chuongTrinhKhuyenMaiAddRequest);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            ChuongTrinhKhuyenMaiResponse? chuongTrinhKhuyenMaiResponse = await _ichuongTrinhKhuyenMaiService.GetChuongTrinhKhuyenMaiById(id);
            return View(chuongTrinhKhuyenMaiResponse);
        }

        [HttpPost]
        public IActionResult Edit(ChuongTrinhKhuyenMaiUpdateRequest chuongTrinhKhuyenMaiUpdateRequest)
        {
            _ichuongTrinhKhuyenMaiService.UpdateChuongTrinhKhuyenMai(chuongTrinhKhuyenMaiUpdateRequest);
            return RedirectToAction("Index");
        }
    }
}
