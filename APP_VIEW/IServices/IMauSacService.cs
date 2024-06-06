using APP_DATA.Models;

namespace APP_VIEW.IServices
{
    public interface IMauSacService
    {
        List<MauSac> MauSac { get; set; }
        public bool CreateMauSac(MauSac mauSac);
        public bool UpdateMauSac(MauSac mauSac);
        public bool DeleteMauSac(Guid id);
        public MauSac GetMauSacById(Guid id);
    }
}
