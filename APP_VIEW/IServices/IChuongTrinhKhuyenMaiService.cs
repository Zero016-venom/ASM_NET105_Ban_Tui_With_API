using APP_DATA.DTO;

namespace APP_VIEW.IServices
{
    public interface IChuongTrinhKhuyenMaiService
    {
        Task<List<ChuongTrinhKhuyenMaiResponse>> GetAllChuongTrinhKhuyenMai();

        Task<ChuongTrinhKhuyenMaiResponse?> GetChuongTrinhKhuyenMaiById(Guid id);

        Task<ChuongTrinhKhuyenMaiResponse> CreateChuongTrinhKhuyenMai(ChuongTrinhKhuyenMaiAddRequest? chuongTrinhKhuyenMaiAddRequest);

        Task<ChuongTrinhKhuyenMaiResponse> UpdateChuongTrinhKhuyenMai(ChuongTrinhKhuyenMaiUpdateRequest? chuongTrinhKhuyenMaiUpdateRequest);

        Task<bool> DeleteChuongTrinhKhuyenMai(Guid? id);
    }
}
