using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APP_DATA.Models
{
    public class GioHangCT
    {
        [Key]
        public Guid ID_GioHangCT { get; set; }
        public Guid ID_User { get; set; }
        public int SoLuong { get; set; }
        public Guid? ID_SanPham { get; set; }

        [ForeignKey("ID_SanPham")]
        public virtual SanPham? SanPham { get; set; }

        [ForeignKey("ID_User")]
        public virtual GioHang? GioHang { get; set; }


    }
}