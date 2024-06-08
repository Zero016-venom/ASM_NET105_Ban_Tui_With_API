using APP_DATA.DTO;

namespace APP_VIEW.IServices
{
    public interface ISanPhamService
    {
        Task<List<SanPhamResponse>> GetAllSanPhams();

        Task<SanPhamResponse?> GetSanPhamById(Guid? id);

        Task<SanPhamResponse> AddSanPham(SanPhamAddRequest? sanPhamAddRequest);

        Task<SanPhamResponse> UpdateSanPham(SanPhamUpdateRequest? sanPhamUpdateRequest);

        Task<bool> DeleteSanPham(Guid? id);
    }
}
