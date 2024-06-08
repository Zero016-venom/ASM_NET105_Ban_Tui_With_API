using APP_DATA.DTO;
using APP_DATA.Models;

namespace APP_VIEW.IServices
{
    public interface IMauSacService
    {
        Task<List<MauSacResponse>> GetAllMauSac();

        Task<MauSacResponse?> GetMauSacById(Guid id);

        Task<MauSacResponse> AddMauSac(MauSacAddRequest? mauSacAddRequest);

        Task<MauSacResponse> UpdateMauSac(MauSacUpdateRequest? mauSacUpdateRequest);

        Task<bool> DeleteMauSac(Guid id);
    }
}
