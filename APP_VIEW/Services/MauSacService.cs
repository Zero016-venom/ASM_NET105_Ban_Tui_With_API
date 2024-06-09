using APP_DATA.DTO;
using APP_DATA.Models;
using APP_VIEW.IServices;
using Newtonsoft.Json;

namespace APP_VIEW.Services
{
    public class MauSacService : IMauSacService
    {
        private readonly HttpClient _httpClient;

        public MauSacService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<MauSacResponse> AddMauSac(MauSacAddRequest? mauSacAddRequest)
        {
            if (mauSacAddRequest == null)
                throw new ArgumentNullException(nameof(mauSacAddRequest));
            MauSac mauSac = mauSacAddRequest.ToMauSac();
            string requestUrl = "https://localhost:7073/api/MauSac/create-mau";
            var response = _httpClient.PostAsJsonAsync(requestUrl, mauSacAddRequest).Result;
            return mauSac.ToMauSacResponse();
        }

        public async Task<bool> DeleteMauSac(Guid id)
        {
            string requestUrl = $"https://localhost:7073/api/MauSac/delete-mau?id={id}";
            var response = _httpClient.DeleteAsync(requestUrl).Result;
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<List<MauSacResponse>> GetAllMauSac()
        {
            string requestUrl = "https://localhost:7073/api/MauSac/get-all-mau";
            var response = _httpClient.GetStringAsync(requestUrl).Result;
            List<MauSacResponse>? mauSacResponses = JsonConvert.DeserializeObject<List<MauSacResponse>>(response);
            return mauSacResponses;
        }

        public async Task<MauSacResponse?> GetMauSacById(Guid id)
        {
            string requestUrl = $"https://localhost:7073/api/MauSac/get-mau-by-id?id={id}";
            var response = _httpClient.GetStringAsync(requestUrl).Result;
            MauSacResponse? mauSacResponses = JsonConvert.DeserializeObject<MauSacResponse>(response);
            return mauSacResponses;
        }

        public async Task<MauSacResponse> UpdateMauSac(MauSacUpdateRequest? mauSacUpdateRequest)
        {
            if (mauSacUpdateRequest == null)
                throw new ArgumentNullException(nameof(mauSacUpdateRequest));

            MauSac mauSac = mauSacUpdateRequest.ToMauSac();

            string requestUrl = "https://localhost:7073/api/MauSac/update-mau";
            var response = _httpClient.PutAsJsonAsync(requestUrl, mauSacUpdateRequest).Result;
            return mauSac.ToMauSacResponse();
        }
    }
}
