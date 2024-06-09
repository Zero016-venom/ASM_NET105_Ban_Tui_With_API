using APP_DATA.DTO;
using APP_VIEW.IServices;
using Microsoft.AspNetCore.Mvc;

namespace APP_VIEW.Controllers
{
    public class LoaiSanPhamController : Controller
    {
        private readonly ILoaiSanPhamService _iloaiSanPhamService;

        public LoaiSanPhamController(ILoaiSanPhamService loaiSanPhamService)
        {
            _iloaiSanPhamService = loaiSanPhamService;
        }

        public async Task<IActionResult> Index()
        {
            List<LoaiSanPhamResponse> loaiSanPhams = await _iloaiSanPhamService.GetAllLoaiSanPham();
            return View(loaiSanPhams);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            LoaiSanPhamResponse? loaiSanPhamResponse = await _iloaiSanPhamService.GetLoaiSanPhamById(id);
            if(loaiSanPhamResponse == null)
                return RedirectToAction("Index");
            return View(loaiSanPhamResponse);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(LoaiSanPhamAddRequest loaiSanPhamAddRequest)
        {
            LoaiSanPhamResponse loaiSanPhamResponse = await _iloaiSanPhamService.AddLoaiSanPham(loaiSanPhamAddRequest);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            LoaiSanPhamResponse? loaiSanPhamResponse = await _iloaiSanPhamService.GetLoaiSanPhamById(id);
            if (loaiSanPhamResponse == null)
                return RedirectToAction("Index");
            return View(loaiSanPhamResponse);
        }

        [HttpPost]
        public IActionResult Edit(LoaiSanPhamUpdateRequest loaiSanPhamUpdateRequest)
        {
            _iloaiSanPhamService.UpdateLoaiSanPham(loaiSanPhamUpdateRequest);
            return RedirectToAction("Index");
        }
 
        public IActionResult Delete(Guid id)
        {
            _iloaiSanPhamService.DeleteLoaiSanPham(id);
            return RedirectToAction("Index");
        }
    }
}
