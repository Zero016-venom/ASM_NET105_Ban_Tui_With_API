using APP_DATA.DTO;
using APP_DATA.Models;
using APP_VIEW.IServices;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace APP_VIEW.Services
{
    public class HoaDonService : IHoaDonService
    {
        private readonly HttpClient _httpClient;

        public HoaDonService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<HoaDonResponce>> GetAllHoaDons()
        {
            string requestURL = "https://localhost:7073/api/HoaDon/get-all-hoadon";
            var response = await _httpClient.GetStringAsync(requestURL);
            List<HoaDonResponce> hoaDons = JsonConvert.DeserializeObject<List<HoaDonResponce>>(response);
            return hoaDons;
        }

        public async Task<bool> DeleteHang(Guid id)
        {
            string requestURL = $"https://localhost:7073/api/HoaDon/delete-hoadon/?id={id}";
            var response = _httpClient.DeleteAsync(requestURL).Result;
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<HoaDon?> GetHoaDonById(Guid? id)
        {
            string requestURL = $"https://localhost:7073/api/HoaDon/get-hoadon-by-id?id={id}";
            var response = _httpClient.GetStringAsync(requestURL).Result;
            HoaDon hoaDonResponce = JsonConvert.DeserializeObject<HoaDon>(response);
            return hoaDonResponce;
        }
        
    }
}