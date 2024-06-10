using APP_DATA.DTO;
using APP_DATA.Models;
using APP_VIEW.IServices;
using Newtonsoft.Json;

namespace APP_VIEW.Services
{
    public class SanPhamService : ISanPhamService
    {
        HttpClient _httpClient;

        public SanPhamService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<SanPhamResponse> AddSanPham(SanPhamAddRequest? sanPhamAddRequest)
        {
            if(sanPhamAddRequest == null)
                throw new ArgumentNullException(nameof(sanPhamAddRequest));
            SanPham sanPham = sanPhamAddRequest.ToSanPham();
            string requestUrl = "https://localhost:7073/api/SanPham/create-san-pham";
            var response = _httpClient.PostAsJsonAsync(requestUrl, sanPhamAddRequest).Result;
            return sanPham.ToSanPhamResponse();
        }

        public async Task<bool> DeleteSanPham(Guid? id)
        {
            string requestUrl = $"https://localhost:7073/api/SanPham/delete-san-pham?id={id}";
            var response = _httpClient.DeleteAsync(requestUrl).Result;
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<List<SanPhamResponse>> GetAllSanPhams()
        {
            string requestUrl = "https://localhost:7073/api/SanPham/get-all-san-pham";
            var response = _httpClient.GetStringAsync(requestUrl).Result;
            List<SanPhamResponse>? sanPhamResponses = JsonConvert.DeserializeObject<List<SanPhamResponse>>(response);
            return sanPhamResponses;
        }

        public async Task<SanPhamResponse?> GetSanPhamById(Guid? id)
        {
            string requestUrl = $"https://localhost:7073/api/SanPham/get-san-pham-by-id?id={id}";
            var response = _httpClient.GetStringAsync(requestUrl).Result;
            SanPhamResponse? sanPhamResponses = JsonConvert.DeserializeObject<SanPhamResponse>(response);
            return sanPhamResponses;

            //string requestUrl = $"https://localhost:7073/api/SanPham/get-san-pham-by-id?id={id}";
            //HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);

            //string responseContent = await response.Content.ReadAsStringAsync();
            //SanPhamResponse? sanPhamResponse = JsonConvert.DeserializeObject<SanPhamResponse>(responseContent);
            //return sanPhamResponse;
        }

        public async Task<SanPhamResponse> UpdateSanPham(SanPhamUpdateRequest? sanPhamUpdateRequest)
        {
            if(sanPhamUpdateRequest == null)
                throw new ArgumentNullException(nameof(sanPhamUpdateRequest));
            SanPham sanPham = sanPhamUpdateRequest.ToSanPham();
            string requestUrl = "https://localhost:7073/api/SanPham/update-san-pham-";
            var response = _httpClient.PutAsJsonAsync(requestUrl, sanPhamUpdateRequest).Result;
            return sanPham.ToSanPhamResponse();
        }
    }
}
