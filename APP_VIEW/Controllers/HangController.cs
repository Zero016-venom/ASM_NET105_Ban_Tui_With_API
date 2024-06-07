using APP_DATA.DTO;
using APP_DATA.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APP_VIEW.Controllers
{
    public class HangController : Controller
    {
        HttpClient client = new HttpClient();
        public IActionResult Index()
        {
            string requestUrl = "https://localhost:7073/api/Hang/get-all-hang";
            var response = client.GetStringAsync(requestUrl).Result;
            var data = JsonConvert.DeserializeObject<List<HangResponse>>(response);
            return View(data);
        }

        public IActionResult Details(Guid id)
        {
            string requestUrl = $"https://localhost:7073/api/Hang/get-hang-by-id?id={id}";
            var response = client.GetStringAsync(requestUrl).Result;
            var data = JsonConvert.DeserializeObject<HangResponse>(response);
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(HangAddRequest hangAddRequest)
        {
            string requestUrl = "https://localhost:7073/api/Hang/create-hang";
            var response = client.PostAsJsonAsync(requestUrl, hangAddRequest).Result;
            return RedirectToAction("Index");
        }

        public IActionResult Edit(Guid id)
        {
            string requestUrl = $"https://localhost:7073/api/Hang/get-hang-by-id?id={id}";
            var response = client.GetStringAsync(requestUrl).Result;
            var data = JsonConvert.DeserializeObject<HangUpdateRequest>(response);
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(HangUpdateRequest hangUpdateRequest)
        {
            string requestUrl = $"https://localhost:7073/api/Hang/update-hang";
            var response = client.PutAsJsonAsync(requestUrl, hangUpdateRequest).Result;
            return RedirectToAction("Index");
        }

        public IActionResult Delete(Guid id)
        {
            string requestUrl = $"https://localhost:7073/api/Hang/delete-hang";
            var response = client.DeleteAsync(requestUrl).Result;
            return RedirectToAction("Index");
        }
    }
}
