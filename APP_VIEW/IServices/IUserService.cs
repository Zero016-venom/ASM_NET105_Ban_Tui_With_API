using APP_DATA.DTO;
using APP_DATA.Models;

namespace APP_VIEW.IServices
{
    public interface IUserService
    {
        List<ApplicationUser> GetAllUsers();

        ApplicationUser GetUserById(Guid id);

        bool Register(RegisterDTO registerDTO);

        bool Login(LoginDTO loginDTO);
    }
}
