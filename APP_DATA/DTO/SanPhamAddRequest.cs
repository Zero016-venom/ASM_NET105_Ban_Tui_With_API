using APP_DATA.Enums;
using APP_DATA.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP_DATA.DTO
{
    public class SanPhamAddRequest
    {
        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        public string? TenSanPham { get; set; }

        [Required(ErrorMessage = "Phải chọn hãng")]
        public Guid? ID_Hang { get; set; }

        [Required(ErrorMessage = "Phải nhập số lượng tồn")]
        public int SoLuongTon { get; set; }

        [Required(ErrorMessage = "Phải chọn màu sắc")]
        public Guid? ID_MauSac { get; set; }

        [Required(ErrorMessage = "Phải nhập giá niêm yết")]
        public decimal GiaNiemYet { get; set; }

        [Required(ErrorMessage = "Phải chọn chất liệu")]
        public Guid? ID_ChatLieu { get; set; }

        [Required(ErrorMessage = "Phải chọn loại sản phẩm")]
        public Guid? ID_LoaiSP { get; set; }

        public string? Img { get; set; }

        [Required(ErrorMessage = "Phải chọn trạng thái")]
        public StatusOptions? TrangThai { get; set; }

        public SanPham ToSanPham()
        {
            return new SanPham()
            {
                TenSanPham = TenSanPham,
                ID_Hang = ID_Hang,
                SoLuongTon = SoLuongTon,
                ID_MauSac = ID_MauSac,
                GiaNiemYet = GiaNiemYet,
                ID_ChatLieu = ID_ChatLieu,
                ID_LoaiSP = ID_LoaiSP,
                Img = Img,
                TrangThai = TrangThai.ToString()
            };
        }
    }
}
