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
    public class ChuongTrinhKhuyenMaiAddRequest
    {
        [Required(ErrorMessage = "Tên chương trình khuyến mãi không được để trống")]
        public string? TenChuongTrinhKhuyenMai { get; set; }

        [Required(ErrorMessage = "Ngày bắt đầu không được để trống")]
        public DateTime NgayBatDau { get; set; }

        [Required(ErrorMessage = "Ngày kết thúc không được để trống")]
        public DateTime NgayKetThuc { get; set; }

        [Required(ErrorMessage = "Trạng thái không được để trống")]
        public StatusOptions? TrangThai { get; set; }

        public ChuongTrinhKhuyenMai ToChuongTrinhKhuyenMai()
        {
            return new ChuongTrinhKhuyenMai()
            {
                TenChuongTrinhKhuyenMai = TenChuongTrinhKhuyenMai,
                NgayBatDau = NgayBatDau,
                NgayKetThuc = NgayKetThuc,
                TrangThai = TrangThai.ToString()
            };
        }
    }
}
