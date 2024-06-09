using APP_DATA.DTO;
using APP_DATA.Models;
using APP_VIEW.IServices;
using Newtonsoft.Json;

namespace APP_VIEW.Services
{
    public class HangService : IHangService
    {
        HttpClient _httpClient;

        public HangService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HangResponse> AddHang(HangAddRequest? hangAddRequest)
        {
            if (hangAddRequest == null)
                throw new ArgumentNullException(nameof(hangAddRequest));
            Hang hang = hangAddRequest.ToHang();
            string requestUrl = "https://localhost:7073/api/Hang/create-hang";
            var response = _httpClient.PostAsJsonAsync(requestUrl, hangAddRequest).Result;
            return hang.ToHangResponse();
        }

        public async Task<bool> DeleteHang(Guid id)
        {
            string requestUrl = $"https://localhost:7073/api/Hang/delete-hang?id{id}";
            var response = _httpClient.DeleteAsync(requestUrl).Result;
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<List<HangResponse>> GetAllHang()
        {
            string requestUrl = "https://localhost:7073/api/Hang/get-all-hang";
            var response = _httpClient.GetStringAsync(requestUrl).Result;
            List<HangResponse>? hangResponses = JsonConvert.DeserializeObject<List<HangResponse>>(response);
            return hangResponses;
        }

        public async Task<HangResponse?> GetHangById(Guid id)
        {
            string requestUrl = $"https://localhost:7073/api/Hang/get-hang-by-id?id={id}";
            var response = _httpClient.GetStringAsync(requestUrl).Result;
            HangResponse? hangResponses = JsonConvert.DeserializeObject<HangResponse>(response);
            return hangResponses;
        }

        public async Task<HangResponse> UpdateHang(HangUpdateRequest? hangUpdateRequest)
        {
            if(hangUpdateRequest == null)
                throw new ArgumentNullException(nameof(hangUpdateRequest));
            Hang hang = hangUpdateRequest.ToHang();
            string requestUrl = "https://localhost:7073/api/Hang/update-hang";
            var response = _httpClient.PutAsJsonAsync(requestUrl, hangUpdateRequest).Result;
            return hang.ToHangResponse();
        }
    }
}
