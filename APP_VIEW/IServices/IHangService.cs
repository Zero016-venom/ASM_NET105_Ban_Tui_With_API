using APP_DATA.DTO;

namespace APP_VIEW.IServices
{
    public interface IHangService
    {
        Task<List<HangResponse>> GetAllHang();

        Task<HangResponse?> GetHangById(Guid id);

        Task<HangResponse> AddHang(HangAddRequest? hangAddRequest);

        Task<HangResponse> UpdateHang(HangUpdateRequest? hangUpdateRequest);

        Task<bool> DeleteHang(Guid id);
    }
}
