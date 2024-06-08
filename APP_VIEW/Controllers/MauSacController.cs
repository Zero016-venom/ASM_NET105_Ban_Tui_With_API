using APP_DATA.DTO;
using APP_VIEW.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace APP_VIEW.Controllers
{
    public class MauSacController : Controller
    {
        private readonly IMauSacService _mauSacService;

        public MauSacController(IMauSacService mauSacService)
        {
            _mauSacService = mauSacService;
        }

        public async Task<ActionResult> Index()
        {
            List<MauSacResponse> mauSacResponses = await _mauSacService.GetAllMauSac();
            return View(mauSacResponses);
        }

        public async Task<ActionResult> Details(Guid id)
        {
            MauSacResponse? mauSacResponse = await _mauSacService.GetMauSacById(id);
            return View(mauSacResponse);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(MauSacAddRequest mauSacAddRequest)
        {
            MauSacResponse mauSacResponse = await _mauSacService.AddMauSac(mauSacAddRequest);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(Guid id)
        {
            MauSacResponse? mauSacResponse = await _mauSacService.GetMauSacById(id);
            return View(mauSacResponse);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(MauSacUpdateRequest mauSacUpdateRequest)
        {
            MauSacResponse mauSacResponse = await _mauSacService.UpdateMauSac(mauSacUpdateRequest);
            return RedirectToAction("Index");
        }
    }
}
