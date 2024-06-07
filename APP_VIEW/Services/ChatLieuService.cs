using APP_DATA.DTO;
using APP_DATA.Models;
using APP_VIEW.IServices;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace APP_VIEW.Services
{
    public class ChatLieuService : IChatLieuService
    {
        private readonly HttpClient _httpClient;

        public ChatLieuService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ChatLieuResponse> AddChatLieu(ChatLieuAddRequest? chatLieuAddRequest)
        {
            if (chatLieuAddRequest == null)
                throw new ArgumentNullException(nameof(chatLieuAddRequest));

            ChatLieu chatLieu = chatLieuAddRequest.ToChatLieu();
            //chatLieu.ID_ChatLieu = Guid.NewGuid();
            
            var requestUrl = "https://localhost:7073/api/ChatLieu/create-chat-lieu";

            var response = _httpClient.PostAsJsonAsync(requestUrl, chatLieuAddRequest).Result;
            return chatLieu.ToChatLieuResponse();
        }

        public async Task<bool> DeleteChatLieu(Guid? id)
        {
            string requestURL = $"https://localhost:7073/api/ChatLieu/delete-chat-lieu?id={id}";
            var response = _httpClient.DeleteAsync(requestURL).Result;
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<List<ChatLieuResponse>> GetAllChatLieu()
        {
            string requestURL = "https://localhost:7073/api/ChatLieu/get-all-chat-lieu";
            var response = await _httpClient.GetStringAsync(requestURL);
            List<ChatLieuResponse> chatLieus = JsonConvert.DeserializeObject<List<ChatLieuResponse>>(response);
            return chatLieus;
        }

        public async Task<ChatLieuResponse> GetChatLieuById(Guid? id)
        {
            string requestURL = $"https://localhost:7073/api/ChatLieu/get-by-id?id={id}";
            var response = await _httpClient.GetStringAsync(requestURL);
            ChatLieuResponse chatLieu = JsonConvert.DeserializeObject<ChatLieuResponse>(response);
            return chatLieu;
        }

        public Task<ChatLieuResponse> UpdateChatLieu(ChatLieuUpdateRequest? chatLieuUpdateRequest)
        {
            throw new NotImplementedException();
        }
    }
}
