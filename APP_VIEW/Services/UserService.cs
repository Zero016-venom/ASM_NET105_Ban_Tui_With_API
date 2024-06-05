using APP_DATA.DTO;
using APP_DATA.Models;
using APP_VIEW.IServices;
using Newtonsoft.Json;

namespace APP_VIEW.Services
{
    public class UserService : IUserService
    {
        HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<ApplicationUser> GetAllUsers()
        {
            string requestUrl = "http://localhost:5275/api/TaiKhoan/get-all-users";
            var response = _httpClient.GetStringAsync(requestUrl).Result;
            List<ApplicationUser> allUsers = JsonConvert.DeserializeObject<List<ApplicationUser>>(response);
            return allUsers;
        }

        public ApplicationUser GetUserById(Guid id)
        {
            string requestUrl = $"http://localhost:5275/api/TaiKhoan/get-user-by-id?id={id}";
            var response = _httpClient.GetStringAsync(requestUrl).Result;
            ApplicationUser users = JsonConvert.DeserializeObject<ApplicationUser>(response);
            return users;
        }

        public bool Login(LoginDTO loginDTO)
        {
            string requestUrl = "http://localhost:5275/api/TaiKhoan/register";
            var response = _httpClient.PostAsJsonAsync(requestUrl, loginDTO).Result;
            if (response.IsSuccessStatusCode) return true;
            return false;
        }

        public bool Register(RegisterDTO registerDTO)
        {
            string requestUrl = "http://localhost:5275/api/TaiKhoan/login";
            var response = _httpClient.PostAsJsonAsync(requestUrl, registerDTO).Result;
            if (response.IsSuccessStatusCode) return true;
            return false;
        }
    }
}
