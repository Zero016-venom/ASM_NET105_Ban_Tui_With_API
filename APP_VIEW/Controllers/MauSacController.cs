using APP_DATA.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace APP_VIEW.Controllers
{
    public class MauSacController : Controller
    {
        HttpClient _httpClient;
        public MauSacController()
        {
            _httpClient = new HttpClient();
        }
        public ActionResult Index()
        {
            string requesUrl = $@"https://localhost:7073/api/MauSac/get-all-mau";
            var response = _httpClient.GetStringAsync(requesUrl).Result;
            var data = JsonConvert.DeserializeObject<List<MauSacResponse>>(response);
            return View(data);
        }
        public ActionResult Details(Guid id)
        {
            string requesUrl = $@"https://localhost:7073/api/MauSac/get-mau-by-id?id={id}";
            var response = _httpClient.GetStringAsync(requesUrl).Result;
            var data = JsonConvert.DeserializeObject<MauSacResponse>(response);
            return View(data);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(MauSacResponse msr)
        {
            try
            {
                string requesUrl = $@"https://localhost:7073/api/MauSac/create-mau";
                var response = _httpClient.PostAsJsonAsync(requesUrl, msr).Result;
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Edit(Guid id)
        {
            string requesUrl = $@"https://localhost:7073/api/MauSac/get-mau-by-id?id={id}";
            var response = _httpClient.GetStringAsync(requesUrl).Result;
            var data = JsonConvert.DeserializeObject<MauSacUpdateRequest>(response);
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(MauSacUpdateRequest msr)
        {
            try
            {
                string requesUrl = $@"https://localhost:7073/api/MauSac/update-mau";
                var response = _httpClient.PutAsJsonAsync(requesUrl, msr).Result;
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Delete(Guid id)
        {
            string requesUrl = $@"https://localhost:7073/api/MauSac/delete-mau?id={id}";
            var response = _httpClient.DeleteAsync(requesUrl).Result;
            return RedirectToAction("Index");
        }
    }
}
