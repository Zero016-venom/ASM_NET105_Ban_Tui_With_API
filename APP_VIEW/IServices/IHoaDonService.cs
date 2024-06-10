using APP_DATA.DTO;
using APP_DATA.Models;

namespace APP_VIEW.IServices
{
    public interface IHoaDonService
    {
        Task<List<HoaDonResponce>> GetAllHoaDons();
        Task<HoaDon?> GetHoaDonById(Guid? id);
        Task<bool> DeleteHang(Guid id);
    }
}
