using APP_DATA.DTO;
using APP_VIEW.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APP_VIEW.Controllers
{
    public class HoaDonController : Controller
    {
        private readonly IHoaDonService _hoadonService;
        public HoaDonController(IHoaDonService hoaDonService)
        {
            _hoadonService=hoaDonService;
        }
        public async Task<ActionResult> Index()
        {
            List<HoaDonResponce> hoaDons = await _hoadonService.GetAllHoaDons();
            return View(hoaDons);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var hoadonResponse = await _hoadonService.GetHoaDonById(id);
            return View(hoadonResponse);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _hoadonService.DeleteHang(id);
            if (result)
            {
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
