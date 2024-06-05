using APP_DATA.Models;
using APP_VIEW.IServices;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace APP_VIEW.Services
{
    public class ChatLieuService : IChatLieuService
    {
        HttpClient _httpClient;

        public ChatLieuService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public bool CreateChatLieu(ChatLieu chatLieu)
        {
            string requestUrl = "https://localhost:7073/api/ChatLieu/get-all-chat-lieu";
            var response = _httpClient.PostAsJsonAsync(requestUrl, chatLieu).Result;
            if (response.IsSuccessStatusCode) return true;
            return false;
        }

        public bool DeleteChatLieu(Guid id)
        {
            string requestUrl = $"https://localhost:7073/api/ChatLieu/delete-chat-lieu?id={id}";
            var response = _httpClient.DeleteAsync(requestUrl).Result;
            if (response.IsSuccessStatusCode) return true;
            return false;
        }

        public List<ChatLieu> GetAllChatLieu()
        {
            string requestUrl = "https://localhost:7073/api/ChatLieu/get-all-chat-lieu";
            var response = _httpClient.GetStringAsync(requestUrl).Result;
            List<ChatLieu> allChatLieus = JsonConvert.DeserializeObject<List<ChatLieu>>(response);
            return allChatLieus;
        }

        public ChatLieu GetChatLieuById(Guid id)
        {
            string requestUrl = $"https://localhost:7073/api/ChatLieu/get-chat-lieu-by-id?id={id}";
            var response = _httpClient.GetStringAsync(requestUrl).Result;
            ChatLieu chatLieus = JsonConvert.DeserializeObject<ChatLieu>(response);
            return chatLieus;
        }

        public bool UpdateChatLieu(ChatLieu chatLieu)
        {
            string requestUrl = "https://localhost:7073/api/ChatLieu/update-chat-lieu";
            var response = _httpClient.PutAsJsonAsync(requestUrl, chatLieu).Result;
            if (response.IsSuccessStatusCode) return true;
            return false;
        }
    }
}
