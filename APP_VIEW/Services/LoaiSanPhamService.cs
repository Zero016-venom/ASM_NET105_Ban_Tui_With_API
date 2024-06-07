using APP_DATA.DTO;
using APP_DATA.Models;
using APP_VIEW.IServices;
using Newtonsoft.Json;

namespace APP_VIEW.Services
{
    public class LoaiSanPhamService : ILoaiSanPhamService
    {
        private readonly HttpClient _httpClient;

        public LoaiSanPhamService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<LoaiSanPhamResponse> AddLoaiSanPham(LoaiSanPhamAddRequest? loaiSanPhamAddRequest)
        {
            if (loaiSanPhamAddRequest == null)
                throw new ArgumentNullException(nameof(loaiSanPhamAddRequest));

            LoaiSP loaiSanPham = loaiSanPhamAddRequest.ToLoaiSanPham();
            string requestUrl = "https://localhost:7073/api/LoaiSanPham/create-loai-san-pham";
            var response = _httpClient.PostAsJsonAsync(requestUrl, loaiSanPhamAddRequest).Result;
            return loaiSanPham.ToLoaiSanPhamResponse();
        }

        public async Task<bool> DeleteLoaiSanPham(Guid? id)
        {
            string requestUrl = $"https://localhost:7073/api/LoaiSanPham/delete-loai-san-pham?id={id}";
            var response = _httpClient.DeleteAsync(requestUrl).Result;
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<List<LoaiSanPhamResponse>> GetAllLoaiSanPham()
        {
            string requestUrl = "https://localhost:7073/api/LoaiSanPham/get-all-loai-san-pham";
            var response = _httpClient.GetStringAsync(requestUrl).Result;
            List<LoaiSanPhamResponse>? loaiSanPhams = JsonConvert.DeserializeObject<List<LoaiSanPhamResponse>>(response);
            return loaiSanPhams;
        }

        public async Task<LoaiSanPhamResponse?> GetLoaiSanPhamById(Guid? id)
        {
            string requestUrl = $"https://localhost:7073/api/LoaiSanPham/get-loai-san-pham-by-id?id={id}";
            var response = _httpClient.GetStringAsync(requestUrl).Result;
            LoaiSanPhamResponse? loaiSanPham = JsonConvert.DeserializeObject<LoaiSanPhamResponse>(response);
            return loaiSanPham;
        }

        public async Task<LoaiSanPhamResponse> UpdateLoaiSanPham(LoaiSanPhamUpdateRequest? loaiSanPhamUpdateRequest)
        {
            if (loaiSanPhamUpdateRequest == null)
                throw new ArgumentNullException(nameof(loaiSanPhamUpdateRequest));

            LoaiSP loaiSanPham = loaiSanPhamUpdateRequest.ToLoaiSanPham();
            string requestUrl = "https://localhost:7073/api/LoaiSanPham/create-loai-san-pham";
            var response = _httpClient.PostAsJsonAsync(requestUrl, loaiSanPhamUpdateRequest).Result;
            return loaiSanPham.ToLoaiSanPhamResponse();
        }
    }
}
