using APP_DATA.DTO;
using APP_DATA.Models;
using APP_VIEW.IServices;
using Newtonsoft.Json;

namespace APP_VIEW.Services
{
    public class ChuongTrinhKhuyenMaiService : IChuongTrinhKhuyenMaiService
    {
        HttpClient _httpClient;

        public ChuongTrinhKhuyenMaiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ChuongTrinhKhuyenMaiResponse> CreateChuongTrinhKhuyenMai(ChuongTrinhKhuyenMaiAddRequest? chuongTrinhKhuyenMaiAddRequest)
        {
            if(chuongTrinhKhuyenMaiAddRequest == null)
                throw new ArgumentNullException(nameof(chuongTrinhKhuyenMaiAddRequest));
            ChuongTrinhKhuyenMai chuongTrinhKhuyenMai = chuongTrinhKhuyenMaiAddRequest.ToChuongTrinhKhuyenMai();
            string requestUrl = "https://localhost:7073/api/ChuongTrinhKhuyenMai/create-chuong-trinh-khuyen-mai";
            var response = _httpClient.PostAsJsonAsync(requestUrl, chuongTrinhKhuyenMaiAddRequest).Result;
            return chuongTrinhKhuyenMai.ToChuongTrinhKhuyenMaiResponse();
        }

        public async Task<bool> DeleteChuongTrinhKhuyenMai(Guid? id)
        {
            string requestUrl = $"https://localhost:7073/api/ChuongTrinhKhuyenMai/delete-chuong-trinh-khuyen-mai?id={id}";
            var response = _httpClient.DeleteAsync(requestUrl).Result;
            if(response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<List<ChuongTrinhKhuyenMaiResponse>> GetAllChuongTrinhKhuyenMai()
        {
            string requestUrl = "https://localhost:7073/api/ChuongTrinhKhuyenMai/get-all-chuong-trinh-khuyen-mai";
            var response = await _httpClient.GetStringAsync(requestUrl);
            List<ChuongTrinhKhuyenMaiResponse>? chuongTrinhKhuyenMaiResponses = JsonConvert.DeserializeObject<List<ChuongTrinhKhuyenMaiResponse>>(response);
            return chuongTrinhKhuyenMaiResponses;
        }

        public async Task<ChuongTrinhKhuyenMaiResponse?> GetChuongTrinhKhuyenMaiById(Guid id)
        {
            string requestUrl = $"https://localhost:7073/api/ChuongTrinhKhuyenMai/get-chuong-trinh-khuyen-mai-by-id?id={id}";
            var response = await _httpClient.GetStringAsync(requestUrl);
            ChuongTrinhKhuyenMaiResponse? chuongTrinhKhuyenMaiResponses = JsonConvert.DeserializeObject<ChuongTrinhKhuyenMaiResponse>(response);
            return chuongTrinhKhuyenMaiResponses;
        }

        public async Task<ChuongTrinhKhuyenMaiResponse> UpdateChuongTrinhKhuyenMai(ChuongTrinhKhuyenMaiUpdateRequest? chuongTrinhKhuyenMaiUpdateRequest)
        {
            if (chuongTrinhKhuyenMaiUpdateRequest == null)
                throw new ArgumentNullException(nameof(chuongTrinhKhuyenMaiUpdateRequest));

            ChuongTrinhKhuyenMai chuongTrinhKhuyenMai = chuongTrinhKhuyenMaiUpdateRequest.ToChuongTrinhKhuyenMai();

            string requestUrl = "https://localhost:7073/api/ChuongTrinhKhuyenMai/update-chuong-trinh-khuyen-mai";

            var response = _httpClient.PutAsJsonAsync(requestUrl, chuongTrinhKhuyenMaiUpdateRequest).Result;
            return chuongTrinhKhuyenMai.ToChuongTrinhKhuyenMaiResponse();
        }
    }
}
