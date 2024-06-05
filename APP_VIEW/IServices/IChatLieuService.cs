using APP_DATA.Models;

namespace APP_VIEW.IServices
{
    public interface IChatLieuService
    {
        List<ChatLieu> GetAllChatLieu();

        ChatLieu GetChatLieuById(Guid id);

        bool CreateChatLieu(ChatLieu chatLieu);

        bool UpdateChatLieu(ChatLieu chatLieu);

        bool DeleteChatLieu(Guid id);
    }
}
