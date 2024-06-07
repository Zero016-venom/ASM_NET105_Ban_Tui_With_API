using APP_DATA.DTO;

namespace APP_VIEW.IServices
{
    public interface ILoaiSanPhamService
    {
        Task<List<LoaiSanPhamResponse>> GetAllLoaiSanPham();

        Task<LoaiSanPhamResponse?> GetLoaiSanPhamById(Guid? id);

        Task<LoaiSanPhamResponse> AddLoaiSanPham(LoaiSanPhamAddRequest? loaiSanPhamAddRequest);

        Task<LoaiSanPhamResponse> UpdateLoaiSanPham(LoaiSanPhamUpdateRequest? loaiSanPhamUpdateRequest);

        Task<bool> DeleteLoaiSanPham(Guid? id);
    }
}
