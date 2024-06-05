using APP_DATA.DTO;
using APP_DATA.Models;

namespace APP_VIEW.IServices
{
    public interface IChatLieuService
    {
        Task<List<ChatLieuResponse>> GetAllChatLieu();

        Task<ChatLieuResponse?> GetChatLieuById(Guid? id);

        Task<ChatLieuResponse> AddChatLieu(ChatLieuAddRequest? chatLieuAddRequest);

        Task<ChatLieuResponse> UpdateChatLieu(ChatLieu chatLieu);

        bool DeleteChatLieu(Guid? id);
    }
}
