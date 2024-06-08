using APP_DATA.DTO;
using APP_DATA.Models;
using APP_VIEW.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APP_VIEW.Controllers
{
    public class HangController : Controller
    {
        private readonly IHangService _hangService;

        public HangController(IHangService hangService)
        {
            _hangService = hangService;
        }

        public async Task<IActionResult> Index()
        {
            List<HangResponse> hangResponses = await _hangService.GetAllHang();
            return View(hangResponses);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            HangResponse? hangResponse = await _hangService.GetHangById(id);
            return View(hangResponse);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(HangAddRequest hangAddRequest)
        {
            HangResponse hangResponse = await _hangService.AddHang(hangAddRequest);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            HangResponse? hangResponse = await _hangService.GetHangById(id);
            return View(hangResponse);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(HangUpdateRequest hangUpdateRequest)
        {
            HangResponse hangResponse = await _hangService.UpdateHang(hangUpdateRequest);
            return RedirectToAction("Index");
        }
    }
}
